using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Factories;
using DirectDebitSubmissionNightyJob.Gateways;
using DirectDebitSubmissionNightyJob.Infrastructure;

namespace DirectDebitSubmissionNightyJob.Gateways
{
    public class DirectDebitSubmissionGateway : IDirectDebitSubmissionGateway
    {
        private readonly DirectDebitContext _directDebitContext;

        public DirectDebitSubmissionGateway(DirectDebitContext directDebitContext)
        {
            _directDebitContext = directDebitContext;
        }
        public async Task<List<DirectDebit>> GetAllDirectDebitsListAsync(DirectDebitSubmissionQuery submissionQuery)
        {
            var data = await _directDebitContext.DirectDebitSubmissionDbEntities.AsNoTracking().FirstOrDefaultAsync(s => s.DateCreated.Date == submissionQuery.DateToCollectAmount.Value.Date).ConfigureAwait(false);

            var response = data.ToDomain();

            return response.DirectDebits;
        }


        public async Task<IEnumerable<DirectDebitSubmission>> GetAllDirectDebitsSubmissionListAsync()
        {
            var data = await _directDebitContext.DirectDebitSubmissionDbEntities
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);


            return data.Select(s => s.ToDomain());
        }
        public async Task<DirectDebitSubmission> GetDirectDebitsSubmissionAsync(DirectDebitSubmissionQuery submissionQuery)
        {
            if (!string.IsNullOrWhiteSpace(submissionQuery.Status) || submissionQuery.DateToCollectAmount.HasValue)
            {
                var data = _directDebitContext.DirectDebitSubmissionDbEntities.AsNoTracking();
                if (submissionQuery.DateToCollectAmount.HasValue)
                    data = data.Where(x => x.DateCreated.Date == submissionQuery.DateToCollectAmount.Value.Date);

                if (!string.IsNullOrWhiteSpace(submissionQuery.Status))
                    data = data.Where(x => x.Status.Equals(submissionQuery.Status));

                var result = await data.FirstOrDefaultAsync().ConfigureAwait(false);
                return result.ToDomain();
            }



            return null;
        }

        public async Task<bool> UploadFileAsync(DirectDebitSubmission directDebitSubmission)
        {
            await _directDebitContext.DirectDebitSubmissionDbEntities.AddAsync(directDebitSubmission.ToDatabase()).ConfigureAwait(false);
            await _directDebitContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}
