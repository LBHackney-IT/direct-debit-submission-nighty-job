using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.Gateways.Interfaces;
using DirectDebitApi.V1.UseCase.Interfaces;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.Gateways;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class AddDirectDebitSubmissionUseCase : IAddDirectDebitSubmissionUseCase
    {

        private readonly IDirectDebitSubmissionGateway _gateway;
        private readonly IPTXPaymentApiService _iPTXFileUploadService;
        private readonly IDirectDebitGateway _debitGateway;
        private readonly IMapper _mapper;

        public AddDirectDebitSubmissionUseCase(IDirectDebitSubmissionGateway gateway, IPTXPaymentApiService iPTXFileUploadService,
                                               IDirectDebitGateway debitGateway, IMapper mapper)
        {
            _gateway = gateway;
            _iPTXFileUploadService = iPTXFileUploadService;
            _debitGateway = debitGateway;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(DirectDebitSubmissionRequest submissionRequest)
        {
            if (submissionRequest != null)
            {
                var directDebits = await _debitGateway.GetAllDirectDebitsByQueryAsync(submissionRequest).ConfigureAwait(false);

                if (directDebits.Any())
                {
                    
                    var data = _mapper.Map<List<PTXSubmissionFileData>>(directDebits);
                    var generatedFile = Generate(data);
                    var filename = $"DDWeekly{DateTime.UtcNow:yyyyddMHHmmss}.dat";
                    byte[] result= Encoding.ASCII.GetBytes(generatedFile); ;

                    
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

            return false;

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
