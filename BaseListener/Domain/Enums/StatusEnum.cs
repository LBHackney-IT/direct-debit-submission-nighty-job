using System.Text.Json.Serialization;

namespace DirectDebitApi.V1.Domain.Enums
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
