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
        public static async Task<HttpClient> DoWork(string baseUrl, string username, string password)
        {
            // Logs in with a user name and passwod and then returns an authenticated client
            var client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };

            string queryString;
            var query = new Dictionary<string, string>
            {
                ["username"] = username,
                ["password"] = password
            };

            using (var content = new FormUrlEncodedContent(query))
            {
                queryString = await content.ReadAsStringAsync();
            }

            var response = await client.GetAsync($"api/v1/authenticate?{queryString}");

            var token = await response.Content.ReadAsStringAsync();
            var authInfo = username + ":" + token.Replace("\"", string.Empty);
            var authKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authKey);

            return client;
        }
    }
}