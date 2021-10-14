using PaymentGateway.Abstractions;
using PaymentGateway.Application.WriteOperations;
using PaymentGateway.ExternalService;
using PaymentGateway.Models;


using System.Collections.Generic;
using static PaymentGateway.Models.MultiplePurchaseCommand;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application;

using System.IO;
using System;
using PaymentGateway.PublishedLanguage.Commands;
using PaymentGateway.Application.Queries;

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


            EnrollCustomerCommand customer1 = new EnrollCustomerCommand
            {
                Name = "Gigi",
                Currency = "$",
                Cnp = "5000118784512",
                ClientType = "Individual",
                AccountType = "Credit"
            };

            IEventSender eventSender = new EventSender();

            var enrollCustomerOperation = serviceProvider.GetRequiredService<EnrollCustomerOperation>();
            enrollCustomerOperation.Handle(customer1, default).GetAwaiter().GetResult();

            CreateAccountCommand account1 = new CreateAccountCommand();
            account1.Currency = "RON";
            account1.Limit = 1000000.00;
            account1.Status = "Open";
            account1.Type = "Current";
            account1.OwnerCnp = "5000118784512";

            var makeAccountOperation = serviceProvider.GetRequiredService<CreateAccountOperation>();
            makeAccountOperation.Handle(account1, default).GetAwaiter().GetResult();

            DepositMoneyCommand deposit1 = new DepositMoneyCommand();
            deposit1.AccountId = 1;
            deposit1.Ammount = 1000;
            var makeDeposit = serviceProvider.GetRequiredService<DepositMoneyOperation>();
            makeDeposit.Handle(deposit1, default).GetAwaiter().GetResult();

            WithdrawMoneyCommand withdraw1 = new WithdrawMoneyCommand();
            withdraw1.AccountId = 1;
            withdraw1.Ammount = 100;
            var makeWithdraw = serviceProvider.GetRequiredService<WithdrawMoneyOperation>();
            makeWithdraw.Handle(withdraw1, default).GetAwaiter().GetResult();

            CreateProductCommand product1 = new CreateProductCommand();
            product1.Name = "pc";
            product1.Currency = "RON";
            product1.Limit = 100;
            product1.Value = 10;

            var product1Op = serviceProvider.GetRequiredService<CreateProductOperation>();

         
            product1Op.Handle(product1,default).GetAwaiter().GetResult();

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
            purchaseProduct.Handle(purchase2, default).GetAwaiter().GetResult();



            var query = new Application.Queries.ListOfAccounts.Query
            {
                PersonId = 1
            };

            var handler = serviceProvider.GetRequiredService<ListOfAccounts.QueryHandler>();
            var result = handler.Handle(query, default).GetAwaiter().GetResult();

        }
    }
}
