using System;
using System.Linq;
using ComplainTrain.Core.Factories;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Strategies
{
    public class CryptoStrategy : ICryptoStrategy
    {
        private readonly ICryptoFactory[] cryptoFactories = new ICryptoFactory[] 
        { 
            new AesFactory(), 
            new HmacSha1Factory() 
        };

        public ICryptoService CreateCrypto(Type type)
        {
            var cryptoFactory = this.cryptoFactories
                .FirstOrDefault(factory => factory.AppliesTo(type));

            if (cryptoFactory == null)
            {
                throw new Exception("type not registered");
            }

            return cryptoFactory.CreateCrypto();
        }
    }
}