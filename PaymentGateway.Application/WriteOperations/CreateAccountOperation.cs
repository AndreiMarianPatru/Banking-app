using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.Commands;
using System;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using System.Threading;


namespace PaymentGateway.Application.WriteOperations
{
    public class CreateAccountOperation : IRequestHandler<CreateAccountCommand>
    {
        private readonly IEventSender _eventSender;
        private readonly AccountOptions _accountOptions;
        private readonly Database _database;


        public CreateAccountOperation(IEventSender eventSender, AccountOptions accountOptions,Database database)
        {
            _eventSender = eventSender;
            _accountOptions = accountOptions;
            _database = database;
        }

        public Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var random = new Random();
            
            Account account = new Account();
            account.AccountID = _database.Accounts.Count() + 1;
            account.Balance = 0;
            account.Currency = request.Currency;
            account.IbanCode = string.IsNullOrEmpty(request.IbanCode) ? random.Next(1000000).ToString() : request.IbanCode;
            account.Limit = request.Limit;
            account.Status = request.Status;
            account.Type = request.Type;
            account.OwnerCnp = request.OwnerCnp;
            _database.Accounts.Add(account);
            _database.SaveChange();

            AccountCreated eventCreateAccount = new(request.Balance, request.Currency, request.IbanCode, request.Type, request.Status, request.Limit, request.AccountID, request.OwnerCnp);
            _eventSender.SendEvent(eventCreateAccount);
            return Unit.Task;

        }

     
    }
}
