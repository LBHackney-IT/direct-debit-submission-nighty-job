using AutoMapper;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Boundary.Response;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Extension;
using DirectDebitSubmissionNightyJob.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateways
{
    public class DirectDebitGateway : IDirectDebitGateway
    {
        private readonly DirectDebitContext _directDebitContext;
        private readonly IMapper _mapper;
        public DirectDebitGateway(DirectDebitContext directDebitContext, IMapper mapper)
        {
            _directDebitContext = directDebitContext;
            _mapper = mapper;
        }

        public async Task AddAsync(DirectDebit directDebit)
        {
            var directDebitDatabase = _mapper.Map<DirectDebitDbEntity>(directDebit);
            directDebitDatabase.BankAccountNumber = directDebitDatabase.BankAccountNumber.PadLeft(8, '0');// ;
            await _directDebitContext.AddAsync(directDebitDatabase).ConfigureAwait(false);
            await _directDebitContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<PagedResult<DirectDebit>> GetAllDirectDebitsAsync(DirectDebitQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            bool isNumber = int.TryParse(query.SearchKeyword, out int n);
            var list = _directDebitContext.DirectDebitDbEntities
                .AsNoTracking()
               .Include(x => x.DirectDebitMaintenance).AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchKeyword) && !isNumber)
            {
                var jsonQuery = new[] { new { Name = query.SearchKeyword } };
                var search = JsonSerializer.Serialize(jsonQuery);
                list = list.Where(x => x.Status.Contains(query.SearchKeyword)
                                || x.Reference.Contains(query.SearchKeyword)
                                || x.PaymentReference.Contains(query.SearchKeyword)
                                || EF.Functions.JsonContains(x.AccountHolders, search));
            }

            if (query.CollectionDate.HasValue || isNumber)
                list = query.CollectionDate.HasValue ? list.Where(x => x.PreferredDate! == query.CollectionDate.Value) : list.Where(x => x.PreferredDate! == n);

            if (query.TargetType.HasValue)
                list = list.Where(x => x.TargetType == query.TargetType.Value);

            if (query.TargetId.HasValue && query.TargetId.Value != Guid.Empty)
                list = list.Where(x => x.TargetId == query.TargetId.Value);


            var result = await list.OrderByDescending(x => x.BankAccountNumber)
                .GetPagedAsync<DirectDebitDbEntity, DirectDebit>(_mapper, query.CurrentPage, query.PageSize).ConfigureAwait(false);

            return result;

        }

        public async Task<List<DirectDebit>> GetAllDirectDebitsByPrnsync(string prn)
        {
            var response = await _directDebitContext.DirectDebitDbEntities
                .AsNoTracking().Where(c => c.PaymentReference == prn).ToListAsync().ConfigureAwait(false);
            return _mapper.Map<List<DirectDebit>>(response);
        }

        public async Task<List<DirectDebit>> GetAllDirectDebitsByQueryAsync(DirectDebitSubmissionRequest query)
        {
            var list = new List<DirectDebit>();
            if (query.DateOfCollection.HasValue)
            {
                var response = await _directDebitContext.DirectDebitDbEntities
               .AsNoTracking().Where(x => x.PreferredDate == query.DateOfCollection && !x.IsPaused && !x.IsCancelled).ToListAsync().ConfigureAwait(false);
                list.AddRange(_mapper.Map<List<DirectDebit>>(response));
            }
            else
            {
                if (query.DirectDebits != null && query.DirectDebits.Any())
                {
                    foreach (var id in query.DirectDebits)
                    {
                        var data = await _directDebitContext.DirectDebitDbEntities.FirstOrDefaultAsync(x => x.Id == id && !x.IsPaused && !x.IsCancelled).ConfigureAwait(false);
                        if (data != null)
                            list.Add(_mapper.Map<DirectDebit>(data));
                    }
                }
            }


            return list;
        }

        public async Task<DirectDebit> GetDirectDebitByIdAsync(Guid id)
        {
            var rId = await _directDebitContext
                .DirectDebitDbEntities
                .AsNoTracking()
                 .Include(x => x.DirectDebitMaintenance)
                .FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return _mapper.Map<DirectDebit>(rId);
        }

        public async Task UpdateAsync(Guid id, DirectDebit directDebit)
        {
            if (directDebit == null)
            {
                throw new ArgumentNullException(nameof(directDebit));
            }
            var rId = await _directDebitContext.DirectDebitDbEntities.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (rId != null)
            {
                rId.Status = directDebit.Status;
                rId.AdditionalAmount = directDebit.AdditionalAmount;
                rId.PreferredDate = directDebit.PreferredDate;
                _directDebitContext.Update(rId);
                await _directDebitContext.SaveChangesAsync().ConfigureAwait(false);
            }

        }


    }
}
