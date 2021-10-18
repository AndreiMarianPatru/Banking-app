using System;
using System.Collections.Generic;

#nullable disable

namespace PaymentGateway.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int AccountId { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string IbanCode { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public decimal? Limit { get; set; }
        public string? OwnerCnp { get; set; }
        public int? OwnerId { get; set; }

        public virtual Person Owner { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
