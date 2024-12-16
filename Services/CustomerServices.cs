using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CentralRepo;
using BankApplication.Models;
using BankApplication.Concerns;



namespace BankApplication.Services
{
    internal class CustomerServices
    {
        public void CreateAccount(Customer customer)
        {
            Storage.Accounts.Add(customer.account);
            Storage.ApplicationUsers.Add(customer);
        }

        public bool ValidateUser(Bank bank, string username, string password, out string customerId)
        {
            bool isValid = false;
            customerId = "";
            List<ApplicationUser> applicationUsers = Storage.ApplicationUsers;
            //foreach (ApplicationUser applicationUser in applicationUsers)
            //{
            //    if (applicationUser.UserName.Equals(username) &&
            //       applicationUser.Password.Equals(password) &&
            //       applicationUser.BankId.Equals(bank.BankId) &&
            //       applicationUser.UserType == Concerns.UserType.Customer
            //        )
            //    {
            //        isValid = true;
            //        customerId = applicationUser.Id;
            //        break;
            //    }
            //}
            ApplicationUser? appUser = applicationUsers.Where(user => user.UserName.Equals(username) && user.Password.Equals(password)
            && user.BankId.Equals(bank.BankId) && user.UserType == Concerns.UserType.Customer).SingleOrDefault();

            if (appUser is not null) { customerId = appUser.Id; isValid = true; }
            return isValid;
        }

        internal decimal CheckBalance(string customerId,out bool status)
        {
            Account account = FindAccountByCustomerId(customerId);
            if ((account is null))
            {
                status = false;
                return 0;
            }
            else
            {
                status = true;
                return account.Balance;
            }
        }

        internal void DepositMoney(string customerId, decimal money,Bank selectedBank,out bool depositStatus)
        {
            Account account = FindAccountByCustomerId(customerId);
            if ((account is not null))
            {
                account.Balance += money;
                CreateTransaction(account, selectedBank, TransactionType.Deposit, money);
                depositStatus = true;
                
            }
            else
            { 
                depositStatus =false;
            }
        }

        private void CreateTransaction(Account account,Bank selectedBank,TransactionType transactionType,decimal money)
        {
            Transaction transaction = new Transaction()
            {
                TransactionType = transactionType,
                ToId = account.AccountId,
                FromId = account.AccountId,
                Amount = money,
                TransactionId = UtilityServices.GenerateTransactionId(selectedBank.BankId, account.AccountId)
            };
            Storage.Transactions.Add(transaction);
        }

        internal ApplicationUser FetchCustomerByAccount(Bank selectedBank,string accountId)
        {
            Account? account = FindAccountByAccountId(accountId);
            if (account is not null)
            {
                ApplicationUser? customer = Storage.ApplicationUsers.Where(cust => cust.Id.Equals(account.CustomerId)).SingleOrDefault();
                if (customer is not null)
                {
                    if (customer.Id.Equals(account.CustomerId) && customer.BankId.Equals(selectedBank.BankId))
                    {
                        return customer;
                    }
                }
            }
            return null;
        }

        private Account FindAccountByAccountId(string accountId)
        {
            Account? account = Storage.Accounts.Where(ac =>  ac.AccountId.Equals(accountId)).SingleOrDefault();
            return account;
        }

        public Account FindAccountOfSameBank(Bank selectedBank,string accountId)
        {
            Account account = FindAccountByAccountId(accountId);
            if(account is not null)
            {
                ApplicationUser applicationUser = FetchCustomerByAccount(selectedBank, accountId);
                if (applicationUser is not null)
                {
                    if(applicationUser.BankId.Equals(selectedBank.BankId))
                    {
                        return account;
                    }
                }
            }
            return null;
        }

        internal bool WithdrawMoney(string customerId, decimal money,Bank selectedBank)
        {
            Account account = FindAccountByCustomerId(customerId);
            if (account is not null)
            {
                if (account.Balance >= money)
                {
                    account.Balance -= money;

                    CreateTransaction(account, selectedBank,TransactionType.Withdrawl,money);
                    return true;
                }
            }

            return false;
        }

        public Account FindAccountByCustomerId(string customerId)
        {
            List<Account> accounts = Storage.Accounts;
            //foreach (Account account in accounts)
            //{
            //    if (account.CustomerId.Equals(customerId))
            //    {
            //       return  account;
            //    }
            //}

            //Account? Fetchedaccount = (from account in accounts
            //          where account.CustomerId.Equals(customerId)
            //                          select account).SingleOrDefault();


            //Account? ac = accounts.Select(ac => ac.CustomerId.Equals(customerId) ? ac : null).SingleOrDefault();

            Account? account = accounts.Where(ac => ac.CustomerId.Equals(customerId)).SingleOrDefault();
            return account;
        }

        internal void UpdateCustomerDetails(ApplicationUser applicationUser, string name)
        {
            applicationUser.Name = name;
        }

        internal bool DeleteAccount(Bank selectedBank,string accountId)
        {
            Account account = FindAccountByAccountId(accountId);
            if (account is not null)
            {
                Storage.Accounts.Remove(account);
                return true;
            }
            return false;
        }

        internal List<Transaction> GetTransactions()
        { return Storage.Transactions; }

        internal List<Transaction> GetTransactionHistory(string accountId)
        {
            List<Transaction> transactions = Storage.Transactions.Where(transaction => (transaction.ToId.Equals(accountId)) || (transaction.FromId.Equals(accountId))).ToList();
            return transactions;
        }

        public bool BankToBankTransfer(Bank bank,string customerId,string receiverAccountId,TransactionType transactionType,decimal money)
        {
            bool bankTransferStatus = false;
            List<ApplicationUser> users = Storage.ApplicationUsers;
            Account account = FindAccountByCustomerId(customerId);
            Account recieverAccount = FindAccountOfSameBank(bank, receiverAccountId);
            if(account.AccountId == receiverAccountId)
            {
                bankTransferStatus = false;
                return bankTransferStatus;
            }
            else if(recieverAccount is null)
            {
                bankTransferStatus = false;
            }
            else
            {
                if(account.Balance >= money)
                {
                    CreateTransactionForTransferFunds(bank, account, recieverAccount, transactionType, money, true);
                    bankTransferStatus = true;
                }
            }
            return bankTransferStatus; 
        }

        public bool BankToOtherBankTransfer(Bank bank, string customerId, string receiverAccountId, TransactionType transactionType,decimal money)
        {
            bool bankTransferStatus = false;
            Account account = FindAccountByCustomerId(customerId);
            Account receiverAccount = FindAccountByAccountId(receiverAccountId);
            List<ApplicationUser> users = Storage.ApplicationUsers;
            Customer c1 = GetCustomer(customerId);
            Customer c2 = GetCustomer(receiverAccount.CustomerId);
            if(c1 is not null && c2 is not null)
            {
                if(c1.BankId != c2.BankId )
                {
                    if(account.Balance >= money)
                    {
                        CreateTransactionForTransferFunds(bank, account, receiverAccount, transactionType, money, false);
                        bankTransferStatus = true;
                    }

                }
            }
            return bankTransferStatus;
        }

        private void CreateTransactionForTransferFunds(Bank bank, Account senderAccount, Account receiverAccount, TransactionType transactionType, decimal money, bool bankToBankTransfer)
        {
            Transaction transaction = new Transaction()
            {
                FromId = senderAccount.AccountId,
                ToId = receiverAccount.AccountId,
                TransactionId = UtilityServices.GenerateTransactionId(bank.BankId, senderAccount.AccountId),
                TransactionType = transactionType,
                Amount = money,
            };
            if (transactionType == TransactionType.IMPS)
            {
                if(!bankToBankTransfer) 
                transaction.Fee = ((decimal)bank.OtherBankImpsCharges / 100) * money;
                else 
                transaction.Fee = ((decimal)bank.SameBankImpsCharges /100) * money;
            }
            else if (transactionType == TransactionType.RTGS)
            {
                if (!bankToBankTransfer)
                    transaction.Fee = ((decimal)bank.OtherBankRtgsCharges / 100) * money;
                else
                    transaction.Fee = ((decimal)bank.SameBankRtgsCharges / 100) * money;
            }
            senderAccount.Balance -= money;
            money -= transaction.Fee;

            receiverAccount.Balance += money;
            Storage.Transactions.Add(transaction);
        }
        
        public int GetCustomersCount(Bank bank)
        {
            int count = 0;
            foreach(var user in Storage.ApplicationUsers)
            {
                if(user is Customer customer)
                {
                    if (customer.BankId.Equals(bank.BankId)) count++;
                }
            }
            return count;
        }
        private Customer GetCustomer(string customerId)
        {
            foreach (var user in Storage.ApplicationUsers)
            {
                if(user is Customer cust)
                {
                    if (cust.Id == customerId) return cust;
                }
            }
            return null;
        }

        internal bool RevertTransaction(string accountId, uint option,Bank selectedBank)
        {
            var transactions = GetTransactionHistory(accountId);
            int record = 1;
            Transaction transaction = null;
            foreach (var tran in transactions)
            {
                if(record == option)
                {
                    transaction = tran; break;
                }
                record++;
            }
            if(transaction is not null)
            {
                Transaction revertedTransaction = new Transaction()
                {
                    TransactionId = UtilityServices.GenerateTransactionId(selectedBank.BankId, accountId),
                    TransactionType = TransactionType.REVERT,
                    Amount = transaction.Amount,
                    FromId = transaction.ToId,
                    ToId = transaction.FromId,
                };
                Account fromAccount = FindAccountByAccountId(transaction.FromId);
                Account toAccount = FindAccountByAccountId(transaction.ToId);
                fromAccount.Balance += transaction.Amount;
                toAccount.Balance -= transaction.Amount;
                toAccount.Balance += transaction.Fee;
                Storage.Transactions.Add(revertedTransaction);
                return true;
            }
            return false;
        }
    }
}

