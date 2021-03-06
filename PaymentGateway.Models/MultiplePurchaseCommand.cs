using System.Collections.Generic;

namespace PaymentGateway.Models
{
    public class MultiplePurchaseCommand
    {
        public List<CommandDetails> Details { get; set; }
        public class CommandDetails
        {
            public int ProductId { get; set; }
            public double Quantity { get; set; }
            // public bool RemoveFromPurchaseIfNotInStock { get; set; }
        }

    }
}
