using System.Threading.Tasks;

namespace ComplainTrain.Core.Interfaces
{
    public interface IHttpService
    {
        Task<string> Post(string url, string contentType, string body);
        string Get(string url, string contentType);
    }
}