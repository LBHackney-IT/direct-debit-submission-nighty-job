using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Helpers
{
    public static class HttpClientHelper
    {
        public static string GetCookieValueFromResponse(this HttpResponseMessage httpResponse, string cookieName)
        {

            foreach (var cookieStr in httpResponse.Headers.GetValues("Set-Cookie"))
            {
                if (string.IsNullOrEmpty(cookieStr))
                    continue;

                var array = cookieStr.Split(';')
                    .Where(x => x.Contains('=')).Select(x => x.Trim());

                var dict = array.Select(item => item.Split(new[] { '=' }, 2)).ToDictionary(s => s[0], s => s[1]);


                if (dict.ContainsKey(cookieName))
                    return dict[cookieName];
            }

            return null;
        }
    }
}
