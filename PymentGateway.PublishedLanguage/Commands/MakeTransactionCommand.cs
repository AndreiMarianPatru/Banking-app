using MediatR;
using System;

namespace PaymentGateway.PublishedLanguage.Commands
{
    public class MakeTransactionCommand : IRequest
    {

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
    }
}
