using DirectDebitApi.V1.Gateways.Interfaces;
using DirectDebitApi.V1.Helpers;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Boundary.Response;
using LazyCache;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public class PTXPaymentApiService : IPTXPaymentApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IAppCache _lazyCache = new CachingService();
        private readonly string _paymentsBaseUrl;
        private const string PTXAuthCacheKey = "PTXAuthCacheKey";
        private readonly string _email;
        private readonly string _password;
        private readonly string _profileId;

        private const string Email = "RegisterPTXEmail";
        private const string Password = "PTXPassword";
        private const string ProfileId = "PTXProfile";

        public PTXPaymentApiService(IHttpClientFactory httpClientFactory, IAppCache cache, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C#/9");
            _lazyCache = cache;

            _paymentsBaseUrl = Environment.GetEnvironmentVariable("PTXDDSubmissionBaseUrl");
            if (string.IsNullOrEmpty(_paymentsBaseUrl) || !Uri.IsWellFormedUriString(_paymentsBaseUrl, UriKind.Absolute))
                throw new ArgumentException($"Configuration does not contain a setting value for the key {_paymentsBaseUrl}.");

            _email = configuration.GetValue<string>(Email);
            if (string.IsNullOrEmpty(_email))
            {
                throw new ArgumentException($"Configuration does not contain a ptx setting value for the parameter {Email}.");
            }
            _password = configuration.GetValue<string>(Password);
            if (string.IsNullOrEmpty(_password))
            {
                throw new ArgumentException($"Configuration does not contain a ptx setting value for the parameter {Password}.");
            }
            _profileId = configuration.GetValue<string>(ProfileId);
            if (string.IsNullOrEmpty(_profileId))
            {
                throw new ArgumentException($"Configuration does not contain a ptx setting value for the parameter {ProfileId}.");
            }
            _email = "eddycase1@gmail.com";
            _password = "wL7efk73cEGjZDv";
            _profileId = "44292";
        }

        public async Task<Tuple<bool, ResultSummaryResponse>> SubmitDirectDebitFile(byte[] bytes, string fileName)
        {
            var authData = _lazyCache.Get<PTXAuthData>(PTXAuthCacheKey);
            if (authData is null)
            {
                var loginRequest = await HandShakeAsync().ConfigureAwait(false);

                authData = await AuthenticateAsync(loginRequest.Item1, loginRequest.Item2).ConfigureAwait(false);
            }

            if (string.IsNullOrEmpty(authData.JsessionId))
                throw new ArgumentException($"Unable to fetch the parameter {authData.JsessionId}.");

            if (string.IsNullOrEmpty(authData.XCsrf))
                throw new ArgumentException($"Unable to fetch the parameter {authData.XCsrf}.");

            if (string.IsNullOrEmpty(authData.Authtoken))
                throw new ArgumentException($"Unable to fetch the parameter {authData.Authtoken}.");


            using var multiContent = new MultipartFormDataContent();
            //Add the file as a byte array
            using var byteContent = new ByteArrayContent(bytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            multiContent.Add(byteContent, name: "file", fileName: fileName);
            _httpClient.DefaultRequestHeaders.Remove("Cookie");
            _httpClient.DefaultRequestHeaders.Remove("X-CSRF");
            _httpClient.DefaultRequestHeaders.Add("Cookie", authData.JsessionId);
            _httpClient.DefaultRequestHeaders.Add("X-CSRF", authData.XCsrf);
            _httpClient.DefaultRequestHeaders.Add("com.bottomline.auth.token", authData.Authtoken);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", $"multipart/form-data,boundary=----{Guid.NewGuid()}");
            var path = new Uri($"{_paymentsBaseUrl}file/upload/{_profileId}", UriKind.Absolute);
            var response = await _httpClient.PostAsync(path, multiContent).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var contentResult = JsonSerializer.Deserialize<UploadedFileResponse>(body);
                var id = contentResult.Pollurl.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
                var confirmData = await GetResultSummaryByFileIdAsync(id, authData).ConfigureAwait(false);
                var status = confirmData.Status == "SUCCESS";
                return new Tuple<bool, ResultSummaryResponse>(status, confirmData);
            }

            return new Tuple<bool, ResultSummaryResponse>(false, null); ;
        }
        private async Task<Tuple<PTXLoginRequest, PTXAuthData>> HandShakeAsync()
        {

            var path = new Uri($"{_paymentsBaseUrl}security/handshake", UriKind.Absolute);
            var response = await _httpClient.GetAsync(path).ConfigureAwait(false);
            var csrfToken = response.Headers.GetValues("X-CSRF").FirstOrDefault();
            if (string.IsNullOrEmpty(csrfToken))
                throw new ArgumentException($"Unable to fetch the parameter {csrfToken}.");

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new Exception("The request was not successful...");
            var jsessionId = response.GetCookieValueFromResponse("JSESSIONID");
            var returnValue = JsonSerializer.Deserialize<PTXResponseModel>(body);
            var loginTokens = new List<LoginToken>()
            {
                new LoginToken {Key = returnValue.LoginFields[1].Key,Value= _email},
                new LoginToken {Key = returnValue.LoginFields[0].Key,Value= _password}
            };
            var apiVersion = returnValue.ApiSpecification.Versions.FirstOrDefault();

            var version = JsonSerializer.Serialize(apiVersion);
            var loginRequest = new PTXLoginRequest { LoginTokens = loginTokens, ApiVersion = version, Purpose = "cpay-auth", TokenLocation = "HEADER" };
            var authData = new PTXAuthData { JsessionId = jsessionId, XCsrf = csrfToken };

            return new Tuple<PTXLoginRequest, PTXAuthData>(loginRequest, authData);
        }

        private async Task<PTXAuthData> AuthenticateAsync(PTXLoginRequest model, PTXAuthData authData)
        {
            if (string.IsNullOrEmpty(authData.JsessionId))
                throw new ArgumentException($"Unable to fetch the parameter {authData.JsessionId}.");

            if (string.IsNullOrEmpty(authData.XCsrf))
                throw new ArgumentException($"Unable to fetch the parameter {authData.XCsrf}.");

            var jsessionId = $"JSESSIONID={authData.JsessionId}";
            var path = new Uri($"{_paymentsBaseUrl}security/login", UriKind.Absolute);
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var json = JsonSerializer.Serialize(model, serializeOptions);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Cookie", jsessionId);
            _httpClient.DefaultRequestHeaders.Add("X-CSRF", authData.XCsrf);
            var response = await _httpClient.PostAsync(path, stringContent).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var ptxAuthData = new PTXAuthData
                {
                    JsessionId = jsessionId,
                    XCsrf = response.Headers.GetValues("X-CSRF").FirstOrDefault(),
                    Authtoken = response.Headers.GetValues("com.bottomline.auth.token").FirstOrDefault()
                };
                _lazyCache.Add(PTXAuthCacheKey, ptxAuthData, DateTimeOffset.UtcNow.AddMinutes(5));

                return ptxAuthData;
            }
            throw new Exception("PTX Authentication failed");
        }

        public async Task<ResultSummaryResponse> GetResultSummaryByFileIdAsync(string id, PTXAuthData authData)
        {

            _httpClient.DefaultRequestHeaders.Remove("Cookie");
            _httpClient.DefaultRequestHeaders.Remove("X-CSRF");
            _httpClient.DefaultRequestHeaders.Remove("com.bottomline.auth.token");
            _httpClient.DefaultRequestHeaders.Add("Cookie", authData.JsessionId);
            _httpClient.DefaultRequestHeaders.Add("X-CSRF", authData.XCsrf);
            _httpClient.DefaultRequestHeaders.Add("com.bottomline.auth.token", authData.Authtoken);
            var path = new Uri($"{_paymentsBaseUrl}file/{id}", UriKind.Absolute);
            var response = await _httpClient.GetAsync(path).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadFromJsonAsync<ResultSummaryResponse>().ConfigureAwait(false);
                return contentResult;
            }
            return null;
        }
    }
}
