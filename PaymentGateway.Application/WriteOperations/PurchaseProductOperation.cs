using PaymentGateway.Abstractions;
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.WriteSide;
using System;
using System.Linq;

namespace PaymentGateway.Application.WriteOperations
{
    public class PurchaseProductOperation
    {
        private readonly Database _database;
        private readonly IEventSender _eventSender;
        public PurchaseProductOperation(IEventSender eventSender, Database database)
        {
            _eventSender = eventSender;
            _database = database;
        }

        public void PerformOperation(PurchaseProductCommand operation)
        {
            
            var total = 0d;
            var account = _database.Accounts.FirstOrDefault(x => x.AccountID == operation.IdAccount);
            if (operation.Command != null)
            {

                foreach (var item in operation.Command.Details)
                {
                    var product = _database.Products.FirstOrDefault(x => x.Id == item.ProductId);
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
            transaction.Type = "Normal";
            _database.Transactions.Add(transaction);
            _database.SaveChange();
            account.Balance -= transaction.Amount;
            foreach (var item in operation.Command.Details)
            {

                var product = _database.Products.FirstOrDefault(x => x.Id == item.ProductId);
                product.Limit -= item.Quantity;
                var ptx = new ProductXTransaction();
                ptx.IdProduct = product.Id;
                ptx.IdTransaction = transaction.Id;
                ptx.Quantity = item.Quantity;
                _database.pxt.Add(ptx);

            }
            ProductPurchased eventProductPurchased = new(operation.Name, operation.Currency, operation.IdAccount, operation.Command);
            _eventSender.SendEvent(eventProductPurchased);

        }
    }
}
