
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;

using System;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using PaymentGateway.PublishedLanguage.Commands;

namespace PaymentGateway.Application.CommandHandlers
{
    public class DepositMoneyOperation : IRequestHandler<DepositMoneyCommand>
    {
        private readonly IMediator _mediator;
        private readonly PaymentDbContext _dbContext;
        public DepositMoneyOperation(IMediator mediator, PaymentDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {

            var account = _dbContext.Accounts.FirstOrDefault(x => x.AccountID == request.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found ");
            }


            var transaction = new Transaction();
            transaction.Amount = request.Amount;
            transaction.Currency = account.Currency;
            transaction.Date = DateTime.UtcNow;
            transaction.Type = "Normal";
            account.Balance += transaction.Amount;


            _dbContext.SaveChanges();
            MoneyDeposited eventMoneyDeposited = new(request.AccountId, request.Amount);
             await _mediator.Publish(eventMoneyDeposited, cancellationToken);
            return Unit.Value;



        }
    }
}
