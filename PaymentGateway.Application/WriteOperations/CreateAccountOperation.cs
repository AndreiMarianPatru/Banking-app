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
        public IEventSender eventSender;
        public CreateAccountOperation(IEventSender eventSender)
        {
            this.eventSender = eventSender;
        }

        public void PerformOperation(CrerateAccountCommand operation)
        {
            var random = new Random();
            Database database = Database.GetInstance();
            Account account = new Account();
            account.AccountID = database.Accounts.Count() + 1;
            account.Balance = 0;
            account.Currency = operation.Currency;
            account.IbanCode = string.IsNullOrEmpty(operation.IbanCode) ? random.Next(1000000).ToString() : operation.IbanCode;
            account.Limit = operation.Limit;
            account.Status = operation.Status;
            account.Type = operation.Type;
            account.OwnerCnp = operation.OwnerCnp;
            database.Accounts.Add(account);
            database.SaveChange();
            
            AccountCreated eventCreateAccount = new(operation.Balance, operation.Currency, operation.IbanCode, operation.Type, operation.Status, operation.Limit, operation.AccountID, operation.OwnerCnp);
            eventSender.SendEvent(eventCreateAccount);
        }
    }
}
