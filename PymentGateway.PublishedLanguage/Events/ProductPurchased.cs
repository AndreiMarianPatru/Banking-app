using MediatR;
using PaymentGateway.Models;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class ProductPurchased : INotification
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
