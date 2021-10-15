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
        public int OwnerID { get; set; }

        public string OwnerCnp { get; set; }
    

        public Account(double balance, string currency, string ibanCode, string type, string status, double limit, int id, string ownerCnp,int ownerID)
        {
            Balance = balance;
            Currency = currency;
            IbanCode = ibanCode;
            Limit = limit;
            Status = status;
            Type = type;
            AccountID = id;
            OwnerCnp = ownerCnp;
            OwnerID = ownerID;
           
        }
        public Account()
        {


        }
    }
}
