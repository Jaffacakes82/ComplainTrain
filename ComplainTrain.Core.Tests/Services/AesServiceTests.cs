using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Services;
using Xunit;

namespace ComplainTrain.Core.Tests.Services
{
    public class AesServiceTests
    {
        private string key = "46A2D239311B8";

        [Fact]
        public void Encrypt()
        {
            ICryptoService fooBar = new AesService();
            string result = fooBar.Encrypt("TestString", key);
        }

        [Fact]
        public void Decrypt()
        {
            ICryptoService fooBar = new AesService();
            string result = fooBar.Decrypt("K7J0X3dpNzGY2LBd/f++Slswx0VRX042jTn6EO7R5ith8ltDieJ8KfxcARl8h1Fy", key);
        }
    }
}