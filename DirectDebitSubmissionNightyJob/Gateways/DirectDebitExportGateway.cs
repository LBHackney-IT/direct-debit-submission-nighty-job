using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public class DirectDebitExportGateway : IDirectDebitExportGateway
    {
        private readonly DirectDebitContext _directDebitContext;

        public DirectDebitExportGateway(DirectDebitContext directDebitContext)
        {
            _directDebitContext = directDebitContext;
        }

        public async Task<List<DirectDebit>> GetAllDirectDebitsListAsync(DirectDebitExportRequest debitExportRequest)
        {
            var result = new List<DirectDebit>();
            if (debitExportRequest.Date.HasValue)
            {
                var response = await _directDebitContext.DirectDebitDbEntities.Where(c => c.PreferredDate == debitExportRequest.Date.Value.Day).ToListAsync().ConfigureAwait(false);
                result = response.Select(x => x.ToDomain()).ToList();
            }
            else
            {
                if (debitExportRequest.SelectedDirectDebitLists != null && debitExportRequest.SelectedDirectDebitLists.Any())
                {
                    foreach (var item in debitExportRequest.SelectedDirectDebitLists)
                    {
                        var data = await _directDebitContext.DirectDebitDbEntities.FindAsync(item).ConfigureAwait(false);
                        if (data != null)
                            result.Add(data.ToDomain());
                    }
                }
            }

            return result;
        }
    }
}
