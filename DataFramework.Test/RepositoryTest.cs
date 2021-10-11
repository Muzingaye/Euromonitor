using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using DataFramework.Entities;
using DataFramework.Interface;
using DataFramework.Repository;
using Moq;
using NUnit.Framework;

namespace DataFramework.Test
{
    [TestFixture]
    public class RepositoryTest
    {
        private IRepository<Account> accountRepository;


        [SetUp]
        public void setUp()
        {
            accountRepository = new AccountRepository("Data Source=.\\SQLExpress;Initial Catalog=BetDb;Integrated Security=True");
        }


        [Test]
        public void CanInstantiateClass()
        {
            Assert.That(accountRepository, Is.Not.Null);
        }

        [Test]
        public void CheckIfCorrectMethodCalled()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Account>>()
                    .Setup(x => x.GetAll()).Returns(GetAllAccount());

                var cls = mock.Create<IRepository<Account>>();
                var expected = GetAllAccount();

                List<Account> actual = cls.GetAll().ToList();

                Assert.True(cls != null);
                Assert.That(expected.Count, Is.EqualTo(actual.Count));
            }
        }

        [Test]
        [TestCase("Muzi", "Dube", "me@example.com", "0853514622", true)]
        [TestCase("Testing", "Testing", "me@example.com", "0824253417", true)]
        public void Insert_Return_True(string firstName, string lastName, string emailAddress, string phoneNumber,
            bool expected)
        {
            Account account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                CaptureDate = DateTime.Now
            };
            //Act
            bool actual = accountRepository.Insert(account);
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void GetAll_Return_ListOfData()
        {
            //Arrange
            //Act
            var actual = accountRepository.GetAll().ToList();
            //Assert
            Assert.That(actual.Count, Is.GreaterThan(1));
            Assert.That(actual, !Is.Null);
            Assert.That(actual.Count, Is.GreaterThan(1));
        }


        [Test]
        [TestCase(13, "Dube")]
        [TestCase(14, "dube")]
        [TestCase(16, "Testing")]
        public void Test_GetWhere_Valid_Return_List_Of_Object(int Id, string expected)
        {
            var actual = accountRepository.GetWhere(x => x.Id == Id).FirstOrDefault();
            Assert.That(actual.LastName, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(13)]
        public void Test_GetFirst_Return_object_of_Type(int Id)
        {
            var actual = accountRepository.GetFirst(x => x.Id == Id);
            //Asset
            Assert.IsInstanceOf<Account>(actual);
        }

        [Test]
        [TestCase(13)]
        [TestCase(13)]
        public void Test_GetFirst_Include_Account_Details(int Id)
        {
            var actual = accountRepository.GetFirst(x => x.Id == Id);
            //Asset
            Assert.IsInstanceOf<Account>(actual);
            Assert.NotNull(actual.AccountDetails);
            Assert.That(actual.AccountDetails.AccountId, Is.EqualTo(Id));
           
        }

        [Test]
        [TestCase(100, null)]
        [TestCase(200, null)]
        [TestCase(400, null)]
        public void Test_GetWhere_NoneExisting_Return_Null(int Id, string expected)
        {

            var actual = accountRepository.GetFirst(x => x.Id == Id);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Update_Existing_Record_Return_True()
        {
            var updateRecord = accountRepository.GetFirst(x => x.Id == 13);
            updateRecord.FirstName = "UpdateRecord";
            updateRecord.CaptureDate = DateTime.Now;
            var actual = accountRepository.Update(updateRecord);
            Assert.That(actual, Is.True);
        }

        [Test]
        [TestCase(3, true)]
        [TestCase(10, false)]
        public void Test_Delete_method(int Id, bool expected)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var delete = DeleteAccount();
                mock.Mock<IRepository<Account>>()
                    .Setup(x => x.Delete(delete)).Returns(expected);

                var cls = mock.Create<IRepository<Account>>();

                var actual = cls.Delete(delete);

                Assert.True(cls != null);
                Assert.That(expected, Is.EqualTo(actual));
            }
        }

        public Account DeleteAccount()
        {
            return new Account
            {
                Id = 3,
                FirstName = "Muzi",
                LastName = "Dube",
                EmailAddress = "me@example.com",
                PhoneNumber = "0824253417",
                CaptureDate = DateTime.Now
            };
        }
        private List<Account> GetAllAccount()
        {
            List<Account> output = new List<Account>
            {
                new Account
                {
                    FirstName = "Muzingaye",
                    LastName = "Dube",
                    EmailAddress = "me@exmpl.com",
                    PhoneNumber = "0116811000",
                    CaptureDate = DateTime.Now
                },
                new Account
                {
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "johndoe@example.com",
                    PhoneNumber = "021456789",
                    CaptureDate = DateTime.Now
                },
                new Account
                {
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
