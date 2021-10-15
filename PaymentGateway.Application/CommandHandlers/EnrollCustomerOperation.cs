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
    public class EnrollCustomerOperation : IRequestHandler<EnrollCustomerCommand>
    {
        private readonly IMediator _mediator;

        private readonly Database _database;
        public EnrollCustomerOperation(IMediator mediator, Database database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<Unit> Handle(EnrollCustomerCommand request, CancellationToken cancellationToken)
        {

            //var Database = new Database();
            var random = new Random();
          
            Person person = new Person();
            person.Cnp = request.Cnp;
            person.Name = request.Name;
            //person.Type = operation.ClientType;
            if (request.ClientType == "Company")
                person.Type = (int)PersonType.Company;
            else if (request.ClientType == "Individual")
                person.Type = (int)PersonType.Individual;
            else
                throw new Exception("Unsupported Type");
            person.PersonID = _database.Persons.Count + 1;


            _database.Persons.Add(person);

            Account account = new Account();
            account.Type = request.AccountType;
            account.Currency = request.Currency;
            account.Balance = 0;
            account.IbanCode = random.Next(1000000).ToString();
            account.AccountID = _database.Accounts.Count() + 1;
            _database.Accounts.Add(account);

            _database.SaveChange();
            CustomerEnrolled eventCustEnroll = new(request.Name, request.Cnp, request.ClientType);
            await _mediator.Publish(eventCustEnroll, cancellationToken);
            return Unit.Value;


        }
    }
}
