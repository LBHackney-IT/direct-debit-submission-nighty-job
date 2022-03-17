using System.Text.Json.Serialization;

namespace DirectDebitSubmissionNightyJob.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetTypeEnum
    {

        Tenant = 1,
        Leaseholder
    }
}
