namespace PaymentGateway.Models
{
    public class Account
    {
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string IbanCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double Limit { get; set; }
        public int AccountID { get; set; }

        public string OwnerCnp { get; set; }

        public Account(double balance, string currency, string IbanCode, string type, string status, double limit, int id, string ownerCnp)
        {
            this.Balance = balance;
            this.Currency = currency;
            this.IbanCode = IbanCode;
            this.Limit = limit;
            this.Status = status;
            this.Type = type;
            this.AccountID = id;
            this.OwnerCnp = ownerCnp;
        }
        public Account()
        {


        }
    }
}
