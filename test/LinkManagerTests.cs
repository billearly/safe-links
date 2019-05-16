using System;
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
        public void GetLinkLocation_CallsSourceWithDecodedUri()
        {
            var mockSource = new Mock<ILinksSource>();

            mockSource
                .Setup(x => x.GetLinkLocation(It.Is<Uri>(uri => uri.AbsoluteUri == "http://bit.ly/abc123")))
                .Returns("http://www.example.com/redirect")
                .Verifiable();

            var manager = new LinkManager(mockSource.Object);
            var result = manager.GetLinkLocation("http://bit.ly/abc123");

            mockSource.VerifyAll();
            Assert.AreEqual("http://www.example.com/redirect", result);
        }

        [TestMethod]
        public void GetLinkLocation_WithInvalidUri_ReturnsNull()
        {
            var mockSource = new Mock<ILinksSource>();
            var manager = new LinkManager(mockSource.Object);

            var result = manager.GetLinkLocation("invalid");

            mockSource.Verify(x => x.GetLinkLocation(It.IsAny<Uri>()), Times.Never);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetLinkLocation_WithInvalidDomain_ReturnsNull()
        {
            var mockSource = new Mock<ILinksSource>();
            var manager = new LinkManager(mockSource.Object);

            var result = manager.GetLinkLocation("http://www.example.com");

            mockSource.Verify(x => x.GetLinkLocation(It.IsAny<Uri>()), Times.Never);
            Assert.IsNull(result);
        }
    }
}