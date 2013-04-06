using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricTests.Controllers;

namespace ElectricTests.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }
    }
}
