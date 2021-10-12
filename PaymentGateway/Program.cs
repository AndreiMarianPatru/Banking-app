using PaymentGateway.Abstractions;
using PaymentGateway.Application.WriteOperations;
using PaymentGateway.ExternalService;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.WriteSide;
using PaymentGateway.WriteSide;
using System.Collections.Generic;
using static PaymentGateway.Models.MultiplePurchaseCommand;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application;
using PaymentGateway.Application.ReadOperations;
using System.IO;
using System;

namespace PaymentGateway
{
    class Program
    {
        static IConfiguration Configuration;
        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // setup
            var services = new ServiceCollection();
            services.RegisterBusinessServices(Configuration);

            services.AddSingleton<IEventSender, EventSender>();
            services.AddSingleton(Configuration);

            // build
            var serviceProvider = services.BuildServiceProvider();


            EnrollCustomerCommand customer1 = new EnrollCustomerCommand();
            customer1.Name = "Gigi";
            customer1.Currency = "$";
            customer1.Cnp = "5000118784512";
            customer1.ClientType = "Individual";
            customer1.AccountType = "Credit";

            IEventSender eventSender = new EventSender();

            var enrollCustomerOperation = serviceProvider.GetRequiredService<EnrollCustomerOperation>();
            enrollCustomerOperation.PerformOperation(customer1);

            CrerateAccountCommand account1 = new CrerateAccountCommand();
            account1.Currency = "RON";
            account1.Limit = 1000000.00;
            account1.Status = "Open";
            account1.Type = "Current";
            account1.OwnerCnp = "5000118784512";

            var makeAccountOperation = serviceProvider.GetRequiredService<CreateAccountOperation>();
            makeAccountOperation.PerformOperation(account1);

            DepositMoneyCommand deposit1 = new DepositMoneyCommand();
            deposit1.AccountId = 1;
            deposit1.Ammount = 1000;
            var makeDeposit = serviceProvider.GetRequiredService<DepositMoneyOperation>();
            makeDeposit.PerformOperation(deposit1);

            WithdrawMoneyCommand withdraw1 = new WithdrawMoneyCommand();
            withdraw1.AccountId = 1;
            withdraw1.Ammount = 100;
            var makeWithdraw = serviceProvider.GetRequiredService<WithdrawMoneyOperation>();
            makeWithdraw.PerformOperation(withdraw1);

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
            var purchaseProduct = serviceProvider.GetRequiredService<PurchaseProductOperation>();
            purchaseProduct.PerformOperation(purchase2);

        }
    }
}
