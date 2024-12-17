using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Concerns
{
    internal static class Constants
    {
        public const string WelcomeMessage= "------- Welcome ------------";
        public const string BankSetupMessage = "1.Set Up New Bank ";
        public const string ExistingBanksMessage = "2.Show Existing Banks ";
        public const string OptionMessage = "Select one of the Option";
        public const string NotValidMessage = "Enter Valid option ";
        public const string EnterBankNameMessage = "Enter Bank Name :";
        public const string EnterStaffNameMessage = "Enter Staff Name : ";
        public const string NotValidStaff = "Not a Valid Staff";
        public const string CreateUeserMessage = "1.Create an Account ";
        public const string CreateNewBankMessage = "No Existing banks available, Create a New Bank";
        public const string LoginAsAdminMessage = "1.Login as an Admin or a Staff";
        public const string LoginAsUserMessage = "2.Login as a User";
        public const string UpdateAccountMessage = "2.Update Account";
        public const string DeleteAccountMessage = "3.Delete Account";
        public const string AddChargesForSameBank = "4.Add Charges to Same Bank";
        public const string AddChargesForOtherBank = "5.Add Charges to Other Bank";
        public const string AddExchangeRate = "6.Add Exchange Rate:";
        public const string ViewTransactionHistory = "7.View Transaction History";
        public const string RevertAnyTransaction = "8.Revert any transaction ";
        public const string StaffExit = "9.Exit";

        public const string EnterAccountIdMessage = "Enter Account Id: ";
        public const string AccountNotFoundMessage = "Account Not Found...";
        public const string SelectTransactionMessage = "Select Which Transaction You want to Choose : ";
        public const string ChooseTransactionMessage = "Choose Transaction again, Entered Option is Not there in History";

        public const string CannotRevertDeposit = "Cannot revert transactions of type Deposit";
        public const string CannotRevertWithdrawl = "Cannot revert transactions of type Withdrawl";

        public const string RevertSuccess = "Revert Transaction is Successfull";

        public const string RevertUnSuccess = "Revert Transaction is Unsuccessfull";

        public const string TransactionHistory = "Transaction History: ";

        public const string Credited = "Credited";

        public const string Debited = "Debited";

        public const string NoTransactionHistory = "No Transaction History Available";

        public const string DepositMoney = "1.Deposit Money";
        public const string WithdrawMoney = "2.Withdraw Money";
        public const string TransferFunds = "3.Transfer Funds";

        public const string ViewTransactionHistoryUser = "4.View Transaction History";
        public const string CheckBalance = "5.Check Balance";

        public const string UserExit = "6.Exit";

        public const string DepositSuccess = "Deposit is Successfull";
        public const string DepositFailure = "Deposit is Unsuccessfull";

        public const string WithdrawlSuccess = "Amount Withdrawl is Successfull";

        public const string WithdrawFailure = "Amount Withdrawl is Unsuccessfull, Check your Balance";

        public const string TotalBalance = "Total Balance : {0} ";

        public const string CannotAbleToFetchBalance = "Something wrong in Checking balance";

        public const string ChooseTransferType = "Choose Transfer Type : ";

        public const string RTGS = "1.RTGS";

        public const string IMPS = "2.IMPS";

        public const string TransactionSuccessfull = "Transaction Success";

        public const string TransactionFailure = "Transaction Failed";

        public const string UnableToFetchAccount = "Unable to Fetch Account ID, Check Account ID";

        public const string RecieverId = "Enter Reciever Id: ";

        public const string BankToBank = "1.Bank To Bank Transfer";

        public const string BankToAnotherBank = "2.Bank to Another Bank Transfer";

        public const string SomethingWrongInFindingAccount = "Something Went wrong in finding account ";

        public const string EnterAmount = "Enter Amount : ";

        public const string EnterUserName = "Enter User Name: ";

        public const string EnterPassword = "Enter Password : ";

        public const string ExchangeRateMessage = "Enter Exchange Curreny Type : ";

        public const string AccountDeletionUnsuccesssfull = "AccountDeletion is Unsuccessfull";

        public const string AccountDeletionSuccessfull = "Account Deletion is Successfull";

        public const string NoCustomer = "No Customer Exists";

        public const string NameOnlyAvailableField = "Name only can be updated";

        public const string UpdatedNamePlease = "Enter Updated Name : ";

        public const string AccountUserNotExist = "Account User Didn't exist";

        public const string NewRTgsCharges = "Enter New RTGS Charges: ";

        public const string NewImpsCharges = "Enter New IMPS Charges: ";

        public const string AccountHolderName = "Enter Account Holder Name: ";

        public const string AccountCreationSuccessfull = "Account Created Successfully with Account Id {0}";

        public const string NoteDown = "Note It down.......";

        public const string StaffUserName = "Enter Staff Username : ";

        public const string StaffPassword = "Enter Staff Password : ";

        public const string SameAccountInfo = "Same Account cannot be accepted For transfer,Try again with different Account Id";

        public const string RecieverAccountNotFoundInfo = "Reciever Account is Not Found";

        public const string InsufficientFunds = "Insufficient Funds";

        public const string SameBankSelectedDifferentBankInfo = "Reciever bank is also smae, you are selected bank to another bank option";

    }
}
