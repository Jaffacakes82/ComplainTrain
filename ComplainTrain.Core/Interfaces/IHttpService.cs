using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplainTrain.Core.Interfaces
{
    public interface IHttpService
    {
        string Post(string url, string contentType, string body, IDictionary<string, IEnumerable<string>> headers);
        
        string Get(string url, string contentType);
    }
}