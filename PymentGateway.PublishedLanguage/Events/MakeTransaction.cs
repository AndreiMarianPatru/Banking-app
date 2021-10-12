using System;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class MakeTransaction
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }

        public MakeTransaction(double amount, DateTime date, string currency, string type)
        {
            this.Amount = amount;
            this.Date = date;
            this.Currency = currency;
            this.Type = type;
        }
    }
}
