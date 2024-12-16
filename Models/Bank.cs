using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    internal class Bank
    {
        public string BankId { get; set; }
        public string BankName { get; set; }

        public uint SameBankRtgsCharges { get; set; } = 0;

        public uint OtherBankRtgsCharges { get; set; } = 2;

        public uint SameBankImpsCharges { get; set; } = 5;

        public uint OtherBankImpsCharges { get; set; } = 6;


        public SortedDictionary<string, uint> CurrenyRates { get; }

        public Bank()
        {
            CurrenyRates = new()
            {
                {"INR",1 }
            };
        }

        public override string ToString()
        {
            return BankName ;
        }
    }
}
