using System;
using System.Collections.Generic;
using DataFramework.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Store.API.Controllers;

namespace API.Test
{
    [TestFixture]
    public class BookController_Test
    {
        public IConfiguration config { get; set; }
        private Mock<BookController> controllerMock;


        [SetUp]
        public void setUp()
        {
            controllerMock = new Mock<BookController>();
            var inMemoryKey = new Dictionary<string, string>
            {
                { "Jwt:Key", "Bet_Key" },
                { "Jwt:Issuer", "generate@example.com" },
                { "Jwt:Duration", "1" }
            };
            this.config = new ConfigurationBuilder().AddInMemoryCollection(inMemoryKey).Build();
        }

        [Test]
        public void Test_Get_All_avaibleBook_Return_List_OfBook()
        {
            var controller = new BookController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.GetBooks();
            //Assert
            Assert.That(actualResults, Is.Not.Null);
            Assert.That(actualResults.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void Test_SubscribeToBook_Return_Success()
        {
            var controller = new BookController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.SubscribeToBook(subscriptions);
            //Assert
            Assert.That(actualResults, Is.Not.Null);
            Assert.That(actualResults.ErrorCode, Is.EqualTo(0));
        }
        [Test]
        public void Test_SubscribeToBook_Return_Error()
        {
            var controller = new BookController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.SubscribeToBook(subscriptions);
            //Assert
            Assert.That(actualResults, Is.Not.Null);
            Assert.That(actualResults.ErrorCode, Is.EqualTo(800));
        }


        [Test]
        public void Test_UnSubscribeToBook_Return_Error()
        {
            var controller = new BookController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.UnSubscribe(subscriptions);
            //Assert
            Assert.That(actualResults, Is.Not.Null);
            Assert.That(actualResults.ErrorCode, Is.EqualTo(800));
        }

        List<Subscription> subscriptions = new List<Subscription>()
        {
            new Subscription
            {
                Name = "How to Kill A Mocking Bird",
                CaptureDate = DateTime.Now
            },
            new Subscription
            {
                Name = "48 Laws of Success",
                CaptureDate = DateTime.Now,
            }
        };
    }
}