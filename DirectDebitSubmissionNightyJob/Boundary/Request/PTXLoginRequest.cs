using DirectDebitApi.V1.Boundary.Response;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class PTXLoginRequest
    {
        [JsonPropertyName("loginTokens")]
        public List<LoginToken> LoginTokens { get; set; }
        [JsonPropertyName("apiVersion")]
        public string ApiVersion { get; set; }

        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }

        [JsonPropertyName("tokenLocation")]
        public string TokenLocation { get; set; }
    }
    public class LoginToken
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }


}
