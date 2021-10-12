namespace PaymentGateway.PublishedLanguage.Events
{


    public class MoneyWithdrawn
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
