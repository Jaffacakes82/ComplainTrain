using System;
using Xunit;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Tests.Services
{
    public class NationalRailServiceTests
    {
        [Fact]
        public async void Test1() 
        {
            INationalRailService foo = new NationalRailService();
            var returnVal = await foo.GetDepartureBoard(0, "FOO", "FOO");
        }
    }
}
