using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;

namespace PaymentGateway.Application.WriteOperations
{
    class CreateTransactionOperation : IWriteOperations<MakeTransactionCommand>
    {
        private readonly IEventSender _eventSender;
        private readonly Database _database;
        public CreateTransactionOperation(IEventSender eventSender, Database database)
        {

            _eventSender = eventSender;
            _database = database;

        }

        public void PerformOperation(MakeTransactionCommand operation)
        {

            Transaction transaction = new Transaction();
            transaction.Amount = operation.Amount;
            transaction.Currency = operation.Currency;
            transaction.Date = operation.Date;
            transaction.Type = operation.Type;
            _database.Transactions.Add(transaction);
            MakeTransaction transactionMade = new(operation.Amount,operation.Date,operation.Currency,operation.Type);
            _eventSender.SendEvent(transactionMade);
        }
    }
}
