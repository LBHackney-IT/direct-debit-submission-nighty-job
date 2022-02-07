using AutoMapper;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway
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
    }
}
