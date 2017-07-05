using System.Net.Http;
using System.Threading.Tasks;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PatchAsync(
        this HttpClient client,
        string url,
        HttpContent content)
   {
       var method = new HttpMethod("PATCH");
       var request = new HttpRequestMessage(method, url)
       {
           Content = content
       };

       return await client.SendAsync(request);
   }
}