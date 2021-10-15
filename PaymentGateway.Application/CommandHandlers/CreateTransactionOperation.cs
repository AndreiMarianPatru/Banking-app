using MediatR;

using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using PaymentGateway.PublishedLanguage.Events;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.CommandHandlers
{
    class CreateTransactionOperation : IRequestHandler<MakeTransactionCommand>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public CreateTransactionOperation(IMediator mediator, Database database)
        {

            _mediator = mediator;
            _database = database;

        }

        public async Task<Unit> Handle(MakeTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = new Transaction();
            transaction.Amount = request.Amount;
            transaction.Currency = request.Currency;
            transaction.Date = request.Date;
            transaction.Type = request.Type;
            _database.Transactions.Add(transaction);
            MakeTransaction transactionMade = new(request.Amount, request.Date, request.Currency, request.Type);
            await _mediator.Publish(transactionMade, cancellationToken);
            return Unit.Value;
        }


    }
}
