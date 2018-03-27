using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Product()
        {
            Console.WriteLine("Product instance created");
        }

        public Product(int productId, string productName, string description) : this()
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;

            Console.WriteLine($"Product instance has a name: {ProductName}");
        }

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

        public string SayHello()
        {
            return $"Hello {ProductName} ({ProductId}): {Description}";
        }
    }
}
