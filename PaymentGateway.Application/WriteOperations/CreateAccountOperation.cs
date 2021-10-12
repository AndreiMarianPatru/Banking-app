using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Linq;

namespace PaymentGateway.Application.WriteOperations
{
    public class CreateAccountOperation : IWriteOperations<CrerateAccountCommand>
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

        public void PerformOperation(CrerateAccountCommand operation)
        {
            var random = new Random();
            
            Account account = new Account();
            account.AccountID = _database.Accounts.Count() + 1;
            account.Balance = 0;
            account.Currency = operation.Currency;
            account.IbanCode = string.IsNullOrEmpty(operation.IbanCode) ? random.Next(1000000).ToString() : operation.IbanCode;
            account.Limit = operation.Limit;
            account.Status = operation.Status;
            account.Type = operation.Type;
            account.OwnerCnp = operation.OwnerCnp;
            _database.Accounts.Add(account);
            _database.SaveChange();

            AccountCreated eventCreateAccount = new(operation.Balance, operation.Currency, operation.IbanCode, operation.Type, operation.Status, operation.Limit, operation.AccountID, operation.OwnerCnp);
            _eventSender.SendEvent(eventCreateAccount);
        }
    }
}
