namespace ComplainTrain.Core.Interfaces
{
    public interface IHttpService
    {
        string Post(string url, string contentType, string body);
        string Get(string url, string contentType);
    }
}