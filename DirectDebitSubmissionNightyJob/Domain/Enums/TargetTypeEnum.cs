using System.Text.Json.Serialization;

namespace DirectDebitApi.V1.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetTypeEnum
    {

        Tenant = 1,
        Lease,
        Major
    }
}
