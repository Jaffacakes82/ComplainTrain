using System;

namespace ComplainTrain.Core.Interfaces
{
    public interface ICryptoStrategy
    {
        ICryptoService CreateCrypto(Type type);
    }
}