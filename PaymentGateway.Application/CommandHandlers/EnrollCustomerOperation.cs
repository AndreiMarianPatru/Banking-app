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

        private readonly PaymentDbContext _dbContext;
        public EnrollCustomerOperation(IMediator mediator, PaymentDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(EnrollCustomerCommand request, CancellationToken cancellationToken)
        {

        
            var random = new Random();
          
            Person person = new Person();
            person.Cnp = request.Cnp;
            person.Name = request.Name;
            
            if (request.ClientType == "Company")
                person.Type = (int)PersonType.Company;
            else if (request.ClientType == "Individual")
                person.Type = (int)PersonType.Individual;
            else
                throw new Exception("Unsupported Type");
            //person.PersonId = _dbContext.Persons.Count() + 1;


            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();

            Account account = new Account();
            account.Type = request.AccountType;
            account.Currency = request.Currency;
            account.Balance = 0;
            account.IbanCode = random.Next(1000000).ToString();
            //account.AccountId = _dbContext.Accounts.Count() + 1;
            account.OwnerCnp = person.Cnp;
            account.OwnerId = person.PersonId;
            account.Status = 1;
            account.Limit = 10000;
            _dbContext.Accounts.Add(account);

            _dbContext.SaveChanges();
            CustomerEnrolled eventCustEnroll = new(request.Name, request.Cnp, request.ClientType);
            await _mediator.Publish(eventCustEnroll, cancellationToken);
            return Unit.Value;


        }
    }
}
