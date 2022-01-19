using Hackney.Core.Testing.Shared;
using Xunit;

namespace DirectDebitSubmissionNightyJob.Tests
{
    [CollectionDefinition("LogCall collection")]
    public class LogCallAspectFixtureCollection : ICollectionFixture<LogCallAspectFixture>
    { }
}
