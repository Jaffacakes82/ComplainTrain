namespace ComplainTrain.Core.Interfaces
{
    public interface ICryptoService
    {
        string Encrypt(string toEncrypt, string key);
        string Decrypt(string toDecrypt, string key);
    }
}