using System.Collections.Generic;
namespace PaymentGateway.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Cnp { get; set; }
        public int Type { get; set; }
        public int PersonID { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
