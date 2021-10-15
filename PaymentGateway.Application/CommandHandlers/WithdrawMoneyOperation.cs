using MediatR;

using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using PaymentGateway.PublishedLanguage.Events;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.CommandHandlers
{
    public class WithdrawMoneyOperation : IRequestHandler<WithdrawMoneyCommand>
    {
        private readonly IMediator _mediator;

        private readonly Database _database;

        public WithdrawMoneyOperation(IMediator mediator, Database database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
        {


            var account = _database.Accounts.FirstOrDefault(x => x.AccountID == request.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found ");
            }
            if (account.Balance < request.Ammount)
            {
                throw new Exception("Insufficient money!");
            }


            var transaction = new Transaction();
            transaction.Amount = request.Ammount;
            transaction.Currency = account.Currency;
            transaction.Date = DateTime.UtcNow;
            transaction.Type = "Normal";
            _database.Transactions.Add(transaction);
            account.Balance -= transaction.Amount;


            _database.SaveChange();
            MoneyWithdrawn eventMoneyDeposited = new(request.AccountId, request.Ammount);
            await _mediator.Publish(eventMoneyDeposited, cancellationToken);
            return Unit.Value;



        }
    }
}
