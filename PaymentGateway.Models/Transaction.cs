using System;
using System.Collections.Generic;

#nullable disable

namespace PaymentGateway.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public int Type { get; set; }
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
