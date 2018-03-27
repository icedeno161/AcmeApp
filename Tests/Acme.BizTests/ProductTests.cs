using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            // Arrange
            var product = new Product()
            {
                ProductId = 1,
                ProductName = "Acme",
                Description = "test description"
            };
            product.ProductVendor.CompanyName = "Acme Inc.";

            var expected = $"Hello Acme (1): test description";

            //Act
            var actual = product.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Parameterized_SayHelloTest()
        {
            //Arrange
            var product = new Product(2, "MI6", "fake agency");
            var expected = "Hello MI6 (2): fake agency";

            //Act
            var actual = product.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Product_Null()
        {
            //Arrange
            Product product = null;
            var companyName = product?.ProductVendor?.CompanyName;
            string expected = null;

            //Act
            var actual = companyName;
            
            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}