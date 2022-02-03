using AutoMapper;
using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.Gateways.Interfaces;
using DirectDebitSubmissionNightyJob.Boundary;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
using Hackney.Core.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.UseCase
{
    public class GenerateWeeklySubmissionFileForUploadUseCase : IGenerateWeeklySubmissionFileForUploadUseCase
    {

        private readonly IDirectDebitSubmissionGateway _gateway;
        private readonly IPTXPaymentApiService _iPTXFileUploadService;
        private readonly IDirectDebitGateway _debitGateway;
        private readonly IMapper _mapper;

        public GenerateWeeklySubmissionFileForUploadUseCase(IDirectDebitSubmissionGateway gateway, IPTXPaymentApiService iPTXFileUploadService,
                                               IDirectDebitGateway debitGateway, IMapper mapper)
        {
            _gateway = gateway;
            _iPTXFileUploadService = iPTXFileUploadService;
            _debitGateway = debitGateway;
            _mapper = mapper;
        }

        [LogCall(LogLevel.Information)]
        public async Task ProcessMessageAsync(ILogger logger)
        {
            try
            {
                var submissionRequest = new DirectDebitSubmissionRequest { };
                var directDebits = await _debitGateway.GetAllDirectDebitsByQueryAsync(submissionRequest).ConfigureAwait(false);

                if (directDebits.Any())
                {

                    var data = _mapper.Map<List<PTXSubmissionFileData>>(directDebits);
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
            catch (Exception ex)
            {
                logger.LogError($"An error occur {Environment.NewLine}==========================Details:=====================" +
                    $"{Environment.NewLine}{ex.Message}{ex?.InnerException?.Message}");
                throw;
            }


        }

    }
}
