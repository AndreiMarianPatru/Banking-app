using System;

namespace PaymentGateway.PublishedLanguage.WriteSide
{
    public class MakeTransactionCommand
    {

        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
    }
}
