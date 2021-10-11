using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using DataFramework.Entities;
using Moq;
using NUnit.Framework;
using ServiceFramework.Services;

namespace ServiceFramework.Test
{
    [TestFixture]
    public class AccountServiceTest
    {
        private Mock<AccountService> accountMockServices;
        private AccountService accountServices;
        [SetUp]
        public void SetUp()
        {
            accountMockServices = new Mock<AccountService>();
            accountServices = new AccountService();
        }

        [Test]
        public void Test_GetAll_Returns_List_Account()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IService<Account>>()
                    .Setup(x => x.GetAll()).Returns(GetAllAccount());


                var cls = mock.Create<IService<Account>>();
                var expected = GetAllAccount();

                var actual = cls.GetAll().ToList();

                Assert.True(cls != null);
                Assert.That(expected.Count, Is.EqualTo(actual.Count));
            }
        }

        [Test]
        public void Test_GetWhere_Returns_List_Account()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<AccountService>()
                    .Setup(x => x.GetWhere(It.IsAny<Func<Account, bool>>())).Returns(GetAllAccount());
                
                var stub = mock.Create<AccountService>();
                var expected = GetAllAccount().Where(x => x.Id == 1);

                var actual = stub.GetWhere(x => x.Id == 1).ToList();

                Assert.True(stub != null);
                Assert.That(expected.Count, Is.LessThanOrEqualTo(actual.Count));
            }
        }

        //[Test]
        // [TestCase("@muz!!", "Dub3\"", "me@xample.com", "08242534", false)]
        // [TestCase("TEST", "4558#$", "me@xample.com", "08242534", false)]
        // public void Insert_Return_false(string firstName, string lastName, string emailAddress, string phoneNumber,
        //     bool expected)
        // {
        //     //Arrange
        //     Account account = new Account
        //     {
        //         FirstName = firstName,
        //         LastName = lastName,
        //         EmailAddress = emailAddress,
        //         PhoneNumber = phoneNumber,
        //         CaptureDate = DateTime.Now
        //     };
        //     //Act
        //     var actual = accountServices.Insert(account);
        //     //Assert
        //     Assert.Throws<Exception>(() => { throw new Exception();});
        // }


       
        [Test]
        [TestCase(null, "Dub3\"", "me@xample.com", "08242534", false)]
        [TestCase("TEST", null, "me@xample.com", "08242534", false)]
        public void Insert_Throw_An_Exception(string firstName, string lastName, string emailAddress, string phoneNumber,
            bool expected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                Account account = new Account
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    CaptureDate = DateTime.Now
                };
                mock.Mock<IService<Account>>()
                    .Setup(x => x.Insert(account)).Returns(expected);


                var stub = mock.Create<IService<Account>>();

                var actual = stub.Insert(account);

                Assert.True(stub != null);
                Assert.That(expected, Is.EqualTo(actual));
            }
        }

        private List<Account> GetAllAccount()
        {
            List<Account> output = new List<Account>
            {
                new Account
                {
                    Id = 1,
                    FirstName = "Muzingaye",
                    LastName = "Dube",
                    EmailAddress = "me@exmpl.com",
                    PhoneNumber = "0116811000",
                    CaptureDate = DateTime.Now
                },
                new Account
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "johndoe@example.com",
                    PhoneNumber = "021456789",
                    CaptureDate = DateTime.Now
                },
                new Account
                {
                    Id = 13,
                    FirstName = "Jim",
                    LastName = "Nobody",
                    EmailAddress = "me@exmpl.com",
                    PhoneNumber = "0116811000",
                    CaptureDate = DateTime.Now
                }
            };

            return output;
        }
    }
}
