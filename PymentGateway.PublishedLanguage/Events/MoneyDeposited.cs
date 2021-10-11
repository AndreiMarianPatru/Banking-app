using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class MoneyDeposited
    {
        public int AccountId;

        public int Ammount;

        public MoneyDeposited(int accountId, int ammount)
        {
            this.AccountId = accountId;
            this.Ammount = ammount;
        }
    }
}
