using MediatR;

namespace PaymentGateway.PublishedLanguage.Commands
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        public int Limit { get; set; }
    }
}
