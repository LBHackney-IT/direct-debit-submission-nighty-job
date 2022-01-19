using DirectDebitApi.V1.Boundary;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways.Interfaces
{
    interface ITransactionsApi
    {
        Task<TransactionRequest> CreateAccountWithTenure(TransactionRequest transactionRequest);
    }
}
