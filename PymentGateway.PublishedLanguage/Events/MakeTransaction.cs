using MediatR;
using System;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class MakeTransaction : INotification
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public int Type1 { get; }

        public MakeTransaction(decimal amount, DateTime date, string currency, string type)
        {
            this.Amount = amount;
            this.Date = date;
            this.Currency = currency;
            this.Type = type;
        }

        public MakeTransaction(decimal amount, DateTime date, string currency, int type)
        {
            Amount = amount;
            Date = date;
            Currency = currency;
            Type1 = type;
        }
    }
}
