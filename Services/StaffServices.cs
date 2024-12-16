using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CentralRepo;
using BankApplication.Concerns;
using BankApplication.Models;

namespace BankApplication.Services
{
    internal class StaffServices
    {
        CustomerServices customerServices;
        public StaffServices()
        {
            customerServices = new();
        }
        public void CreateStaff(Staff staff)
        {
            Storage.ApplicationUsers.Add(staff);
        }

        public bool ValidateStaff(Bank selectedBank, string? userName, string? password)
        {
            bool isValid = false;

            var ApplicationsUsersList = Storage.ApplicationUsers;

            foreach (var appUser in ApplicationsUsersList)
            {
                if (appUser.UserName.Equals(userName) && appUser.Password.Equals(password) && appUser.UserType == UserType.Staff
                    && appUser.BankId.Equals(selectedBank.BankId))
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        public void AddRtgsChargesForSameBank(Bank bank,uint percentage)
        {
            bank.SameBankRtgsCharges = percentage;
        }

        internal void AddImpsChargesForSameBank(Bank selectedBank, uint imps)
        {
            selectedBank.SameBankImpsCharges = imps;
        }

        internal void AddRtgsChargesForOtherBank(Bank selectedBank, uint rtgs)
        {
            selectedBank.OtherBankRtgsCharges = rtgs;
        }

        internal void AddImpsChargesForOtherBank(Bank selectedBank, uint imps)
        {
            selectedBank.OtherBankImpsCharges = imps;
        }

        internal void AddExchangeRate(Bank selectedBank, string currency, uint value)
        {
            selectedBank.CurrenyRates.Add(currency, value);
        }
    }
}
