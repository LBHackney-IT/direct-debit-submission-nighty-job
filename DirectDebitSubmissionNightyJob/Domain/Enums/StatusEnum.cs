using System.Text.Json.Serialization;

namespace DirectDebitSubmissionNightyJob.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Applied = 1,
        Pending,
        Cancelled,
        Paused
    }
}
