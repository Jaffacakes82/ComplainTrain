using System;
using System.IO;
using System.Xml.Serialization;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Services
{
    public class XmlSerializationService : ISerializer
    {
        public T Deserialize<T>(string obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(obj))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}