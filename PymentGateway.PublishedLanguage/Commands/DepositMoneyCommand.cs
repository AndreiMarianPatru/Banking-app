using MediatR;
namespace PaymentGateway.PublishedLanguage.Commands
{
    public class DepositMoneyCommand : IRequest
    {
        public int AccountId;

        public decimal Amount { get; set; }




    }
}
