using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Services
{
    public sealed class HttpService : IHttpService
    {
        public string Get(string url, string contentType)
        {
            throw new NotImplementedException();
        }

        public string Post(string url, string contentType, string body, IDictionary<string, IEnumerable<string>> headers)
        {
            using (var httpClient = new HttpClient())
            {
                if (headers != null)
                {   
                    foreach (var header in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(body, Encoding.UTF8, contentType)).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }
    }
}