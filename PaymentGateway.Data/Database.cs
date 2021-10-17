using PaymentGateway.Models;
using System;
using System.Collections.Generic;

namespace PaymentGateway.Data
{
    public class PaymentDbContext
    {
        public List<Person> Persons = new List<Person>();
        public List<Account> Accounts = new List<Account>();
        public List<Product> Products = new List<Product>();
        public List<Transaction> Transactions = new List<Transaction>();
        public List<ProductXTransaction> pxt = new List<ProductXTransaction>();

      

        public void SaveChange()
        {
            Console.WriteLine("Saved to database");
        }




    }
}
