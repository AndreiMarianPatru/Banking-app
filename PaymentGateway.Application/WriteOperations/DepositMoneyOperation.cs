using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;

using System;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using PaymentGateway.PublishedLanguage.Commands;

namespace PaymentGateway.Application.WriteOperations
{
    public class DepositMoneyOperation : IRequestHandler<DepositMoneyCommand>
    {
        private readonly IEventSender _eventSender;
        private readonly Database _database;
        public DepositMoneyOperation(IEventSender eventSender, Database database)
        {
            _eventSender = eventSender;
            _database = database;
        }

        public Task<Unit> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {

            var account = _database.Accounts.FirstOrDefault(x => x.AccountID == request.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found ");
            }


            var transaction = new Transaction();
            transaction.Amount = request.Ammount;
            transaction.Currency = account.Currency;
            transaction.Date = DateTime.UtcNow;
            transaction.Type = "Normal";
            account.Balance += transaction.Amount;


            _database.SaveChange();
            MoneyDeposited eventMoneyDeposited = new(request.AccountId, request.Ammount);
            _eventSender.SendEvent(eventMoneyDeposited);
            return Unit.Task;



        }
    }
}
