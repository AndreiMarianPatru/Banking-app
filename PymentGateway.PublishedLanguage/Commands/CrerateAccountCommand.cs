﻿using MediatR;
namespace PaymentGateway.PublishedLanguage.Commands
{
    public class CreateAccountCommand : IRequest
    {

        public double Balance { get; set; }
        public string Currency { get; set; }
        public string IbanCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Limit { get; set; }
        public int AccountID { get; set; }
        public string OwnerCnp { get; set; }




    }
}
