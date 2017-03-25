using System;
using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Services;

namespace ComplainTrain.Core.Factories
{
    public class HmacSha1Factory : ICryptoFactory
    {
        public HmacSha1Factory()
        {
            // No dependencies yet
        }

        public ICryptoService CreateCrypto()
        {
            return new HmacSha1Service();
        }

        public bool AppliesTo(Type type)
        {
            return typeof(HmacSha1Service).Equals(type);
        }
    }
}