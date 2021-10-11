using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.WriteOperations
{
    public class CreateProductOperation : IWriteOperations<CreateProductCommand>
    {
       
            public IEventSender eventSender;
            public CreateProductOperation(IEventSender eventSender)
            {
                this.eventSender = eventSender;
            }

        

        public void PerformOperation(CreateProductCommand operation)
        {

            var random = new Random();
            Database database = Database.GetInstance();
            Product product = new Product();
            product.Id = database.Products.Count() + 1;
            product.Currency = operation.Currency;
            product.Limit = operation.Limit;
            product.Name = operation.Name;
            product.Value = operation.Value;
            database.Products.Add(product);
            database.SaveChange();
            ProductAdded eventProductAdded = new(operation.Name,operation.Value,operation.Currency,operation.Limit);
            eventSender.SendEvent(eventProductAdded);

        }

        
    }
}
