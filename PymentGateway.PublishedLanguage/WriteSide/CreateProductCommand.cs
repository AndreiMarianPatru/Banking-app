namespace PaymentGateway.PublishedLanguage.WriteSide
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        public int Limit { get; set; }
    }
}
