using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Linq;

namespace PaymentGateway.Application.WriteOperations
{
    public class DepositMoneyOperation : IWriteOperations<DepositMoneyCommand>
    {
        private readonly IEventSender _eventSender;
        private readonly Database _database;
        public DepositMoneyOperation(IEventSender eventSender, Database database)
        {
            _eventSender = eventSender;
            _database = database;
        }

        public void PerformOperation(DepositMoneyCommand operation)
        {
           
            var account = _database.Accounts.FirstOrDefault(x => x.AccountID == operation.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found ");
            }


            var transaction = new Transaction();
            transaction.Amount = operation.Ammount;
            transaction.Currency = account.Currency;
            transaction.Date = DateTime.UtcNow;
            transaction.Type = "Normal";
            account.Balance += transaction.Amount;


            _database.SaveChange();
            MoneyDeposited eventMoneyDeposited = new(operation.AccountId, operation.Ammount);
            _eventSender.SendEvent(eventMoneyDeposited);


        }
    }
}
