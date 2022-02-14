using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Tests.E2ETests.Fixtures;
using DirectDebitSubmissionNightyJob.Tests.E2ETests.Steps;
using System;
using System.Collections.Generic;
using TestStack.BDDfy;
using Xunit;

namespace DirectDebitSubmissionNightyJob.Tests.E2ETests.Stories
{
    [Collection("Aws collection")]
    public class AddNewDirectDebitSubmissionDbWhenFileUploadedTest : IntegrationTest
    {
        private readonly TenureApiFixture _tenureApiFixture;
        private readonly TransactionApiFixture _transactionApiFixture;
        private readonly PtxApiFixture _ptxApiFixture;


        private readonly AddDirectDebitSubmissionDbWhenFileUploadedUseCaseStep _steps;

        public AddNewDirectDebitSubmissionDbWhenFileUploadedTest()
        {

            _tenureApiFixture = new TenureApiFixture();
            _transactionApiFixture = new TransactionApiFixture();
            _ptxApiFixture = new PtxApiFixture();
            _steps = new AddDirectDebitSubmissionDbWhenFileUploadedUseCaseStep();
        }


        [Fact]
        public void DirectDebitSubmissionCreated()
        {
            var id = Guid.NewGuid();
            var targetId = Guid.NewGuid();
            var directDebits = new List<DirectDebit>();

            this.Given(g => _ptxApiFixture.UploadedFileSucceed())
                .When(w => _steps.WhenTheFunctionIsTriggered(id))
                .Then(t => _steps.ThenSentDirectDebitSubmissionRequest())
                .Then(t => _steps.ThenTheDirectDebitSubmissionIsCreated(_steps.DirectDebitSubmission, DatabaseContext))
                .Then(t => _tenureApiFixture.GivenTheTenureExists(targetId))
                .Then(t => _transactionApiFixture.CreateTransactionRecord(directDebits))
                .BDDfy();
        }
    }
}
