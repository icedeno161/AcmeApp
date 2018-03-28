using System;
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
        #region Constructors

        
        public Product()
        {
            //ProductVendor = new Vendor();

            Console.WriteLine("Product instance created");
        }

        public Product(int productId, string productName, string description) : this()
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;

            Console.WriteLine($"Product instance has a name: {ProductName}");
        }
        #endregion

        #region Properties
        
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
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

        public DateTime? AvailabilityDate { get; set; }

        #endregion

        #region Methods

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
        #endregion
    }
}
