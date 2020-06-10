using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SafeLinks.Filters;
using SafeLinks.Models;

namespace SafeLinks.Test
{
    [TestClass]
    public class ExceptionFilterTests
    {
        private ExceptionContext context;

        [TestInitialize]
        public void Initialize()
        {
            var mockHttpContext = Mock.Of<HttpContext>();
            var mockRouteData = Mock.Of<RouteData>();
            var mockActionDescriptor = Mock.Of<ActionDescriptor>();
            var mockFilterMetaDataList = Mock.Of<IList<IFilterMetadata>>();

            var actionContext = new ActionContext(mockHttpContext, mockRouteData, mockActionDescriptor);
            var exceptionContext = new ExceptionContext(actionContext, mockFilterMetaDataList);

            context = exceptionContext;
        }

        [TestMethod]
        public void OnException_WithArgumentException_ReturnsResultBadRequest()
        {
            var exception = Mock.Of<ArgumentException>(
                e => e.Message == "ArgumentException message"
            );

            context.Exception = exception;

            var filter = new ExceptionFilter();
            filter.OnException(context);

            var jsonResult = context.Result as JsonResult;
            var resultObj = JsonConvert.DeserializeObject<ErrorResult>(JsonConvert.SerializeObject(jsonResult.Value));

            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual(400, resultObj.Status);
            Assert.AreEqual("ArgumentException message", resultObj.Message);
        }

        [TestMethod]
        public void OnException_WithUncheckedException_ReturnsResultInternalServerError()
        {
            var exception = Mock.Of<Exception>();

            context.Exception = exception;

            var filter = new ExceptionFilter();
            filter.OnException(context);

            var jsonResult = context.Result as JsonResult;
            var resultObj = JsonConvert.DeserializeObject<ErrorResult>(JsonConvert.SerializeObject(jsonResult.Value));


            Assert.AreEqual(500, jsonResult.StatusCode);
            Assert.AreEqual(500, resultObj.Status);
            Assert.AreEqual("An unknown error has occured", resultObj.Message);
        }
    }
}