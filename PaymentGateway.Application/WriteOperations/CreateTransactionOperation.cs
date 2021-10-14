using MediatR;
using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using PaymentGateway.PublishedLanguage.Events;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.WriteOperations
{
    class CreateTransactionOperation : IRequestHandler<MakeTransactionCommand>
    {
        private readonly IEventSender _eventSender;
        private readonly Database _database;
        public CreateTransactionOperation(IEventSender eventSender, Database database)
        {

            _eventSender = eventSender;
            _database = database;

        }

        public Task<Unit> Handle(MakeTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = new Transaction();
            transaction.Amount = request.Amount;
            transaction.Currency = request.Currency;
            transaction.Date = request.Date;
            transaction.Type = request.Type;
            _database.Transactions.Add(transaction);
            MakeTransaction transactionMade = new(request.Amount, request.Date, request.Currency, request.Type);
            _eventSender.SendEvent(transactionMade);
            return Unit.Task;
        }

       
    }
}
