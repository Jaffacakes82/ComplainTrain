using System.Collections.Generic;

namespace ComplainTrain.Core.Interfaces
{
    public interface IOAuthHelper
    {
        KeyValuePair<string, string> GetOAuthHeader(string message);
    }
}