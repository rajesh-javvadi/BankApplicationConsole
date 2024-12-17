using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Concerns;

namespace BankApplication.View
{
    internal class InputValidation
    {
        public void ValidateNumberFormat(out uint option)
        {
            do
            {
                if (uint.TryParse(Console.ReadLine(), out option))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Constants.OptionMessage);
                }
            } while (true);
        }

        public void ValidateString(out string name)
        {
            
            do
            {
                
                try
                {
                    name = Console.ReadLine().Trim();
                    if (name.Length < 3)
                    {
                        throw new Exception("Name Should be More than Three letters");
                    }
                    else
                    {
                        break;
                    }
                }
                catch(NullReferenceException)
                {
                    Console.WriteLine("Can't be Empty");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                   
                }
            } while (true);
        }

        //public void ValidateUserName(out string userName)
        //{
        //    //do
        //    //{

        //    //}
        //}

        public void ValidateUserName(string userName)
        {
            do
            {
                userName = Console.ReadLine();
                if(userName.Length == 0)
                {
                    break;
                }
                else if(userName.Length > 3)
                {
                    break;
                }
            } while (true);
        }
    }
}
