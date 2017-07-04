using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class MakeAuthenticatedClient
    {
        public static async Task<IReportsLibrary> DoWork(string baseUrl, string username, string password)
        {
            var httpClient = new HttpClient();
            var iReportsLibrary = new IReportsLibrary(baseUrl, httpClient);

            var token = await iReportsLibrary.GetAuthenticationTokenAsync(username, password);
            
            var authInfo = username + ":" + token;
            var authKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authKey);

            return iReportsLibrary;
        }
    }
}