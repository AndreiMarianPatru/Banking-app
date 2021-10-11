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
        public IEventSender eventSender;
        public DepositMoneyOperation(IEventSender eventSender)
        {
            this.eventSender = eventSender;
        }

        public void PerformOperation(DepositMoneyCommand operation)
        {
            Database database = Database.GetInstance();
            var account = database.Accounts.FirstOrDefault(x => x.AccountID == operation.AccountId);

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


            database.SaveChange();
            MoneyDeposited eventMoneyDeposited = new(operation.AccountId, operation.Ammount);
            eventSender.SendEvent(eventMoneyDeposited);


        }
    }
}
