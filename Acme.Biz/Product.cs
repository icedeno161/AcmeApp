﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;
using static Acme.Common.LoggingService;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the products in our inventory.
    /// </summary>
    public class Product
    {
        private string productName;
        private string description;
        private int productId;
        private Vendor productVendor;

        public const double InchesPerMeter = 39.37;

        public readonly decimal MinimumPrice;

        #region Constructors

        
        public Product()
        {
            //ProductVendor = new Vendor();
            MinimumPrice = .96m;
            Category = "Tools";

            Console.WriteLine("Product instance created");
        }

        public Product(int productId, string productName, string description) : this()
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;

            if (ProductName.StartsWith("Bulk"))
            {
                MinimumPrice = 9.99m;
            }

            Console.WriteLine($"Product instance has a name: {ProductName}");
        }
        #endregion

        #region Properties
        
        public string ProductName
        {
            get
            {
                var formattedValue = productName.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be greater than 3 characters.";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name must be less than 20 characters.";
                }
                else
                {
                    productName = value;
                }
            }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public Vendor ProductVendor {
            get
            {
                return productVendor ?? new Vendor();
            }
            set { productVendor = value; }
        }

        internal string Category { get;  set; }
        public int SequenceNumber { get; set; } = 1;

        public string ProductCode => $"{Category}: {SequenceNumber}";
        public DateTime? AvailabilityDate { get; set; }
        public decimal Cost { get; set; }

        public string ValidationMessage { get; private set; }

        #endregion

        #region Methods

        public decimal CalculateSuggestedPrice(decimal markupPercent) =>
            Cost + (Cost * markupPercent / 100);

        public string SayHello()
        {
            //var vendor = new Vendor();
            var emailService = new EmailService();

            string confirmation;
            string result;

            //vendor.SendWelcomeEmail("Message from Product");
            confirmation = emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");
            result = LogAction("saying hello");

            return $"Hello {ProductName} ({ProductId}): {Description} Available on: {AvailabilityDate?.ToShortDateString()}";
        }

        public override string ToString() =>
            $"{ProductName} ({ProductId})";
        
        #endregion
    }
}
