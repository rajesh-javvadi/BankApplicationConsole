using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public static class UtilityServices
    {
        private static Random Random = new Random();
        public static string GetBankId(string bankName)
        {
            return (bankName.Substring(0, 3) + GetCurrentDate()).ToUpper();
        }

        public static string GetAccountId(string accountName)
        {
            return (accountName.Substring(0, 3) + GetCurrentDate()).ToUpper();
        }
        public static string GetCurrentDate()
        {
            return
                ("" + DateTime.Now.Day
                + DateTime.Now.Month
                + DateTime.Now.Year
                + DateTime.Now.Hour
                + DateTime.Now.Minute
                + DateTime.Now.Second).ToString();
        }

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_@#";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string GenerateStaffId()
        {
            return "Staff@"+RandomString();
        }

        public static string GenerateCustomerId()
        {
            return "Customer@" + RandomString();
        }

        public static string GenerateTransactionId(string bankId,string accountId)
        {
            return "TXN" + bankId + accountId + GetCurrentDate();
        }
    }
}
