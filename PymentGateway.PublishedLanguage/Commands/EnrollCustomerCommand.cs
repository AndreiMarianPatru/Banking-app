using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace PaymentGateway.PublishedLanguage.Commands
{
    public class EnrollCustomerCommand : IRequest
    {
        public string Name { get; set; }
        public string Cnp { get; set; }
        public string ClientType { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
    }
}
