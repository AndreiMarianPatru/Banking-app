using PaymentGateway.Abstractions;
using PaymentGateway.Application.WriteOperations;
using PaymentGateway.ExternalService;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.WriteSide;
using PaymentGateway.WriteSide;
using System.Collections.Generic;
using static PaymentGateway.Models.MultiplePurchaseCommand;

namespace PaymentGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            EnrollCustomerCommand customer1 = new EnrollCustomerCommand();
            customer1.Name = "Gigi";
            customer1.Currency = "$";
            customer1.Cnp = "5000118784512";
            customer1.ClientType = "Individual";
            customer1.AccountType = "Credit";

            IEventSender eventSender = new EventSender();

            EnrollCustomerOperation enrollOp1 = new EnrollCustomerOperation(eventSender);
            enrollOp1.PerformOperation(customer1);

            CrerateAccountCommand account1 = new CrerateAccountCommand();
            account1.Currency = "RON";
            account1.Limit = 1000000.00;
            account1.Status = "Open";
            account1.Type = "Current";
            account1.OwnerCnp = "5000118784512";

            CreateAccountOperation createacc1 = new CreateAccountOperation(eventSender);
            createacc1.PerformOperation(account1);

            DepositMoneyCommand deposit1 = new DepositMoneyCommand();
            deposit1.AccountId = 1;
            deposit1.Ammount = 1000;
            DepositMoneyOperation depositop1 = new DepositMoneyOperation(eventSender);
            depositop1.PerformOperation(deposit1);

            WithdrawMoneyCommand withdraw1 = new WithdrawMoneyCommand();
            withdraw1.AccountId = 1;
            withdraw1.Ammount = 100;
            WithdrawMoneyOperation withdrawOp1 = new WithdrawMoneyOperation(eventSender);
            withdrawOp1.PerformOperation(withdraw1);

            CreateProductCommand product1 = new CreateProductCommand();
            product1.Name = "pc";
            product1.Currency = "RON";
            product1.Limit = 100;
            product1.Value = 10;
            CreateProductOperation product1Op = new CreateProductOperation(eventSender);
            product1Op.PerformOperation(product1);

            MultiplePurchaseCommand purchase1 = new MultiplePurchaseCommand();
            var items = new List<CommandDetails>();
            var item = new CommandDetails();
            item.ProductId = 1;
            item.Quantity = 2d;
            items.Add(item);
            purchase1.Details = items;
            PurchaseProductCommand purchase2 = new PurchaseProductCommand();
            purchase2.IdAccount = 1;
            purchase2.Command = purchase1;
            purchase2.Currency = "RON";
            purchase2.Name = "pc";
            PurchaseProductOperation purchase1Op = new PurchaseProductOperation(eventSender);
            purchase1Op.PerformOperation(purchase2);

        }
    }
}
