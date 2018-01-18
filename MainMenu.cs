using System;

namespace BankTeller
{
    public class MainMenu
    {
        public static int Show()
        {
            Console.Clear();
            Console.WriteLine ("WELCOME TO NASHVILLE SAFE & SOUND BANK");
            Console.WriteLine ("**************************************");
            Console.WriteLine ("1. Create customer account");
            Console.WriteLine ("2. Deposit Money");
            Console.WriteLine ("3. Withdraw Money");
            Console.WriteLine ("4. Show account balance");
            Console.WriteLine ("5. Exit");

            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            return int.Parse(enteredKey.KeyChar.ToString());
        }
    }
}