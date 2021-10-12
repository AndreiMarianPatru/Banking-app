using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Linq;

namespace PaymentGateway.Application.WriteOperations
{
    public class CreateProductOperation : IWriteOperations<CreateProductCommand>
    {

        private readonly  IEventSender _eventSender;
        private readonly Database _database;
        public CreateProductOperation(IEventSender eventSender,Database database)
        {
            _eventSender = eventSender;
            _database = database;
        }



        public void PerformOperation(CreateProductCommand operation)
        {

            var random = new Random();
            
            Product product = new Product();
            product.Id = _database.Products.Count() + 1;
            product.Currency = operation.Currency;
            product.Limit = operation.Limit;
            product.Name = operation.Name;
            product.Value = operation.Value;
            _database.Products.Add(product);
            _database.SaveChange();
            ProductAdded eventProductAdded = new(operation.Name, operation.Value, operation.Currency, operation.Limit);
            _eventSender.SendEvent(eventProductAdded);

        }


    }
}
