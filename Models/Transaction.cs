using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Concerns;

namespace BankApplication.Models
{
    internal class Transaction
    {
        public string TransactionId { get; set; }
        
        public TransactionType TransactionType { get; set; }

        public string ToId { get; set; }

        public string FromId { get; set; } 

        public decimal Amount { get; set; }

        public decimal Fee { get; set; } = 0;

        public readonly DateTime TimeOfTransaction;

        public Transaction()
        {
            TimeOfTransaction = DateTime.Now;
        }

    }
}
