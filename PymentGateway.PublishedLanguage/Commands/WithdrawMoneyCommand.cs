using MediatR;
namespace PaymentGateway.PublishedLanguage.Commands
{
    public class WithdrawMoneyCommand : IRequest
    {
        public int AccountId;

        public int amount;
    }
}
