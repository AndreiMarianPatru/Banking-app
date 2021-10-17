using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class MoneyDeposited : INotification
    {
        public int AccountId;

        public decimal Ammount;

        public MoneyDeposited(int accountId, decimal amount)
        {
            this.AccountId = accountId;
            this.Ammount = amount;
        }
    }
}
