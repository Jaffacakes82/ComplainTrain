using System;
using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Services;

namespace ComplainTrain.Core.Factories
{
    public class AesFactory : ICryptoFactory
    {
        public AesFactory()
        {
            // No dependencies yet
        }

        public ICryptoService CreateCrypto()
        {
            return new AesService();
        }

        public bool AppliesTo(Type type)
        {
            return typeof(AesService).Equals(type);
        }
    }
}