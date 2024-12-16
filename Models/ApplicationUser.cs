using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Concerns;

namespace BankApplication.Models
{
    internal abstract class ApplicationUser
    { 
        public string Id { get; set; } 
        public string BankId { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; } 
        
        public UserType UserType { get; set; }
       
    }
}
