using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Linq;

namespace PaymentGateway.Application.WriteOperations
{
    public class WithdrawMoneyOperation : IWriteOperations<WithdrawMoneyCommand>
    {
        private readonly IEventSender _eventSender;
        private readonly Database _database;

        public WithdrawMoneyOperation(IEventSender eventSender, Database database)
        {
            _eventSender = eventSender;
            _database = database;
        }

        public void PerformOperation(WithdrawMoneyCommand operation)
        {

           
            var account = _database.Accounts.FirstOrDefault(x => x.AccountID == operation.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found ");
            }
            if (account.Balance < operation.Ammount)
            {
                throw new Exception("Insufficient money!");
            }


            var transaction = new Transaction();
            transaction.Amount = operation.Ammount;
            transaction.Currency = account.Currency;
            transaction.Date = DateTime.UtcNow;
            transaction.Type = "Normal";
            _database.Transactions.Add(transaction);
            account.Balance -= transaction.Amount;


            _database.SaveChange();
            MoneyWithdrawn eventMoneyDeposited = new(operation.AccountId, operation.Ammount);
            _eventSender.SendEvent(eventMoneyDeposited);


        }
    }
}
