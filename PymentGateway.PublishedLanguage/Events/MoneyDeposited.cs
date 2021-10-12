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
