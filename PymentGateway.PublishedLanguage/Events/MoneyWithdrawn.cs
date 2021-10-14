using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{


    public class MoneyWithdrawn : INotification
    {
        public int AccountId;

        public int Ammount;

        public MoneyWithdrawn(int accountId, int ammount)
        {
            this.AccountId = accountId;
            this.Ammount = ammount;
        }
    }
}
