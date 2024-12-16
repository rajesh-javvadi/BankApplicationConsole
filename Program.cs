using BankApplication.View;

namespace BankAppliaction
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ApplicationMenu applicationMenu = new();
            applicationMenu.StartMenu();
        }
    }
}