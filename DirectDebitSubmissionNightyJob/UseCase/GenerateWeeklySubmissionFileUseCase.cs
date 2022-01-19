using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.Gateways.Interfaces;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Gateways;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.UseCase
{
    public class GenerateWeeklySubmissionFileUseCase : IGenerateWeeklySubmissionFileUseCase
    {

        private readonly IDirectDebitSubmissionGateway _gateway;
        private readonly IPTXPaymentApiService _iPTXFileUploadService;
        private readonly IDirectDebitGateway _debitGateway;
        //private readonly IMapper _mapper;

        public GenerateWeeklySubmissionFileUseCase(IDirectDebitSubmissionGateway gateway, IPTXPaymentApiService iPTXFileUploadService,
                                               IDirectDebitGateway debitGateway)
        {
            _gateway = gateway;
            _iPTXFileUploadService = iPTXFileUploadService;
            _debitGateway = debitGateway;
            //_mapper = mapper;
        }
        public async Task ProcessMessageAsync(ILogger logger)
        {
            var collectionDate = DateTime.UtcNow.Day;
            var submissionRequest = new DirectDebitSubmissionRequest { DateOfCollection = collectionDate };
            var directDebits = await _debitGateway.GetAllDirectDebitsByQueryAsync(submissionRequest).ConfigureAwait(false);

            if (directDebits.Any())
            {

                var data = new List<PTXSubmissionFileData>();
                var generatedFile = Generate(data);
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

                    return await _gateway.UploadFileAsync(fileData).ConfigureAwait(false);
                }

            }
        }

        private static string Generate(List<PTXSubmissionFileData> pTXSubmissionFileDatas)
        {
            var hackneySortCode = "300002";
            var hackneyAccountNumber = "00641877";
            var hackneyAccountName = "LB Hackney";
            var sb = new StringBuilder();
            foreach (var item in pTXSubmissionFileDatas)
            {
                var date = DateTime.UtcNow.ToString("yyMdd");
                sb.AppendLine($"{item.Sort.PadRight(6)}{item.Number.PadRight(8)}0{item.Type}{hackneySortCode}{hackneyAccountNumber}0000{item.Amount}{hackneyAccountName.PadRight(18)}{item.Ref.PadRight(10)}HSGSUN {item.Name,-18}{date}");
            }
            return sb.ToString();
        }
    }
}
