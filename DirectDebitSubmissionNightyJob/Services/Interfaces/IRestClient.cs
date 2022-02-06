using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Services.Interfaces
{
    /// <summary>
    /// Provides methods to communicate with REST API
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Allows caller to do extra configuration for a given HttpClient instance.
        /// </summary>
        public void Init(HttpClient httpClient);

        /// <summary>
        /// Sends a GET request to specified url
        /// </summary>
        Task<TResult> GetAsync<TResult>(string url, string errorMessage);

        /// <summary>
        /// Sends a POST method with payload in request body to specified url
        /// </summary>
        Task<TResult> PostAsync<TResult>(string url, object payload, string errorMessage);

        /// <summary>
        /// Sends a PATCH request with payload in request body  to specified url.
        /// </summary>
        Task<TResult> PatchAsync<TResult>(string url, object payload, string errorMessage);
    }
}
