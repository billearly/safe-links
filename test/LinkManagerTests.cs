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
            var redirectInfo = manager.GetLinkLocation("http://bit.ly/abc123");

            mockSource.VerifyAll();

            Assert.AreEqual("http://www.example.com/redirect", redirectInfo.Location);
        }

        [TestMethod]
        public void GetLinkLocation_WithInvalidUri_ThrowsException()
        {
            var mockSource = new Mock<ILinksSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = Assert.ThrowsException<ArgumentException>(
                () => manager.GetLinkLocation("invalid")
            );

            mockSource.Verify(x => x.GetLinkLocation(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'invalid' is not a valid uri", ex.Message);
        }

        [TestMethod]
        public void GetLinkLocation_WithEmptyString_ThrowsException()
        {
            var mockSource = new Mock<ILinksSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = Assert.ThrowsException<ArgumentException>(
                () => manager.GetLinkLocation(string.Empty)
            );

            mockSource.Verify(x => x.GetLinkLocation(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'' is not a valid uri", ex.Message);
        }

        [TestMethod]
        public void GetLinkLocation_WithInvalidDomain_ThrowsException()
        {
            var mockSource = new Mock<ILinksSource>();
            var manager = new LinkManager(mockSource.Object);

            var ex = Assert.ThrowsException<ArgumentException>(
                () => manager.GetLinkLocation("http://www.example.com")
            );

            mockSource.Verify(x => x.GetLinkLocation(It.IsAny<Uri>()), Times.Never);

            Assert.AreEqual("'www.example.com' is not a known url shortener domain", ex.Message);
        }
    }
}