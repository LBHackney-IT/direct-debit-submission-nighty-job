using AutoMapper;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.UseCase
{
    public class GenerateWeeklySubmissionFileUseCase : IGenerateWeeklySubmissionFileUseCase
    {
        private readonly IDirectDebitSubmissionGateway _directDebitSubmissionGateway;
        private readonly IPTXPaymentApiService _iPTXFileUploadService;
        private readonly IDirectDebitGateway _directDebitGateway;
        private readonly IMapper _mapper;
        private readonly ICreateTransactionRecordUseCase _createTransactionRecordUseCase;

        public GenerateWeeklySubmissionFileUseCase(IDirectDebitSubmissionGateway directDebitSubmissionGateway,
            IPTXPaymentApiService iPTXFileUploadService,
            IDirectDebitGateway directDebitGateway,
            IMapper mapper,
            ICreateTransactionRecordUseCase createTransactionRecordUseCase)
        {
            _directDebitSubmissionGateway = directDebitSubmissionGateway;
            _iPTXFileUploadService = iPTXFileUploadService;
            _directDebitGateway = directDebitGateway;
            _mapper = mapper;
            _createTransactionRecordUseCase = createTransactionRecordUseCase;
        }

        public async Task ProcessMessageAsync(ILogger logger)
        {
            var collectionDate = DateTime.UtcNow.Day;
            var submissionRequest = new DirectDebitSubmissionRequest { DateOfCollection = collectionDate };
            var directDebits = await _directDebitGateway.GetAllDirectDebitsByQueryAsync(submissionRequest).ConfigureAwait(false);
            logger.LogInformation($"{directDebits.Count} records to be process");
            if (directDebits.Any())
            {
                var data = _mapper.Map<List<PTXSubmissionFileData>>(directDebits);
                var generatedFile = GenerateHousingRentFile(data);
                var filename = $"DDWeekly{DateTime.UtcNow:yyyyddMHHmmss}.dat";
                byte[] result = Encoding.ASCII.GetBytes(generatedFile);
                var rId = await _iPTXFileUploadService.SubmitDirectDebitFile(result, filename).ConfigureAwait(false);
                if (string.IsNullOrEmpty(rId))
                    return;

                var fileSubmissionStatus = await _iPTXFileUploadService.GetResultSummaryByFileIdAsync(rId).ConfigureAwait(false);
                logger.LogInformation($"======File submission Status:=={fileSubmissionStatus.Status}");
                if (fileSubmissionStatus.Status == "SUCCESS")
                {
                    var fileData = new DirectDebitSubmission
                    {
                        DirectDebits = directDebits,
                        FileName = filename,
                        DateOfCollection = submissionRequest.DateOfCollection ?? directDebits.FirstOrDefault().PreferredDate,
                        Status = fileSubmissionStatus?.Status ?? "Unknow",
                        PTXSubmissionResponse = fileSubmissionStatus
                    };

                    await _directDebitSubmissionGateway.UploadFileAsync(fileData).ConfigureAwait(false);
                    logger.LogInformation($"Submitted records logged to DB");
                    // Create transaction record on Transaction API
                    await _createTransactionRecordUseCase.CreateTransactionRecordAsync(directDebits);
                    logger.LogInformation($"Payment credited");
                }
            }
        }

        private static string GenerateHousingRentFile(List<PTXSubmissionFileData> pTXSubmissionFileDatas)
        {
            var sb = new StringBuilder();
            foreach (var item in pTXSubmissionFileDatas)
            {
                var date = DateTime.UtcNow.ToString("yyMdd");
                var prn = item.Ref.PadLeft(10);
                sb.AppendLine($"{item.Sort,-6}{item.Number,-8}0{item.Type}{Environment.GetEnvironmentVariable("HackneySortCode"),-6}{Environment.GetEnvironmentVariable("HackneyAccountNumber"),-8}0000{item.Amount}{Environment.GetEnvironmentVariable("HackneyAccountName"),-18}{prn.Substring(0, 10)}hsg rent{item.Name,-18} {date}");
            }
            return sb.ToString();
        }

    }
}
