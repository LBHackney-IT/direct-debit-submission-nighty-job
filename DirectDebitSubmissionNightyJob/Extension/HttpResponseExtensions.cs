using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Exceptions;
using DirectDebitSubmissionNightyJob.Exceptions.Models;
using Newtonsoft.Json;

namespace DirectDebitSubmissionNightyJob.Extension
{
    public static class HttpResponseExtensions
    {
        public static async Task ThrowResponseExceptionAsync(this HttpResponseMessage httpResponse, string defaultErrorMessage)
        {
            var err = JsonConvert.DeserializeObject<ApiError>(await httpResponse.Content.ReadAsStringAsync());
            if (string.IsNullOrEmpty(err?.Message))
            {
                throw new ApiException(defaultErrorMessage, (int) httpResponse.StatusCode);
            }
            throw new ApiException(err?.Message, (int) httpResponse.StatusCode, err?.Errors, err?.Detail);
        }
    }
}
