using DirectDebitSubmissionNightyJob.Extension;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Services.Concrete
{
    /// <summary>
    /// Provides methods to communicate with REST API supporting JSON requests and responses.
    /// </summary>
    public class JsonRestClient : IRestClient
    {
        private HttpClient _httpClient;

        public void Init(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Direct Debit Submission Nighty Job");
        }

        /// <summary>
        /// Sends a GET method to specified url.
        /// </summary>
        public async Task<TResult> GetAsync<TResult>(string url, string errorMessage)
        {
            return await SubmitRequest<TResult>(url, null, errorMessage, HttpMethod.Get).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a POST method to specified url, passing a body in JSON format
        /// </summary>
        public async Task<TResult> PostAsync<TResult>(string url, object payload, string errorMessage)
        {
            return await SubmitRequest<TResult>(url, payload, errorMessage, HttpMethod.Post).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a PATCH method to specified url, passing a body in JSON format
        /// </summary>
        public async Task<TResult> PatchAsync<TResult>(string url, object payload, string errorMessage)
        {
            return await SubmitRequest<TResult>(url, payload, errorMessage, HttpMethod.Patch).ConfigureAwait(false);
        }

        private async Task<TResult> SubmitRequest<TResult>(string url, object payload, string errorMessage,
            HttpMethod method)
        {
            Debug.Assert(_httpClient != null, "Init() method must be called before making any requests");

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{_httpClient.BaseAddress}{url}")
            };

            if (payload != null)
            {
                var jsonBody = JsonConvert.SerializeObject(payload);
                httpRequestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            var httpResponse = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode && httpResponse.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                await httpResponse.ThrowResponseExceptionAsync(errorMessage).ConfigureAwait(false);
            }

            if (httpResponse.Content == null ||
                httpResponse.Content.Headers.ContentType?.MediaType != "application/json") return default;

            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(content);
        }
    }
}
