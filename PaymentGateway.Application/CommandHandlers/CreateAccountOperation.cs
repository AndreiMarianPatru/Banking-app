
using PaymentGateway.Data;
using PaymentGateway.Models;
using PaymentGateway.PublishedLanguage.Events;
using PaymentGateway.PublishedLanguage.Commands;
using System;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using System.Threading;


namespace PaymentGateway.Application.CommandHandlers
{
    public class CreateAccountOperation : IRequestHandler<CreateAccountCommand>
    {
        private readonly IMediator _mediator;
        private readonly AccountOptions _accountOptions;
        private readonly PaymentDbContext _dbContext;


        public CreateAccountOperation(IMediator mediator, AccountOptions accountOptions, PaymentDbContext dbContext)
        {
            _mediator = mediator;
            _accountOptions = accountOptions;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = _dbContext.Persons.FirstOrDefault(e => e.Cnp == request.OwnerCnp);
            if (user == null)
            {
                throw new Exception("User invalid");
            }
            var random = new Random();
            
            Account account = new Account();
            account.AccountId = _dbContext.Accounts.Count() + 1;
            account.Balance = 0;
            account.Currency = request.Currency;
            account.IbanCode = string.IsNullOrEmpty(request.IbanCode) ? random.Next(1000000).ToString() : request.IbanCode;
            account.Limit = request.Limit;
            account.Status = request.Status;
            account.Type = request.Type;
            account.OwnerCnp = request.OwnerCnp;
            account.OwnerId = _dbContext.Persons.Count();
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            AccountCreated eventCreateAccount = new(request.Balance, request.Currency, request.IbanCode, request.Type, request.Status, request.Limit, request.AccountID, request.OwnerCnp);
            await _mediator.Publish(eventCreateAccount, cancellationToken);
            return Unit.Value;

        }

     
    }
}
