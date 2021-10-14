using MediatR;
using System.Collections.Generic;
using static PaymentGateway.Models.MultiplePurchaseCommand;

namespace PaymentGateway.PublishedLanguage.Commands
{
    public class Command : IRequest
    {
        public List<CommandDetails> Details { get; set; } = new List<CommandDetails>();
        public string Cnp { get; set; }
        public string Iban { get; set; }
    }
}
