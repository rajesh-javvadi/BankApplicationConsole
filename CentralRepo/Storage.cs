using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.CentralRepo
{
    internal static class Storage
    {
        public static List<Bank> Banks
        {
            get;
        }
        public static List<ApplicationUser> ApplicationUsers { get; } 
        public static List<Account> Accounts { get; }

        public static List<Transaction> Transactions { get; }
        static Storage()
        {
            Banks = [];
            ApplicationUsers = [];
            Accounts = [];
            Transactions = [];
        }
    }
}
