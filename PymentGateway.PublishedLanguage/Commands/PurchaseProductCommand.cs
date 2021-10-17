using MediatR;
using PaymentGateway.Models;

namespace PaymentGateway.PublishedLanguage.Commands
{
    public class PurchaseProductCommand : IRequest
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }

        public int IdAccount { get; set; }

        public MultiplePurchaseCommand Command { get; set; }
    }
}
