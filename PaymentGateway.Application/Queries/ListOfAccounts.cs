﻿using FluentValidation;
using MediatR;
using PaymentGateway.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using static PaymentGateway.Application.Queries.ListOfAccounts.Validator;

namespace PaymentGateway.Application.Queries
{
    public class ListOfAccounts
    {
        public class Model
        {
            public int Id { get; set; }
            public double Balance { get; set; }
            public string Currency { get; set; }
            public string Iban { get; set; }
            public string Status { get; set; }
            public double Limit { get; set; }
            public string Type { get; set; }
        }
        public class Query : IRequest<List<Model>>
        {
            public int? PersonId { get; set; }
            public string Cnp { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {


            public Validator(Database _database)

            {
                RuleFor(q => q).Must(query =>
                {
                    var person = query.PersonId.HasValue ?
                    _database.Persons.FirstOrDefault(x => x.PersonID == query.PersonId) :
                    _database.Persons.FirstOrDefault(x => x.Cnp == query.Cnp);

                    return person != null;
                }).WithMessage("Customer not found");

            }

            public class Validator2 : AbstractValidator<Query>
            {
                public Validator2(Database _database)
                {
                    RuleFor(q => q).Must(query =>
                    {
                        var person = query.PersonId.HasValue ?
                        _database.Persons.FirstOrDefault(x => x.PersonID == query.PersonId) :
                        _database.Persons.FirstOrDefault(x => x.Cnp == query.Cnp);
                        return person != null;
                    }).WithMessage("Customer not found");

                }
            }
           

            public class QueryHandler : IRequestHandler<Query, List<Model>>
            {
                private readonly Database _database;


                public QueryHandler(Database database)
                {
                    _database = database;

                }

                public Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
                {


                    var person = request.PersonId.HasValue ?
                       _database.Persons.FirstOrDefault(x => x.PersonID == request.PersonId) :
                       _database.Persons.FirstOrDefault(x => x.Cnp == request.Cnp);

                    var db = _database.Accounts.Where(x => x.OwnerCnp == person.Cnp);
                    var result = db.Select(x => new Model
                    {
                        Balance = x.Balance,
                        Currency = x.Currency,
                        Iban = x.IbanCode,
                        Id = x.AccountID,
                        Limit = x.Limit,
                        Status = x.Status,
                        Type = x.Type
                    }).ToList();
                    return Task.FromResult(result);
                }
            }

           
        }
    }
}
