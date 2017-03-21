using System.Threading.Tasks;

namespace ComplainTrain.Core.Interfaces
{
    public interface ITwitterService
    {   
        void Tweet(string message);
    }
}