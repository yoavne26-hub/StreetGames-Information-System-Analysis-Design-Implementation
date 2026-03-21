using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetGames
{
    public class Customer
    {
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int customerViaFormId { get; set; }

        public Customer() { }

        public Customer(int customerId, string firstName, string lastName, string phone, string email, int customerViaFormId)
        {
            this.customerId = customerId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
            this.customerViaFormId = customerViaFormId;
        }
    }
}
