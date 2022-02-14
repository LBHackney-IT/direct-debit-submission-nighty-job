using System;
using System.Collections.Generic;
using System.Text;
using DirectDebitSubmissionNightyJob.Boundary.Response;

namespace DirectDebitSubmissionNightyJob.Tests.E2ETests.Fixtures
{
    public class PtxApiFixture : BaseApiFixture<Tuple<bool, ResultSummaryResponse>>
    {
        public PtxApiFixture()
        {
            Environment.SetEnvironmentVariable("PTXDDSubmissionBaseUrl", FixtureConstants.PtxApiRoute);
            Environment.SetEnvironmentVariable("PTXVerifyApiKey", FixtureConstants.PtxApiToken);
            Environment.SetEnvironmentVariable("RegisterPTXEmail", FixtureConstants.RegisterPTXEmail);
            Environment.SetEnvironmentVariable("PTXPassword", FixtureConstants.PTXPassword);
            Environment.SetEnvironmentVariable("PTXProfile", FixtureConstants.PTXProfile);
            Environment.SetEnvironmentVariable("HackneySortCode", FixtureConstants.HackneySortCode);
            Environment.SetEnvironmentVariable("HackneyAccountNumber", FixtureConstants.HackneyAccountNumber);
            Environment.SetEnvironmentVariable("HackneyAccountName", FixtureConstants.HackneyAccountName);

            Route = FixtureConstants.PtxApiRoute;
            Token = FixtureConstants.PtxApiToken;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !Disposed)
            {
                ResponseObject = null;
                base.Dispose(disposing);
            }
        }

        public bool UploadedFileSucceed()
        {
            return true;
        }
    }
}
