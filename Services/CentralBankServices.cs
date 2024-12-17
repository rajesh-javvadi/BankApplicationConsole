using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CentralRepo;
using BankApplication.Models;

namespace BankApplication.Services
{
    internal class CentralBankServices
    {

        public CentralBankServices()
        {
           
        }

        public List<Bank> GetBanks()
        {
            return Storage.Banks;
        }

        public void AddBank(Bank bank)
        {
            Storage.Banks.Add(bank);
        }
    }
}
