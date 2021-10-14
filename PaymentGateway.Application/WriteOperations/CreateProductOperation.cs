using MediatR;
using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using PaymentGateway.PublishedLanguage.Events;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.WriteOperations
{
    public class CreateProductOperation : IRequestHandler<CreateProductCommand> { 

        private readonly  IEventSender _eventSender;
        private readonly Database _database;
        public CreateProductOperation(IEventSender eventSender,Database database)
        {
            _eventSender = eventSender;
            _database = database;
        }



    public Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

            var random = new Random();
            
            Product product = new Product();
            product.Id = _database.Products.Count() + 1;
            product.Currency = request.Currency;
            product.Limit = request.Limit;
            product.Name = request.Name;
            product.Value = request.Value;
            _database.Products.Add(product);
            _database.SaveChange();
            ProductAdded eventProductAdded = new(request.Name, request.Value, request.Currency, request.Limit);
            _eventSender.SendEvent(eventProductAdded);
        return Unit.Task;

        }


    }
}
