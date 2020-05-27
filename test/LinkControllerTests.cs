using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafeLinks.Controllers;
using SafeLinks.Managers;
using SafeLinks.Models;

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
                .Returns(new RedirectInfo
                {
                    Location = "http://www.example.com/redirect"
                })
                .Verifiable();

            var controller = new LinkController(mockManager.Object);
            var result = controller.GetLinkLocation("http://www.example.com");
            var redirectInfo = result.Value as RedirectInfo;

            mockManager.VerifyAll();
            Assert.AreEqual("http://www.example.com/redirect", redirectInfo.Location);
        }
    }
}
