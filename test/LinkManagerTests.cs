using System;
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
            var mockSource = new Mock<ILinkSource>();

            mockSource
                .Setup(x => x.GetLinkLocationAsync(It.Is<Uri>(uri => uri.AbsoluteUri == "http://bit.ly/abc123")))
                .Returns(Task.FromResult("http://www.example.com/redirect"))
                .Verifiable();

            var manager = new LinkManager(mockSource.Object);
            var redirectInfo = await manager.GetLinkLocationAsync("http://bit.ly/abc123");

            mockSource.VerifyAll();

            Assert.AreEqual("http://www.example.com/redirect", redirectInfo.Location);
        }

        [TestMethod]
        public async Task GetLinkLocation_WithInvalidUri_ThrowsException()
        {
            var mockSource = new Mock<ILinkSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => manager.GetLinkLocationAsync("invalid")
            );

            mockSource.Verify(x => x.GetLinkLocationAsync(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'invalid' is not a valid uri", ex.Message);
        }

        [TestMethod]
        public async Task GetLinkLocation_WithEmptyString_ThrowsException()
        {
            var mockSource = new Mock<ILinkSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => manager.GetLinkLocationAsync(string.Empty)
            );

            mockSource.Verify(x => x.GetLinkLocationAsync(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'' is not a valid uri", ex.Message);
        }
    }
}