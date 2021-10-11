using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.WriteOperations
{
    class CreateTransactionOperation : IWriteOperations<MakeTransactionCommand>
    {
        public IEventSender eventSender;
        public CreateTransactionOperation(IEventSender eventSender)
        {
            this.eventSender = eventSender;
        }

        public void PerformOperation(MakeTransactionCommand operation)
        {
            Database database = Database.GetInstance();
            Transaction transaction = new Transaction();
            transaction.Amount = operation.Amount;
            transaction.Currency = operation.Currency;
            transaction.Date = operation.Date;
            transaction.Type = operation.Type;
            database.Transactions.Add(transaction);
        }
    }
}
