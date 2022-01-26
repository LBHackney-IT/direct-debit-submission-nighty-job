using System.Text.Json.Serialization;

namespace DirectDebitSubmissionNightyJob.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DirectDebitStatusEnum
    {
        New = 1,
        Active,
        Cancelled
    }
}
