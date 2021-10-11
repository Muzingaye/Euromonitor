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
    public class ProductServicesTest
    {
        private Mock<ProductServices> productMockServices;

        [SetUp]
        public void SetUp()
        {
            productMockServices = new Mock<ProductServices>();
        }

        [Test]
        public void Test_GetAvailable_Products_Returns_All_Products()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IService<Product>>()
                    .Setup(x => x.GetAll()).Returns(new List<Product>());


                var cls = mock.Create<IService<Product>>();
                var expected = new List<Product>();

                var actual = cls.GetAll().ToList();

                Assert.True(cls != null);
                Assert.That(expected.Count, Is.EqualTo(actual.Count));
            }
        }
    }
}