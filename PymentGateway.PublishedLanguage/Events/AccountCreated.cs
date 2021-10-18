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
        public decimal Limit { get; set; }
        public int AccountID { get; set; }
        public string OwnerCnp { get; set; }
        public int Type1 { get; }
        public int Status1 { get; }
        public decimal? Limit1 { get; }
        public int? AccountID1 { get; }

        public AccountCreated(double balance, string currency, string ibanCode, string type, string status, decimal limit, int accountID, string ownerCnp)
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

        public AccountCreated(double balance, string currency, string ibanCode, int type, int status, decimal? limit, int? accountID, string ownerCnp)
        {
            Balance = balance;
            Currency = currency;
            IbanCode = ibanCode;
            Type1 = type;
            Status1 = status;
            Limit1 = limit;
            AccountID1 = accountID;
            OwnerCnp = ownerCnp;
        }
    }
}
