using System;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Services
{
    public class XmlSerializer : ISerializer
    {
        public T Deserialize<T>(string obj)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}