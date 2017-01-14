using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Services
{
    public class HttpService : IHttpService
    {
        public string Get(string url, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Post(string url, string contentType, string body)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(body, Encoding.UTF8, contentType));
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
    }
}