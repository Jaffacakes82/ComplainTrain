using TrainComplain.Core.Settings;
using TrainComplain.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace TrainComplain.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IOptions<MessageSettings>> messageSettings;

        public HomeControllerTests()
        {
            this.messageSettings = new Mock<IOptions<MessageSettings>>();
        }

        [Fact]
        public void HomeController_GET_Index_ReturnsIndexView() 
        {
            // Arrange
            MessageSettings testSettings = new MessageSettings { MessageOfTheDay = "Test" };
            this.messageSettings.SetupGet(msg => msg.Value).Returns(testSettings);

            // Act
            HomeController homeController = new HomeController(this.messageSettings.Object);
            ViewResult result = (ViewResult)homeController.Index();
            
            // Assert
            Assert.Equal("Index", result.ViewName);
        }
    }
}
