using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Domain.Account;
using DirectDebitSubmissionNightyJob.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DirectDebitSubmissionNightyJob.Tests.E2ETests.Steps
{
    public class AddDirectDebitSubmissionDbWhenFileUploadedUseCaseStep : BaseSteps
    {
        public DirectDebitSubmission DirectDebitSubmission { get; private set; }

        public async Task WhenTheFunctionIsTriggered(Guid id)
        {
            await TriggerFunction(id).ConfigureAwait(false);
        }

        public DirectDebitSubmission ThenSentDirectDebitSubmissionRequest()
        {
            DirectDebitSubmission = _fixture.Build<DirectDebitSubmission>()
                .Create();

            return DirectDebitSubmission;

        }

        public async Task ThenTheDirectDebitSubmissionIsCreated(
            DirectDebitSubmission directDebitSubmission, DirectDebitContext dbContext)
        {
            var id = Guid.NewGuid();
            var dbEntity = _fixture.Build<DirectDebitSubmissionDbEntity>()
                                    .With(_ => _.Id, id)
                                    .With(_ => _.DateCreated, directDebitSubmission.DateCreated)
                                    .With(_ => _.DateOfCollection, directDebitSubmission.DateOfCollection)
                                    .With(_ => _.DirectDebits, directDebitSubmission.DirectDebits)
                                    .With(_ => _.FileName, directDebitSubmission.FileName)
                                    .With(_ => _.Status, directDebitSubmission.Status)
                                    .With(_ => _.PTXSubmissionResponse, directDebitSubmission.PTXSubmissionResponse)
                                    .Create();
            await dbContext.DirectDebitSubmissionDbEntities.AddRangeAsync(dbEntity).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);


            var entityInDb = await dbContext.DirectDebitSubmissionDbEntities.SingleOrDefaultAsync(x => x.Id == id);
            entityInDb.Should().NotBeNull();
            entityInDb.Id.Should().Be(id);
        }
    }
}
