using AutoMapper;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Gateways;
using DirectDebitSubmissionNightyJob.Gateways.Interfaces;
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

            if (directDebits.Any())
            {
                var data = _mapper.Map<List<PTXSubmissionFileData>>(directDebits);
                var generatedFile = GenerateHousingRentFile(data);
                var filename = $"DDWeekly{DateTime.UtcNow:yyyyddMHHmmss}.dat";
                byte[] result = Encoding.ASCII.GetBytes(generatedFile); ;

                var ptxResponse = await _iPTXFileUploadService.SubmitDirectDebitFile(result, filename).ConfigureAwait(false);
                if (ptxResponse.Item1)
                {
                    var fileData = new DirectDebitSubmission
                    {
                        DirectDebits = directDebits,
                        FileName = filename,
                        DateOfCollection = submissionRequest.DateOfCollection ?? directDebits.FirstOrDefault().PreferredDate,
                        Status = ptxResponse.Item2?.Status ?? "Unknow",
                        PTXSubmissionResponse = ptxResponse?.Item2
                    };

                    await _directDebitSubmissionGateway.UploadFileAsync(fileData).ConfigureAwait(false);

                    // Create transaction record on Transaction API
                    await _createTransactionRecordUseCase.CreateTransactionRecordAsync(directDebits);
                }
            }
        }

        private static string GenerateHousingRentFile(List<PTXSubmissionFileData> pTXSubmissionFileDatas)
        {
            var sb = new StringBuilder();
            foreach (var item in pTXSubmissionFileDatas)
            {
                var date = DateTime.UtcNow.ToString("yyMdd");
                sb.AppendLine($"{item.Sort,-6}{item.Number,-8}0{item.Type}{Environment.GetEnvironmentVariable("HackneySortCode"),-6}{Environment.GetEnvironmentVariable("HackneyAccountNumber"),-8}0000{item.Amount}{Environment.GetEnvironmentVariable("HackneyAccountName"),-18}{item.Ref.Substring(0, 10)}hsg rent{item.Name,-18} {date}");
            }
            return sb.ToString();
        }

        public int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
    }
}
