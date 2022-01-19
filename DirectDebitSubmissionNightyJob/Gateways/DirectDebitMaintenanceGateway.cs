using AutoMapper;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public class DirectDebitMaintenanceGateway : IDirectDebitMaintenanceGateway
    {
        private readonly DirectDebitContext _directDebitContext;
        private readonly IMapper _mapper;

        public DirectDebitMaintenanceGateway(DirectDebitContext directDebitContext, IMapper mapper)
        {
            _directDebitContext = directDebitContext;
            _mapper = mapper;
        }

        public async Task AddAsync(DirectDebitMaintenance directDebitMaintenance)
        {

            var dbMaintenanceEntity = _mapper.Map<DirectDebitMaintenanceDbEntity>(directDebitMaintenance);
            var rdirectDebit = await _directDebitContext
                                            .DirectDebitDbEntities
                                          .FirstOrDefaultAsync(x => x.Id == dbMaintenanceEntity.DirectDebitId)
                                          .ConfigureAwait(false);

            await _directDebitContext.DirectDebitMaintenanceDbEntities.AddAsync(dbMaintenanceEntity).ConfigureAwait(false);
            await _directDebitContext.SaveChangesAsync().ConfigureAwait(false);

            rdirectDebit.Status = dbMaintenanceEntity.Status.ToString();
            switch (directDebitMaintenance.Status)
            {
                case Domain.Enums.StatusEnum.Pending:
                case Domain.Enums.StatusEnum.Applied:
                    rdirectDebit.PreferredDate = dbMaintenanceEntity.NewPreferredDate.Value;
                    rdirectDebit.AdditionalAmount = dbMaintenanceEntity.NewAdditionalAmount.Value;
                    break;
                case Domain.Enums.StatusEnum.Cancelled:
                    rdirectDebit.CancellationDate = DateTime.UtcNow;
                    break;
                case Domain.Enums.StatusEnum.Paused:
                    {
                        int duration = dbMaintenanceEntity.PauseDuration.Value;
                        rdirectDebit.IsPause = true;
                        rdirectDebit.PauseDate = DateTime.UtcNow;
                        rdirectDebit.PauseTillDate = duration > 0 ? DateTime.UtcNow.AddMonths(duration) : (DateTime?) null;
                        break;
                    }
            }
            _directDebitContext.DirectDebitDbEntities.Update(rdirectDebit);
            await _directDebitContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<DirectDebitMaintenance>> GetDirectDebitMaintenanceByDDIdAsync(Guid directDebitId)
        {
            var rId = await _directDebitContext.DirectDebitMaintenanceDbEntities
                                                .AsNoTracking().Where(x => x.DirectDebitId == directDebitId).ToListAsync().ConfigureAwait(false);
            return _mapper.Map<List<DirectDebitMaintenance>>(rId);
        }

        public async Task<DirectDebitMaintenance> GetDirectDebitMaintenanceByIdAsync(Guid id, Guid directdebitId)
        {
            var rId = await _directDebitContext.DirectDebitMaintenanceDbEntities
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id && x.DirectDebitId == directdebitId)
                                                .ConfigureAwait(false);
            return _mapper.Map<DirectDebitMaintenance>(rId);
        }

        public async Task UpdateAsync(Guid id, Guid directdebitId, DirectDebitMaintenance directDebitMaintenance)
        {
            if (directDebitMaintenance == null)
            {
                throw new ArgumentNullException(nameof(directDebitMaintenance));
            }
            var rId = await _directDebitContext.DirectDebitMaintenanceDbEntities
                                                .FirstOrDefaultAsync(x => x.Id == id && x.DirectDebitId == directdebitId)
                                                .ConfigureAwait(false);

            rId.NewAdditionalAmount = directDebitMaintenance.NewAdditionalAmount.Value;
            rId.NewPreferredDate = directDebitMaintenance.NewPreferredDate.Value;
            rId.PreviousAdditionalAmount = directDebitMaintenance.PreviousAdditionalAmount.Value;
            rId.PreviousPreferredDate = directDebitMaintenance.PreviousPreferredDate.Value;
            rId.Reason = directDebitMaintenance.Reason;
            rId.Status = directDebitMaintenance.Status;
            _directDebitContext.DirectDebitMaintenanceDbEntities.Update(rId);
            await _directDebitContext.SaveChangesAsync().ConfigureAwait(false);

        }
    }
}
