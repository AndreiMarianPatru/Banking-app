using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{


    public class MoneyWithdrawn : INotification
    {
        public int AccountId;

        public decimal Amount { get; set; }

        public MoneyWithdrawn(int accountId, int amount)
        {
            this.AccountId = accountId;
            this.Amount = amount;
        }
    }
}
