using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application;
using PaymentGateway.Application.Queries;
using PaymentGateway.Application.CommandHandlers;
//using PaymentGateway.Application.Services;
using PaymentGateway.Data;
using PaymentGateway.ExternalService;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static PaymentGateway.Models.MultiplePurchaseCommand;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.WebApi.MediatorPipeline;
using System.Linq;

namespace PaymentGateway
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
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

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            services.Scan(scan => scan
                .FromAssemblyOf<ListOfAccounts>()
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());


            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));
            services.AddScopedContravariant<INotificationHandler<INotification>, AllEventsHandler>(typeof(CustomerEnrolled).Assembly);
            services.AddMediatR(new[] { typeof(ListOfAccounts).Assembly, typeof(AllEventsHandler).Assembly }); // get all IRequestHandler and INotificationHandler classes

            // services.AddSingleton<IEventSender, EventSender>();
            services.AddSingleton(Configuration);

            // build
            var serviceProvider = services.BuildServiceProvider();
            var database = serviceProvider.GetRequiredService<PaymentDbContext>();

            var mediator = serviceProvider.GetRequiredService<IMediator>();


            for(int i=0;i<=100;i++)
            {
                EnrollCustomerCommand customer = new EnrollCustomerCommand();
                customer.Name= Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
                if(i%2==0)
                {
                    customer.Currency = "RON";
                    customer.ClientType = "Individual";
                    customer.AccountType = "Debit";
                }
                else
                {
                    customer.Currency = "EUR";
                    customer.ClientType = "Company";
                    customer.AccountType = "Credit";
                }
                customer.Cnp = (5000118780000 + i).ToString();
                
                await mediator.Send(customer, cancellationToken);
            }

            //EnrollCustomerCommand customer1 = new EnrollCustomerCommand
            //{
            //    Name = "Gigi",
            //    Currency = "RON",
            //    Cnp = "5000118784512",
            //    ClientType = "Individual",
            //    AccountType = "Credit"
            //};



            //// enrollCustomerOperation = serviceProvider.GetRequiredService<EnrollCustomerOperation>();
            ////enrollCustomerOperation.Handle(customer1, default).GetAwaiter().GetResult();
            //await mediator.Send(customer1, cancellationToken);


            for (int i = 0; i <= 100; i++)
            {
                CreateAccountCommand customer = new CreateAccountCommand();
                customer.Balance = 100;
                if (i % 2 == 0)
                {
                    customer.Currency = "RON";
                    customer.Type = "Individual";
                    customer.Status = "Open";
                    customer.Limit = 1000000;
                }
                else
                {
                    customer.Currency = "EUR";
                    customer.Type = "Company";
                    customer.Status = "Closed";
                    customer.Limit = 1000000000;
                }
                customer.AccountID = i;
                customer.OwnerCnp = database.Persons[i].Cnp;
                
                customer.IbanCode = (75410000 + i).ToString();
                await mediator.Send(customer, cancellationToken);
            }

            DepositMoneyCommand deposit1 = new DepositMoneyCommand();
            deposit1.AccountId = 1;
            deposit1.Ammount = 1000;
            
            await mediator.Send(deposit1, cancellationToken);


            WithdrawMoneyCommand withdraw1 = new WithdrawMoneyCommand();
            withdraw1.AccountId = 1;
            withdraw1.Ammount = 100;
            
            await mediator.Send(withdraw1, cancellationToken);


            CreateProductCommand product1 = new CreateProductCommand();
            product1.Name = "pc";
            product1.Currency = "RON";
            product1.Limit = 100;
            product1.Value = 10;

            await mediator.Send(product1, cancellationToken);


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
           
            
            await mediator.Send(purchase2, cancellationToken);



            var query = new Application.Queries.ListOfAccounts.Query
            {
                PersonId = 1,
                Cnp = "5001187845122"
            };

            var result = await mediator.Send(query, cancellationToken);

            //List<int> ana= new List<int>();
            //foreach(var myitem in database.Accounts)
            //{
            //    ana.Add(myitem.OwnerID);
            //    Console.WriteLine(myitem.OwnerID);
            //}

            //if (ana.Count != ana.Distinct().Count())
            //{
            //    Console.WriteLine("duplicates in ");
            //}

        }
    }
}
