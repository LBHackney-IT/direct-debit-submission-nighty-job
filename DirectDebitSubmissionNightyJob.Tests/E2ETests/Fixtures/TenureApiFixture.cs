using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using DirectDebitSubmissionNightyJob.Boundary.Response;

namespace DirectDebitSubmissionNightyJob.Tests.E2ETests.Fixtures
{
    public class TenureApiFixture : BaseApiFixture<TenureResponse>
    {
        public TenureApiFixture()
        {
            Environment.SetEnvironmentVariable("TenureApiUrl", FixtureConstants.TenureApiRoute);
            Environment.SetEnvironmentVariable("TenureApiToken", FixtureConstants.TenureApiToken);

            Route = FixtureConstants.TenureApiRoute;
            Token = FixtureConstants.TenureApiToken;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !Disposed)
            {
                ResponseObject = null;
                base.Dispose(disposing);
            }
        }

        public void GivenTheTenureDoesNotExist(Guid id)
        {

        }

        public TenureResponse GivenTheTenureExists(Guid id)
        {
            ResponseObject = Fixture.Build<TenureResponse>()
                .With(x => x.Id, id.ToString())
                .With(x => x.PaymentReference, "12345test")
                .With(x => x.EndOfTenureDate, DateTime.UtcNow.AddYears(6))
                .Create();


            return ResponseObject;
        }
    }
}
