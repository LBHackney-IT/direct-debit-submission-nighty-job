using CsvHelper;
using CsvHelper.Configuration;
using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class DirectDebitExportUseCase : IDirectDebitExportUseCase
    {
        private readonly IDirectDebitExportGateway _gateway;
        private readonly IDirectDebitSubmissionGateway _submissionGateway;

        public DirectDebitExportUseCase(IDirectDebitExportGateway gateway, IDirectDebitSubmissionGateway submissionGateway)
        {
            _gateway = gateway;
            _submissionGateway = submissionGateway;
        }

        public async Task<byte[]> ExecuteAsync(DirectDebitExportRequest directDebitExportRequest)
        {
            var result = directDebitExportRequest?.FileType switch
            {
                "csv" => await GetCSVFileResult(directDebitExportRequest).ConfigureAwait(false),
                "dat" => await GetDATFileResult(directDebitExportRequest).ConfigureAwait(false),
                _ => null
            };
            return result;
        }
        private async Task<byte[]> GetCSVFileResult(DirectDebitExportRequest directDebitExportRequest)
        {
            var fileDataCSV = await GetFileData(directDebitExportRequest).ConfigureAwait(false);
            if (fileDataCSV.Any())
                return WriteCSVFile(fileDataCSV);
            else return null;
        }
        private async Task<byte[]> GetDATFileResult(DirectDebitExportRequest directDebitExportRequest)
        {
            var fileDataDAT = await GetFileData(directDebitExportRequest).ConfigureAwait(false);
            if (fileDataDAT.Any())
                return WriteDatFile(fileDataDAT);
            else return null;
        }
        private async Task<List<DirectDebitExport>> GetFileData(DirectDebitExportRequest request)
        {
            IEnumerable<DirectDebit> result;
            if (request.Date.HasValue)
            {
                result = await _submissionGateway.GetAllDirectDebitsListAsync(new DirectDebitSubmissionQuery { DateToCollectAmount = request.Date }).ConfigureAwait(false);
            }
            else
            {
                result = await _gateway.GetAllDirectDebitsListAsync(request).ConfigureAwait(false);
            }

            return result.Select(x => x.ToExport()).ToList();
        }
        private static byte[] WriteDatFile(List<DirectDebitExport> directDebits)
        {
            byte[] result;
            using (var ms = new MemoryStream())
            {
                using StreamWriter bw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true));
                directDebits.ForEach(item =>
                {
                    bw.WriteLine("{0,-10}{1,-15}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}",
                        item.Status, item.Name, item.PRN, item.AccountNumber,
                        item.Fund, item.Acc, item.Trans, item.Amount);
                });
                bw.Flush();
                result = ms.ToArray();
            }
            return result;
        }
        private static byte[] WriteCSVFile(List<DirectDebitExport> directDebits)
        {
            byte[] result;
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true)))
                {
                    using var cw = new CsvWriter(sw, cc);
                    cw.WriteRecords(directDebits);
                }
                result = ms.ToArray();
            }
            return result;
        }
    }
}
