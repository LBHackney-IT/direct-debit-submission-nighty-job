using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DirectDebitSubmissionNightyJob.Boundary.Request.Account;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;

namespace DirectDebitSubmissionNightyJob.UseCase
{
    public class UpdateRentAccountUseCase : IUpdateRentAccountUseCase
    {
        private readonly IAccountApiService _accountApiService;
        private readonly IMapper _mapper;

        public UpdateRentAccountUseCase(IAccountApiService accountApiService, IMapper mapper)
        {
            _accountApiService = accountApiService;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(DirectDebit directDebit)
        {
            var accountResponse = await _accountApiService.GetAccountInformationByPrn(directDebit.PaymentReference);
            accountResponse.AccountBalance += directDebit.AdditionalAmount;

            var updateRequest = _mapper.Map<AccountUpdateRequest>(accountResponse);

            await _accountApiService.UpdateRentAccountBalance(updateRequest);
        }
    }
}
