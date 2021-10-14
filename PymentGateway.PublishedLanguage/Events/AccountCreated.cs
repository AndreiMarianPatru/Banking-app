using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class AccountCreated : INotification
    {
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string IbanCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double Limit { get; set; }
        public int AccountID { get; set; }
        public string OwnerCnp { get; set; }


        public AccountCreated(double balance, string currency, string ibanCode, string type, string status, double limit, int accountID, string ownerCnp)
        {
            this.Balance = balance;
            this.Currency = currency;
            this.IbanCode = ibanCode;
            this.Type = type;
            this.Status = status;
            this.Limit = limit;
            this.AccountID = accountID;
            this.OwnerCnp = ownerCnp;
        }
    }
}
