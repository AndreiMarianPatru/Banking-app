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
        public IEventSender eventSender;
        public WithdrawMoneyOperation(IEventSender eventSender)
        {
            this.eventSender = eventSender;
        }

        public void PerformOperation(WithdrawMoneyCommand operation)
        {
            Database database = Database.GetInstance();
            var account = database.Accounts.FirstOrDefault(x => x.AccountID == operation.AccountId);

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
            database.Transactions.Add(transaction);
            account.Balance -= transaction.Amount;


            database.SaveChange();
            MoneyWithdrawn eventMoneyDeposited = new(operation.AccountId, operation.Ammount);
            eventSender.SendEvent(eventMoneyDeposited);


        }
    }
}
