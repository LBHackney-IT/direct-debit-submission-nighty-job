using System;
using System.Collections.Generic;
using System.Text;
using DirectDebitSubmissionNightyJob.Domain;

namespace DirectDebitSubmissionNightyJob.Tests.E2ETests.Fixtures
{
    public class TransactionApiFixture : BaseApiFixture<object>
    {
        public TransactionApiFixture()
        {
            Environment.SetEnvironmentVariable("TransactionApiUrl", FixtureConstants.TransactionsApiRoute);
            Environment.SetEnvironmentVariable("TransactionApiToken", FixtureConstants.TransactionsApiToken);

            Route = FixtureConstants.TransactionsApiRoute;
            Token = FixtureConstants.TransactionsApiToken;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && !Disposed)
            {
                ResponseObject = null;
                base.Dispose(disposing);
            }
        }

        public void CreateTransactionRecord(List<DirectDebit> directDebits)
        {

        }
    }
}
