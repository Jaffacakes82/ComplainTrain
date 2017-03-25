using System;

namespace ComplainTrain.Core.Interfaces
{
    public interface ICryptoFactory
    {
        ICryptoService CreateCrypto();
        bool AppliesTo(Type type);
    }
}