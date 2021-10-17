namespace PaymentGateway.Models
{
    public class Account
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string IbanCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Limit { get; set; }
        public int AccountID { get; set; }
        public int OwnerID { get; set; }

        public string OwnerCnp { get; set; }
    

        public Account(decimal balance, string currency, string ibanCode, string type, string status, decimal limit, int id, string ownerCnp,int ownerID)
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
