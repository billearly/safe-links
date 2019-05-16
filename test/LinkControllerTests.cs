using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafeLinks.Controllers;
using SafeLinks.Managers;

namespace SafeLinks.Test
{
    [TestClass]
    public class LinkControllerTests
    {
        [TestMethod]
        public void GetLinkLocation_RedirectExists_ReturnsUrl()
        {
            var mockManager = new Mock<ILinkManager>();

            mockManager
                .Setup(x => x.GetLinkLocation("http://www.example.com"))
                .Returns("http://www.example.com/redirect")
                .Verifiable();

            var controller = new LinkController(mockManager.Object);
            var result = controller.GetLinkLocation("http://www.example.com");

            mockManager.VerifyAll();
            Assert.AreEqual("http://www.example.com/redirect", result.Value);
        }

        [TestMethod]
        public void GetLinkLocation_RedirectDoesNotExist_Returns400()
        {
            var mockManager = new Mock<ILinkManager>();

            mockManager
                .Setup(x => x.GetLinkLocation("http://www.example.com"))
                .Returns<string>(null)
                .Verifiable();

            var controller = new LinkController(mockManager.Object);
            var result = controller.GetLinkLocation("http://www.example.com");

            mockManager.VerifyAll();
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }
    }
}
