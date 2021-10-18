using System;
using System.Collections.Generic;

#nullable disable

namespace PaymentGateway.Models
{
    public partial class ProductXtransaction
    {
        public int? IdProduct { get; set; }
        public int? IdTransaction { get; set; }
        public int Quantity { get; set; }

        public virtual Product IdProductNavigation { get; set; }
        public virtual Transaction IdTransactionNavigation { get; set; }
    }
}
