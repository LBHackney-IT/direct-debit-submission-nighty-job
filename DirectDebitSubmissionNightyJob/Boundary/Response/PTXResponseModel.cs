using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DirectDebitSubmissionNightyJob.Boundary.Response
{
    public class PTXResponseModel
    {
        [JsonPropertyName("apiSpecification")]
        public ApiSpecification ApiSpecification { get; set; }

        [JsonPropertyName("loginFields")]
        public List<LoginField> LoginFields { get; set; }
    }
    [Serializable]
    public class Version
    {
        [JsonPropertyName("major")]
        public string Major { get; set; }

        [JsonPropertyName("minor")]
        public string Minor { get; set; }

        [JsonPropertyName("patch")]
        public string Patch { get; set; }

        [JsonPropertyName("build")]
        public string Build { get; set; }
        public override string ToString()
        {

            return base.ToString();
        }
    }

    public class ApiSpecification
    {
        [JsonPropertyName("versions")]
        public List<Version> Versions { get; set; }
        //return //JsonSerializer.ToString<WeatherForecast>(value);
    }

    public class LoginField
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("maskedField")]
        public bool MaskedField { get; set; }
    }

    public class FailResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class UploadedFileResponse
    {
        [JsonPropertyName("version")]
        public object Version { get; set; }

        [JsonPropertyName("pollUrl")]
        public string Pollurl { get; set; }

        [JsonPropertyName("extensions")]
        public List<object> Extensions { get; set; }
    }
    public class ResultSummaryResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("submissionId")]
        public int SubmissionId { get; set; }

        [JsonPropertyName("applicationId")]
        public int ApplicationId { get; set; }

        [JsonPropertyName("batchId")]
        public int BatchId { get; set; }

        [JsonPropertyName("estimatedProcessingTime")]
        public int EstimatedProcessingTime { get; set; }

        [JsonPropertyName("processingStartTime")]
        public DateTime ProcessingStartTime { get; set; }
    }
}
