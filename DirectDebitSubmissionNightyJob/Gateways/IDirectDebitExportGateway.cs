using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public interface IDirectDebitExportGateway
    {
        public Task<List<DirectDebit>> GetAllDirectDebitsListAsync(DirectDebitExportRequest debitExportRequest);
    }
}
