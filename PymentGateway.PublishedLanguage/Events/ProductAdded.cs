using MediatR;

namespace PaymentGateway.PublishedLanguage.Events
{
    public class ProductAdded : INotification
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public int Limit { get; set; }

        public ProductAdded(string name, decimal value, string currency, int limit)
        {
            this.Name = name;
            this.Value = value;
            this.Currency = currency;
            this.Limit = limit;
        }
    }
}
