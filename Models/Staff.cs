using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    internal class Staff : ApplicationUser
    {
        public Staff() 
        {
            this.UserType = Concerns.UserType.Staff;
        }
    }
}
