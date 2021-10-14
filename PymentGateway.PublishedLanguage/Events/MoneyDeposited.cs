using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class MoneyDeposited : INotification
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
