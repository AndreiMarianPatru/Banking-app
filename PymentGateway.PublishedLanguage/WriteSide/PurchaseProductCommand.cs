using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.PublishedLanguage.WriteSide
{
    public class PurchaseProductCommand
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        
        public int IdAccount { get; set; }

        public MultiplePurchaseCommand Command { get; set; }
    }
}
