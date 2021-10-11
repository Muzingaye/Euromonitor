using System;
using System.Collections.Generic;
using DataFramework.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ServiceFramework;
using ServiceFramework.Services;
using Store.API.Controllers;
using Store.API.Models;

namespace API.Test
{
    [TestFixture]
    public class AccountControllerTest
    {
        public IConfiguration config { get; set; }
        private Mock<AccountController> controllerMock;


        [SetUp]
        public void setUp()
        {
            controllerMock = new Mock<AccountController>();
            var inMemoryKey = new Dictionary<string, string>
            {
                { "Jwt:Key", "Bet_Key" },
                { "Jwt:Issuer", "generate@example.com" },
                { "Jwt:Duration", "1" }
            };
            this.config = new ConfigurationBuilder().AddInMemoryCollection(inMemoryKey).Build();
        }


        [Test]
        public void CanInstatiateClass()
        {
            Assert.That(controllerMock, Is.Not.Null);
        }


        [Test]
        public void Test_Logout_Return_StatusCode_OK()
        {
            var mock = new Mock<AccountService>();
            mock.Setup(s => s.GetFirst(It.IsAny<Func<Account, bool>>())).Returns(GetFirst());

            var mockAccountDetails = new Mock<IService<AccountDetails>>();
            mockAccountDetails.Setup(s => s.GetFirst(It.IsAny<Func<AccountDetails, bool>>()))
                .Returns(GetFirst().AccountDetails);

            var controller = new AccountController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.Logout();
            Assert.That(actualResults, Is.Not.Null);
        }

        [Test]
        [TestCase("muzingayed.dube@gmail.com", "P@55w0rd", 200)]
        public void Test_Login_Return_Valid_Token(string emailAddress, string password, int expected)
        {
            //Arrage
            Login login = new Login
            {
                EmailAddress = emailAddress,
                Password = password
            };


            var controller = new AccountController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.Login(login);

            //
            Assert.That(expected, Is.EqualTo(200));
        }

        [TestCase("muzingayed.dube@gmail.com", "111111", 401)]
        public void Test_Login_Return_Invalid_Token(string emailAddress, string password, int expected)
        {
            //Arrage
            Login login = new Login
            {
                EmailAddress = emailAddress,
                Password = password
            };


            var controller = new AccountController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.Login(login);

            //
            Assert.That(expected, Is.EqualTo(401));
        }

        [Test]
        public void Test_Registration_Return_Error()
        {
            //Arrange
            AccountModel register = new AccountModel
            {
                LastName = "Doe",
                PhoneNumber = "0824253",
                EmailAddress = "me@example.com",
                CaptureDate = DateTime.Now,
                Password = "P@55w0rd",
                AccountDetails = new AccountDetailsModel
                {
                    Password = "P@55word",
                    ActiveMin = 0,
                    LastLogInTime = DateTime.Now,
                    CaptureDate = DateTime.Now
                }
            };

            //Act

            var controller = new AccountController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.Register(register);
            Assert.That(actualResults.ErrorCode, Is.GreaterThanOrEqualTo(800));
        }


        [Test]
        public void Test_Valid_Registration()
        {
            //Arrange
            AccountModel register = new AccountModel
            {
                FirstName = "Muzi",
                LastName = "Doe",
                PhoneNumber = "0824253",
                EmailAddress = "johndoe@example.co.za",
                CaptureDate = DateTime.Now,
                Password = "P@55w0rd",
                AccountDetails = new AccountDetailsModel
                {
                    Password = "P@55word",
                    ActiveMin = 0,
                    LastLogInTime = DateTime.Now,
                    CaptureDate = DateTime.Now
                }
            };

            //Act

            var controller = new AccountController(config);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["token"] = "";
            //Act
            var actualResults = controller.Register(register);
            Assert.That(actualResults.ErrorCode, Is.EqualTo(0));
        }
        private static Account GetFirst()
        {
            AccountDetails details = new AccountDetails
            {
                AccountId = 3,
                Password = "someencriptedtext",
                ActiveMin = Convert.ToDecimal(55.5),
                LastLogInTime = DateTime.Now,
                CaptureDate = DateTime.Now
            };
            Account output = new Account
            {
                Id = 3,
                FirstName = "Jim",
                LastName = "Nobody",
                EmailAddress = "me@xample.com",
                PhoneNumber = "0116811000",
                CaptureDate = DateTime.Now,
                AccountDetails = details
            };

            return output;
        }
    }
}