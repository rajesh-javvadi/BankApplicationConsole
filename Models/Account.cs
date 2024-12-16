using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    internal class Account 
    {
        public string AccountId { get; set; }

        public decimal Balance { get; set; }

        public string CustomerId { get; set; }
    }
}
