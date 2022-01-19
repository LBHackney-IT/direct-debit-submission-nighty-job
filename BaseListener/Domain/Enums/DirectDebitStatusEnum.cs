using System.Text.Json.Serialization;

namespace DirectDebitApi.V1.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DirectDebitStatusEnum
    {
        New = 1,
        Active,
        Cancelled
    }
}
