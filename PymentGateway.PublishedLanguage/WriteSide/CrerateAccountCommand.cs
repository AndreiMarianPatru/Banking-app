namespace PaymentGateway.PublishedLanguage.WriteSide
{
    public class CrerateAccountCommand
    {

        public double Balance { get; set; }
        public string Currency { get; set; }
        public string IbanCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double Limit { get; set; }
        public int AccountID { get; set; }
        public string OwnerCnp { get; set; }




    }
}
