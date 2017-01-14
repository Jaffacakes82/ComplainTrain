using ComplainTrain.Core.Settings;
using ComplainTrain.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ComplainTrain.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void HomeController_GET_Index_ReturnsIndexView() 
        {
            // Act
            HomeController homeController = new HomeController();
            ViewResult result = (ViewResult)homeController.Index();
            
            // Assert
            Assert.Equal("Index", result.ViewName);
        }
    }
}
