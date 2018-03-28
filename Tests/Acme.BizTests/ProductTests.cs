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

            var expected = "Hello Acme (1): test description Available on: ";

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
            var expected = "Hello MI6 (2): fake agency Available on: ";

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

        [TestMethod]
        public void ConvertMetersToInchesTest()
        {
            //Arrange
            var expected = 78.74;

            //Act
            var actual = Product.InchesPerMeter * 2;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinimumPriceTest_Default()
        {
            //Arrange
            var product = new Product();
            var expected = .96m;

            //Act
            var actual = product.MinimumPrice;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinimumPriceTest_Bulk()
        {
            //Arrange
            var product = new Product(1, "Bulk iPod", "lost of ipods");
            var expected = 9.99m;

            //Act
            var actual = product.MinimumPrice;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductName_Format()
        {
            //Arrange
            var product = new Product();
            var expected = "ACME";

            product.ProductName = "   ACME       ";

            //Act
            var actual = product.ProductName;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductName_TooShort()
        {
            //Arrange
            var product = new Product();
            var expected = "Product Name must be greater than 3 characters.";

            product.ProductName = "AB";

            //Act
            var actual = product.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductName_TooLong()
        {
            //Arrange
            var product = new Product();
            var expected = "Product Name must be less than 20 characters.";

            product.ProductName = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //Act
            var actual = product.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductName_JustRight()
        {
            //Arrange
            var product = new Product();
            //var expected = nu;

            product.ProductName = " Pied Piper   ";

            //Act
            var actual = product.ValidationMessage;

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ProductCategoryTest()
        {
            //Arrange
            var product = new Product();
            var expected = "Tools";

            //Act
            var actual = product.Category;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductSequenceNumberTest()
        {
            //Arrange
            var product = new Product();
            var expected = 1;

            //Act
            var actual = product.SequenceNumber;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductCodeTest()
        {
            //Arrange
            var product = new Product
            {
                Category = "Garden",
                SequenceNumber = 3
            };
            var expected = "Garden: 3";

            //Act
            var actual = product.ProductCode;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateSuggestedPriceTest()
        {
            //Arrange
            var product = new Product();
            var expected = 3.15m;

            product.Cost = 2.25m;

            //Act
            var actual = product.CalculateSuggestedPrice(40m);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}