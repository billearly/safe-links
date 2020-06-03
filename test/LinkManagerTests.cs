using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafeLinks.Managers;
using SafeLinks.Source;

namespace SafeLinks.Test
{
    [TestClass]
    public class LinkManagerTests
    {
        [TestMethod]
        public async Task GetLinkLocation_CallsSourceWithDecodedUri()
        {
            var response = new HttpResponseMessage();
            response.Headers.Location = new Uri("http://www.example.com/redirect");

            var mockSource = new Mock<ILinkSource>();

            mockSource
                .Setup(x => x.GetLinkInfoAsync(It.Is<Uri>(uri => uri.AbsoluteUri == "http://bit.ly/abc123")))
                .Returns(Task.FromResult(response))
                .Verifiable();

            var manager = new LinkManager(mockSource.Object);
            var redirectInfo = await manager.GetLinkInfoAsync("http://bit.ly/abc123");

            mockSource.VerifyAll();

            Assert.AreEqual("http://www.example.com/redirect", redirectInfo.Location);
        }

        [TestMethod]
        public async Task GetLinkLocation_WithInvalidUri_ThrowsException()
        {
            var mockSource = new Mock<ILinkSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => manager.GetLinkInfoAsync("invalid")
            );

            mockSource.Verify(x => x.GetLinkInfoAsync(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'invalid' is not a valid uri", ex.Message);
        }

        [TestMethod]
        public async Task GetLinkLocation_WithEmptyString_ThrowsException()
        {
            var mockSource = new Mock<ILinkSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => manager.GetLinkInfoAsync(string.Empty)
            );

            mockSource.Verify(x => x.GetLinkInfoAsync(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'' is not a valid uri", ex.Message);
        }
    }
}