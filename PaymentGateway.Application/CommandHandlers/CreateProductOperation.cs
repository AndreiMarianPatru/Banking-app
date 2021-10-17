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
    public class CreateProductOperation : IRequestHandler<CreateProductCommand> {

        private readonly IMediator _mediator;
        private readonly PaymentDbContext _dbContext;
        public CreateProductOperation(IMediator mediator, PaymentDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }



        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var random = new Random();
            
            Product product = new Product();
            product.Id = _dbContext.Products.Count() + 1;
            product.Currency = request.Currency;
            product.Limit = request.Limit;
            product.Name = request.Name;
            product.Value = request.Value;
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            ProductAdded eventProductAdded = new(request.Name, request.Value, request.Currency, request.Limit);
            await _mediator.Publish(eventProductAdded, cancellationToken);
            return Unit.Value;

        }


    }
}
