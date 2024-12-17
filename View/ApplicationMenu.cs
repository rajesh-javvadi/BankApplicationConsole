using BankApplication.Concerns;
using BankApplication.Models;
using BankApplication.Services;

namespace BankApplication.View
{
    internal class ApplicationMenu
    {
        readonly InputValidation inputValidation = new InputValidation();

        readonly CentralBankServices centralBankServices = new CentralBankServices();

        readonly StaffServices staffServices = new StaffServices();

        readonly CustomerServices customerServices = new CustomerServices();

        public void StartMenu()
        {
            bool stopLoop = false;
            do
            {
                Console.WriteLine(Constants.WelcomeMessage);
                Console.WriteLine(Constants.BankSetupMessage);
                Console.WriteLine(Constants.ExistingBanksMessage);
                Console.WriteLine("3.Exit");
                uint option;
                Console.WriteLine(Constants.OptionMessage);
                inputValidation.ValidateNumberFormat(out option);
                switch (option)
                {
                    case 1:
                        SetUpNewBank();
                        break;
                    case 2:
                        LoadExistingBanks();
                        break;
                    case 3:
                        stopLoop = true;
                        break;
                    default:
                        Console.WriteLine(Constants.NotValidMessage);
                        break;
                }
            } while (!stopLoop);

        }

        private void LoadExistingBanks()
        {
            List<Bank> banks = centralBankServices.GetBanks();
            int count = 1;
            foreach (Bank bank in banks)
            {
                Console.WriteLine($"{count}. {bank}");
                count++;
            }
            ChooseBank();
        }

        private void ChooseBank()
        {
            List<Bank> banks = centralBankServices.GetBanks();
            if(banks.Count > 0)
            {
                uint option;
                do
                {
                    Console.WriteLine(Constants.OptionMessage);
                    Console.WriteLine("Press 0 to Exit");
                    inputValidation.ValidateNumberFormat(out option);
                    if (option == 0) return;
                    int count = 1;
                    Bank? selectedBank = null;
                    foreach (Bank bank in banks)
                    {
                        if (count == option)
                        {
                            selectedBank = bank;
                        }
                        count++;
                    }
                    if (selectedBank is null)
                    {
                        Console.WriteLine(Constants.NotValidMessage);
                    }
                    else
                    {
                        ChooseUserType(selectedBank);
                    }
                } while (option != 0);
               
            }
            else
            {
                Console.WriteLine(Constants.CreateNewBankMessage);
            }
            
        }

        private void ChooseUserType(Bank selectedBank)
        {
            bool stopLoop = false;
            do
            {
                uint option;
                Console.WriteLine(Constants.OptionMessage);
                Console.WriteLine(Constants.LoginAsAdminMessage);
                Console.WriteLine(Constants.LoginAsUserMessage);
                Console.WriteLine("3.Exit");
                inputValidation.ValidateNumberFormat(out option);
                switch (option)
                {
                    case 1:
                        LoginStaff(selectedBank);
                        break;
                    case 2:
                        LoginUser(selectedBank);
                        break;
                    case 3:
                        stopLoop = true;
                        break;
                    default:
                        Console.WriteLine(Constants.NotValidMessage); break;
                }
            } while (!stopLoop);
        }

        private void LoginUser(Bank selectedBank)
        {
            if (!CheckCustomerExists(selectedBank)) return;
            string username;
            string password;
            GetUserNameandPassword(out username, out password);
            string customerId;
            if (customerServices.ValidateUser(selectedBank, username, password, out customerId))
            {
                ListOutUserOperations(customerId,selectedBank);
            }
            else
            {
                Console.WriteLine("User doen't Exist with this Username and Password");
            }
        }
        private void ListOutOperations(Bank selectedBank)
        {
            bool stopLoop = false;
            do
            {
                Console.WriteLine(Constants.OptionMessage);
                Console.WriteLine(Constants.CreateUeserMessage);
                Console.WriteLine(Constants.UpdateAccountMessage);
                Console.WriteLine(Constants.DeleteAccountMessage);
                Console.WriteLine(Constants.AddChargesForSameBank);
                Console.WriteLine(Constants.AddChargesForOtherBank);
                Console.WriteLine(Constants.AddExchangeRate);
                Console.WriteLine(Constants.ViewTransactionHistory);
                Console.WriteLine(Constants.RevertAnyTransaction);
                Console.WriteLine(Constants.StaffExit);
                uint option;
                Console.WriteLine(Constants.OptionMessage);
                inputValidation.ValidateNumberFormat(out option);
                switch (option)
                {
                    case 1:
                        CreateAccount(selectedBank);
                        break;
                    case 2:
                        UpdateAccount(selectedBank);
                        break;
                    case 3:
                        DeleteAccount(selectedBank);
                        break;
                    case 4:
                        AddChargesForSameBank(selectedBank);
                        break;
                    case 5:
                        AddChargesForOtherBank(selectedBank);
                        break;
                    case 6:
                        AddExchangeRate(selectedBank);
                        break;
                    case 7:
                        ViewTransactionHistoryByAdmin(selectedBank);
                        break;
                    case 8:
                        RevertTransaction(selectedBank);
                        break;
                    case 9:
                        stopLoop = true;
                        break;
                }
            } while (!stopLoop);
        }

        private void RevertTransaction(Bank selectedBank)
        {
            Console.WriteLine(Constants.EnterAccountIdMessage);
            string accountId;
            inputValidation.ValidateString(out accountId);
            Account account = customerServices.FindAccountOfSameBank(selectedBank,accountId);
            if(account is null)
            {
                Console.WriteLine(Constants.AccountNotFoundMessage);
            }
            else
            {
                ViewTransactionHistory(accountId);
                int historyCount = customerServices.GetTransactionHistory(accountId).Count;
                if (historyCount == 0) return;
                uint option;
                do
                {
                    Console.WriteLine(Constants.SelectTransactionMessage);
                    inputValidation.ValidateNumberFormat(out option);
                    if(option > historyCount)
                    Console.WriteLine(Constants.ChooseTransactionMessage);
                } while (option > historyCount);
                List<Transaction> transactions = customerServices.GetTransactionHistory(accountId);
                int record = 1;
                bool validTransactionType = true;
                foreach(var transaction in transactions)
                {
                    if(option == record)
                    {
                        if (transaction.TransactionType == TransactionType.Deposit)
                        {
                            Console.WriteLine(Constants.CannotRevertDeposit);
                            validTransactionType = false;
                        }
                        else if (transaction.TransactionType == TransactionType.Withdrawl)
                        {
                            Console.WriteLine(Constants.CannotRevertWithdrawl);
                            validTransactionType = false;
                        }
                    }
                    record++;
                }
                if (validTransactionType)
                {
                    bool transactionStatus = customerServices.RevertTransaction(accountId, option, selectedBank);
                    if(transactionStatus) Console.WriteLine(Constants.RevertSuccess);
                }
                else
                {
                    Console.WriteLine(Constants.RevertUnSuccess);
                }
            }

        }

        private void ViewTransactionHistoryByAdmin(Bank selectedBank)
        {
            string accountId;
            Console.WriteLine(Constants.EnterAccountIdMessage);
            inputValidation.ValidateString(out accountId);
            Account ac = customerServices.FindAccountOfSameBank(selectedBank, accountId);
            if (ac is not null)
            {
                ViewTransactionHistory(accountId);
            }
            else
            {
                Console.WriteLine(Constants.AccountNotFoundMessage);
            }
        }

        private void ViewTransactionHistory(string accountId)
        {
            List<Transaction> transactionHistory = customerServices.GetTransactionHistory(accountId);
            if (transactionHistory.Count > 0)
            {
                Console.WriteLine(Constants.TransactionHistory);
                foreach (Transaction transaction in transactionHistory)
                {
                    if (transaction.FromId.Equals(transaction.ToId))
                    {
                        string value = transaction.TransactionType == TransactionType.Deposit ? Constants.Credited : Constants.Debited;
                        Console.WriteLine($"{transaction.TransactionId} {transaction.TimeOfTransaction} {transaction.TransactionType} {transaction.Amount} {value}");
                    }
                    else if (transaction.FromId.Equals(accountId))
                    {
                        Console.WriteLine($"{transaction.TransactionId} {transaction.TimeOfTransaction} {transaction.TransactionType} -{transaction.Amount} Debited {transaction.Fee}");
                    }
                    else if (transaction.ToId.Equals(accountId))
                    {
                        Console.WriteLine($"{transaction.TransactionId} {transaction.TimeOfTransaction} {transaction.TransactionType} +{transaction.Amount - transaction.Fee} Credited");
                    }
                }
            } else Console.WriteLine(Constants.NoTransactionHistory);
        }

        private void ListOutUserOperations(string customerId,Bank selectedBank)
        {
            bool stopLoop = false;
            do
            {
                Console.WriteLine(Constants.DepositMoney);
                Console.WriteLine(Constants.WithdrawMoney);
                Console.WriteLine(Constants.TransferFunds);
                Console.WriteLine(Constants.ViewTransactionHistoryUser);
                Console.WriteLine(Constants.CheckBalance);
                Console.WriteLine(Constants.UserExit);
                stopLoop = ChooseUserOperation(customerId,selectedBank);
            } while (!stopLoop);

        }

        private bool ChooseUserOperation(string customerId,Bank selectedBank)
        {
            Console.WriteLine(Constants.OptionMessage);
            uint option;
            decimal money;
            inputValidation.ValidateNumberFormat(out option);
            bool stopLoop = false;
            bool withdrawlSuccess = false;
            switch (option)
            {
                case 1:
                    uint value = ListOutExchangeRates(selectedBank);
                    bool depositStatus;
                    money = GetMoney();
                    money = money * value;
                    customerServices.DepositMoney(customerId, money,selectedBank,out depositStatus);
                    if(depositStatus) Console.WriteLine(Constants.DepositSuccess);
                    else Console.WriteLine(Constants.DepositFailure);
                    break;
                case 2:
                    money = GetMoney();
                    withdrawlSuccess = customerServices.WithdrawMoney(customerId, money,selectedBank);
                    if (!withdrawlSuccess) Console.WriteLine(Constants.WithdrawFailure);
                    else Console.WriteLine(Constants.WithdrawlSuccess);
                    break;
                case 3:
                    TransferFunds(customerId, selectedBank);
                    break;
                case 4:
                    ViewTransactionHistoryByUser(customerId, selectedBank);
                    break;
                case 5:
                    bool checkStatus = false;
                    money = customerServices.CheckBalance(customerId,out checkStatus);
                    if(checkStatus) Console.WriteLine(String.Format(Constants.TotalBalance,money));
                    else Console.WriteLine(Constants.CannotAbleToFetchBalance);
                    break;
                case 6:
                    stopLoop = true;
                    break;
            }
            return stopLoop;
        }

        private void TransferFunds(string customerId, Bank selectedBank)
        {
            Console.WriteLine(Constants.ChooseTransferType);
            //inputValidation.ValidateString(out recieverAccountId);
            Console.WriteLine(Constants.RTGS);
            Console.WriteLine(Constants.IMPS);
            uint option;
            Console.WriteLine(Constants.OptionMessage);
            inputValidation.ValidateNumberFormat(out option);
            switch(option)
            {
                case 1:
                    TransferTransaction(customerId, selectedBank,TransactionType.RTGS);  
                    break;
                case 2:
                    TransferTransaction(customerId, selectedBank, TransactionType.IMPS);
                    break;
                default:
                    Console.WriteLine(Constants.NotValidMessage);
                    break;
            }
        }

        private void TransferTransaction(string customerId, Bank selectedBank,TransactionType transactionType)
        { 
            uint option;
            Response transactionStatus;
            Console.WriteLine(Constants.OptionMessage);
            SelectBankTransferType(out option);
            switch(option)
            {
                case 1:
                    string rAcId = AskAccountId();
                    bool userExistsInCurrentBank = UserExistInSameBank(selectedBank,rAcId);
                    if(userExistsInCurrentBank)
                    {
                        transactionStatus = customerServices.BankToBankTransfer(selectedBank, customerId, rAcId, transactionType,GetMoney());
                        Console.WriteLine(transactionStatus.ResponseMessage);
                    }
                    else Console.WriteLine(Constants.UnableToFetchAccount);
                    break;
                case 2:
                     rAcId = AskAccountId();
                    transactionStatus = customerServices.BankToOtherBankTransfer(selectedBank, customerId, rAcId, transactionType, GetMoney());
                    Console.WriteLine(transactionStatus.ResponseMessage);
                    break;
                default:
                    Console.WriteLine(Constants.NotValidMessage);
                    break;
            }
        }

        bool UserExistInSameBank(Bank selectedBank, string rAcId)
        {
            return customerServices.FindAccountOfSameBank(selectedBank, rAcId) is null ? false : true;
        }
        string AskAccountId()
        {
            string accountId;
            Console.WriteLine(Constants.RecieverId);
            inputValidation.ValidateString(out accountId);
            return accountId;
        }

        void SelectBankTransferType(out uint option)
        {
            Console.WriteLine(Constants.ChooseTransferType);
            Console.WriteLine(Constants.BankToBank);
            Console.WriteLine(Constants.BankToAnotherBank);
            inputValidation.ValidateNumberFormat(out option);

        }

        private void ViewTransactionHistoryByUser(string customerId, Bank selectedBank)
        {
            Account ac = customerServices.FindAccountByCustomerId(customerId);
            if(ac is not null)
            {
                ViewTransactionHistory(ac.AccountId);
            }
            else
            {
                Console.WriteLine(Constants.SomethingWrongInFindingAccount);
            }
        }

        decimal GetMoney()
        {
            decimal money;
            Console.WriteLine(Constants.EnterAmount);
            money = Convert.ToDecimal(Console.ReadLine());
            return money;
        }

        uint ListOutExchangeRates(Bank bank)
        {
            var exchangeRates = bank.CurrenyRates;
            int count = 1;
            foreach (var exchangeRate in exchangeRates)
            {
                Console.WriteLine(count + ". " +exchangeRate.Key);
            }
            uint option;
            
            do
            {
                Console.WriteLine(Constants.OptionMessage);
                inputValidation.ValidateNumberFormat(out option);

            } while (!(option <= exchangeRates.Count));
           
            int record = 1;
            uint exchangeValue = 1;
            foreach (var exchangeRate in exchangeRates)
            {
                if(option == record)
                {
                    exchangeValue = exchangeRate.Value;
                }
                record++;
            }
            return exchangeValue;
        }

        private void GetUserNameandPassword(out string userName, out string password)
        {
            Console.WriteLine(Constants.EnterUserName);
            inputValidation.ValidateString(out userName);
            Console.WriteLine(Constants.EnterPassword);
            inputValidation.ValidateString(out password);
        }


        private void LoginStaff(Bank selectedBank)
        {
            string userName;
            string password;
            GetUserNameandPassword(out userName, out password);
            if (ValidateStaff(selectedBank, userName, password))
            {
                ListOutOperations(selectedBank);
            }
            else
            {
                Console.WriteLine(Constants.NotValidStaff);
            }
        }

        

        private void AddExchangeRate(Bank selectedBank)
        {
            string currency;
            Console.Write(Constants.AddExchangeRate);
            inputValidation.ValidateString(out currency);
            uint value;
            inputValidation.ValidateNumberFormat(out value);
            staffServices.AddExchangeRate(selectedBank, currency, value);
        }

        private void DeleteAccount(Bank selectedBank)
        {
            if (!CheckCustomerExists(selectedBank)) return;
            string accountId;
            Console.WriteLine(Constants.EnterAccountIdMessage);
            inputValidation.ValidateString(out accountId);
            if(!customerServices.DeleteAccount(selectedBank,accountId))
            {
                Console.WriteLine(Constants.AccountDeletionUnsuccesssfull);
            }
            else
            {
                Console.WriteLine(Constants.AccountDeletionSuccessfull);
            }
        }

        bool CheckCustomerExists(Bank bank)
        {
            int customerCount = customerServices.GetCustomersCount(bank);
            if (customerCount == 0)
            {
                Console.WriteLine(Constants.NoCustomer);
                return false;
            }
            return true;
        }

        private void UpdateAccount(Bank selectedBank)
        {
            string accountId;
            if (!CheckCustomerExists(selectedBank)) return;
            Console.WriteLine(Constants.EnterAccountIdMessage);
            inputValidation.ValidateString(out accountId);
            ApplicationUser applicationUser = customerServices.FetchCustomerByAccount(selectedBank,accountId);
            if(applicationUser is not null)
            {
                Console.WriteLine(Constants.NameOnlyAvailableField);
                Console.WriteLine(Constants.UpdatedNamePlease);
                string name;
                inputValidation.ValidateString(out name);
                customerServices.UpdateCustomerDetails(applicationUser, name);
            }
            else
            {
                Console.WriteLine(Constants.AccountUserNotExist);
            }
        }
        private void AddChargesForOtherBank(Bank selectedBank)
        {
            uint rtgs, imps;
            bool changeRtgs = true;
            ChargesOption(out rtgs, out imps, ref changeRtgs);
            if (changeRtgs) staffServices.AddRtgsChargesForOtherBank(selectedBank, rtgs);
            else staffServices.AddImpsChargesForOtherBank(selectedBank, imps);
        }

        private void AddChargesForSameBank(Bank selectedBank)
        {
            uint rtgs, imps;
            bool changeRtgs = true;
            ChargesOption(out rtgs, out imps, ref changeRtgs);
            if (changeRtgs) staffServices.AddRtgsChargesForSameBank(selectedBank, rtgs);
            else staffServices.AddImpsChargesForSameBank(selectedBank, imps);
        }

        private void ChargesOption(out uint rtgs, out uint imps, ref bool changeRtgs)
        {
            Console.WriteLine(Constants.OptionMessage);
            Console.WriteLine(Constants.RTGS);
            Console.WriteLine(Constants.IMPS);
            uint option;
            rtgs = 0;
            imps = 5;
            inputValidation.ValidateNumberFormat(out option);
            switch (option)
            {
                case 1:
                    Console.WriteLine(Constants.NewRTgsCharges);
                    rtgs = Convert.ToUInt32(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine(Constants.NewImpsCharges);
                    imps = Convert.ToUInt32(Console.ReadLine());
                    changeRtgs = false;
                    break;
            }

        }

        private void CreateAccount(Bank selectedBank)
        {
            string username;
            string password;
            Customer customer = new();
            Console.WriteLine(Constants.AccountHolderName);
            string name;
            inputValidation.ValidateString(out name);
            customer.Name = name;
            GetUserNameandPassword(out username, out password);
            customer.UserName = username;
            customer.Password = password;
            customer.Id = UtilityServices.GenerateCustomerId();
            customer.BankId = selectedBank.BankId;
            Account account = new()
            {
                AccountId = UtilityServices.GetAccountId(customer.Name),
                Balance = 0,
                CustomerId = customer.Id,
            };
            customer.account = account;
            customerServices.CreateAccount(customer);
            Console.WriteLine(String.Format(Constants.AccountCreationSuccessfull,account.AccountId));
            Console.WriteLine(Constants.NoteDown);
        }

        private bool ValidateStaff(Bank selectedBank, string? userName, string? password)
        {

            return staffServices.ValidateStaff(selectedBank, userName, password);
        }

        private void SetUpNewBank()
        {
            string name;
            Console.Write(Constants.EnterBankNameMessage);
            inputValidation.ValidateString(out name);
            Bank bank = new()
            {
                BankId = UtilityServices.GetBankId(name),
                BankName = name
            };
            centralBankServices.AddBank(bank);
            Console.WriteLine(Constants.EnterStaffNameMessage);
            string staffName;
            inputValidation.ValidateString(out staffName);
            Staff staff = new Staff()
            {
                BankId = bank.BankId,
                Name = staffName,
                Id = UtilityServices.GenerateStaffId(),
            };
            string staffUName;
            Console.WriteLine(Constants.StaffUserName);
            inputValidation.ValidateString(out staffUName);
            staff.UserName = staffUName;
            staff.Name = staffUName;
            Console.WriteLine(Constants.StaffPassword);
            string password;
            inputValidation.ValidateString(out password);
            staff.Password = password;
            staffServices.CreateStaff(staff);
        }
    }
}
