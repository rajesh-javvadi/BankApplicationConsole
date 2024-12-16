namespace BankApplication.Models
{
    internal class Customer : ApplicationUser
    {
        public Customer()
        {
            this.UserType = Concerns.UserType.Customer;
        }
        public Account account { get; set; }
    }
}
