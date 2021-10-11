using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class ProductPurchased
    {
        public string Name { get; set; }
        
        public string Currency { get; set; }

        public int IdAccount { get; set; }

        public MultiplePurchaseCommand Command { get; set; }

        public ProductPurchased(string name, string currency, int IdAccount, MultiplePurchaseCommand command)
        {
            this.Name = name;
            this.Currency = currency;
            this.IdAccount = IdAccount;
            this.Command = command;
        }
    }
}
