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
                .Setup(x => x.GetLinkLocation(It.Is<Uri>(uri => uri.AbsoluteUri == "http://www.example.com/")))
                .Returns("http://www.example.com/redirect")
                .Verifiable();

            var manager = new LinkManager(mockSource.Object);
            var result = manager.GetLinkLocation("http://www.example.com/");

            mockSource.VerifyAll();
            Assert.AreEqual("http://www.example.com/redirect", result);
        }
    }
}