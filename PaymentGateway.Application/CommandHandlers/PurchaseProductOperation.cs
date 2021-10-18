using MediatR;

using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using PaymentGateway.PublishedLanguage.Events;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.CommandHandlers
{
    public class PurchaseProductOperation : IRequestHandler<PurchaseProductCommand>
    {
        private readonly PaymentDbContext _dbContext;

        private readonly IMediator _mediator;

        public PurchaseProductOperation(IMediator mediator, PaymentDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(PurchaseProductCommand request, CancellationToken cancellationToken)
        {

            double total = 0d;
            var account = _dbContext.Accounts.FirstOrDefault(x => x.AccountId == request.IdAccount);
            if (request.Command != null)
            {

                foreach (var item in request.Command.Details)
                {
                    var product = _dbContext.Products.FirstOrDefault(x => x.Id == item.ProductId);
                    if (product.Limit < item.Quantity)
                    {
                        throw new Exception("Insufficient stocks");
                    }
                    total += product.Value * item.Quantity;

                }
            }
            if (account == null)
            {
                throw new Exception("Invalid Account");
            }
            if (account.Balance < total)
            {
                throw new Exception("Insufficient funds");
            }

            var transaction = new Transaction();
            transaction.Amount = total;
            transaction.Currency = account.Currency;
            transaction.Date = DateTime.UtcNow;
            transaction.Type = 1;
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            account.Balance -= transaction.Amount;
            foreach (var item in request.Command.Details)
            {

                var product = _dbContext.Products.FirstOrDefault(x => x.Id == item.ProductId);
                product.Limit -= item.Quantity;
                var ptx = new ProductXtransaction();
                ptx.IdProduct = product.Id;
                ptx.IdTransaction = transaction.Id;
                ptx.Quantity = (int)item.Quantity;
                _dbContext.ProductXtransactions.Add(ptx);
                _dbContext.SaveChanges();

            }
            ProductPurchased eventProductPurchased = new(request.Name, request.Currency, request.IdAccount, request.Command);
            await _mediator.Publish(eventProductPurchased, cancellationToken);
            return Unit.Value;


        }
    }
}
