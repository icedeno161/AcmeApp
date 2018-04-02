using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrderTest_Product_Null()
        {
            //Arrange
            Product product = null;
            var vendor = new Vendor();
            var quantity = 2;

            //Act
            vendor.PlaceOrder(product, quantity);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrderTest_QuantityInvalid()
        {
            //Arrange
            Product product = new Product();
            var vendor = new Vendor();
            var quantity = 0;

            //Act
            vendor.PlaceOrder(product, quantity);
        }

        [TestMethod()]
        public void PlaceOrderTest_Success()
        {
            //Arrange
            Product product = new Product();
            var vendor = new Vendor();
            var quantity = 2;
            var expected_bool = true;
            var expected_message =
                @"Order from Acme, Inc
Product: Tools: 1
Quantity: 2
Instructions: standard delivery";

            //Act
            var actual = vendor.PlaceOrder(product, quantity);

            //Assert
            Assert.AreEqual(expected_bool, actual.Success);
            Assert.AreEqual(expected_message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_Success_3Params()
        {
            //Arrange
            Product product = new Product();
            var vendor = new Vendor();
            var quantity = 2;
            var expected_bool = true;
            var expected_message =
                @"Order from Acme, Inc
Product: Tools: 1
Quantity: 2
Deliver By: 4/26/2018
Instructions: standard delivery";

            //Act
            var actual = vendor.PlaceOrder(product, quantity, new DateTimeOffset(new DateTime(2018, 4, 26)));

            //Assert
            Assert.AreEqual(expected_bool, actual.Success);
            Assert.AreEqual(expected_message, actual.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrderTest_3Params_DateInvalid()
        {
            //Arrange
            Product product = new Product();
            var vendor = new Vendor();
            var quantity = 2;

            //Act
            var actual = vendor.PlaceOrder(product, quantity, new DateTimeOffset(new DateTime(2017, 4, 26)));

        }

        [TestMethod()]
        public void PlaceOrderTest_Success_4Params()
        {
            //Arrange
            Product product = new Product();
            var vendor = new Vendor();
            var quantity = 2;
            var expected_bool = true;
            var expected_message =
                @"Order from Acme, Inc
Product: Tools: 1
Quantity: 2
Deliver By: 4/26/2018
Instructions: leave at door.";

            //Act
            var actual = vendor.PlaceOrder(product, quantity, new DateTimeOffset(new DateTime(2018, 4, 26)), " leave at door. ");

            //Assert
            Assert.AreEqual(expected_bool, actual.Success);
            Assert.AreEqual(expected_message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_Success_4Params_NoInstructions()
        {
            //Arrange
            Product product = new Product();
            var vendor = new Vendor();
            var quantity = 2;
            var expected_bool = true;
            var expected_message =
                @"Order from Acme, Inc
Product: Tools: 1
Quantity: 2
Deliver By: 4/26/2018";

            //Act
            var actual = vendor.PlaceOrder(product, quantity, new DateTimeOffset(new DateTime(2018, 4, 26)), null);

            //Assert
            Assert.AreEqual(expected_bool, actual.Success);
            Assert.AreEqual(expected_message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_WithAddress()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product();
            OperationResult expected = new OperationResult(true, "Test With Address");

            //Act
            var actual = vendor.PlaceOrder(product,
                quantity: 12, includeAddress: Vendor.IncludeAddress.yes, sendCopy: Vendor.SendCopy.no);

            //Assert
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_WithCopy()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product();
            OperationResult expected = new OperationResult(true, "Test With Copy");

            //Act
            var actual = vendor.PlaceOrder(product, quantity: 12,
                includeAddress: Vendor.IncludeAddress.no, sendCopy: Vendor.SendCopy.yes);

            //Assert
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void VendorToStringTest()
        {
            //Arrange
            var vendor = new Vendor
            {
                CompanyName = "Sweet Water"
            };
            var expected = "Vendor: Sweet Water";

            //Act
            var actual = vendor.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}