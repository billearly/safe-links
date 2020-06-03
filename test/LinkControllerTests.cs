using System.Threading.Tasks;
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
        public async Task GetLinkLocation_RedirectExists_ReturnsUrl()
        {
            var mockManager = new Mock<ILinkManager>();

            mockManager
                .Setup(x => x.GetLinkInfoAsync("http://www.example.com"))
                .Returns(Task.FromResult(new LinkInfo
                {
                    Location = "http://www.example.com/redirect"
                }))
                .Verifiable();

            var controller = new LinkController(mockManager.Object);
            var result = await controller.GetLinkInfo("http://www.example.com");
            var redirectInfo = result.Value as LinkInfo;

            mockManager.VerifyAll();
            Assert.AreEqual("http://www.example.com/redirect", redirectInfo.Location);
        }
    }
}
