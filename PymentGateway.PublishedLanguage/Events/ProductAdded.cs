namespace PaymentGateway.PublishedLanguage.Events
{
    public class ProductAdded
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        public int Limit { get; set; }

        public ProductAdded(string name, double value, string currency, int limit)
        {
            this.Name = name;
            this.Value = value;
            this.Currency = currency;
            this.Limit = limit;
        }
    }
}
