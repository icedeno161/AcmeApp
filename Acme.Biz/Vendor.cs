using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor
    {
        #region Fields

        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message,
                                                        this.Email);
            return confirmation;
        }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">product to order.</param>
        /// <param name="quantity">quantity of product to order</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity) =>
            PlaceOrder(product, quantity, null, null);

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">product to order.</param>
        /// <param name="quantity">quantity of product to order</param>
        /// <param name="deliverBy">Date to deliver order by</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy) 
            => PlaceOrder(product, quantity, deliverBy, null);
        
        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">product to order.</param>
        /// <param name="quantity">quantity of product to order</param>
        /// <param name="deliverBy">Date to deliver order by</param>
        /// <param name="instructions">Instructions for order</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy, string instructions)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now) throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;
            var emailService = new EmailService();
            var orderText =
$@"Order from Acme, Inc
Product: {product.ProductCode}
Quantity: {quantity}";

            if (deliverBy.HasValue)
            {
                orderText += $"\r\nDeliver By: {deliverBy.Value.ToString("d")}";
            }

            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderText += $"\r\nInstructions: {instructions.Trim()}";
            }

            var confirmation = emailService.SendMessage("New Order", orderText, Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }

            return new OperationResult(success, orderText);
        }
        #endregion

    }
}
