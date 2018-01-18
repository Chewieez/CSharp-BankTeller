using System;
using Microsoft.Data.Sqlite;

namespace BankTeller
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the database interface
            DatabaseInterface db = new DatabaseInterface();

            // Check/create the Account table
            db.CheckAccountTable();

            int choice;

            // variable to hold the account Id after it's been created by step 1. 
            int accountId = -1;

            do
            {
                // Show the main menu by invoking the static method
                choice = MainMenu.Show();

                switch (choice)
                {
                    // Menu option 1: Adding account
                    case 1:
                        // Ask user to input customer name
                        Console.WriteLine("Please enter your full name.");
                        Console.Write(">");
                        string CustomerName = Console.ReadLine();

                        // Insert customer account into database
                        accountId = db.Insert($@"
                            INSERT INTO Account
                            (Id, Customer, Balance)
                            VALUES
                            (null, '{CustomerName}', 0)
                        ");
                        break;

                    // Menu option 2: Deposit money
                    case 2:
                        // Logic here
                        // if the account id has not already been populated, ask user for their account id number
                        if (accountId == -1)
                        {
                            Console.WriteLine("Enter your account number.");
                            Console.Write(">");
                            accountId = int.Parse(Console.ReadLine());
                            // string CustomerNameForDeposit = Console.ReadLine();
                        }

                        Console.WriteLine("How much would you like to deposit?");
                        Console.Write(">");
                        double depositAmount = double.Parse(Console.ReadLine());

                        // find out what the current balance is, and then add the amount the user wants to add before updating the db
                        db.Update($@"
                            UPDATE Account
                            SET Balance = Balance + {depositAmount}
                            WHERE Id = {accountId}
                            ");
                        break;

                    // Menu option 3: Withdraw money
                    case 3:
                        // if the account id has not already been populated, ask user for their account id number
                        if (accountId == -1)
                        {
                            Console.WriteLine("Enter your account number.");
                            Console.Write(">");
                            accountId = int.Parse(Console.ReadLine());
                            // string CustomerNameForDeposit = Console.ReadLine();
                        }

                        Console.WriteLine("How much would you like to withdraw?");
                        Console.Write(">");
                        double withdrawAmount = double.Parse(Console.ReadLine());

                        // find out what the current balance is, and then add the amount the user wants to add before updating the db
                        db.Update($@"
                            UPDATE Account
                            SET Balance = Balance - {withdrawAmount}
                            WHERE Id = {accountId}
                            ");
                        break;

                    // Menu option 4: Display Account Balance
                    case 4:
                        // if the account id has not already been populated, ask user for their account id number
                        if (accountId == -1)
                        {
                            Console.WriteLine("Enter your account number.");
                            Console.Write(">");
                            accountId = int.Parse(Console.ReadLine());
                            // string CustomerNameForStatement = Console.ReadLine();
                        }
                        double balance = 0;
                        string customerN = "";

                        db.Query($@"
                            SELECT Balance, Customer FROM Account
                            WHERE Id = {accountId};
                            ", (SqliteDataReader reader) =>
                        {
                            while (reader.Read ())
                            {
                                balance = reader.GetDouble(0);
                                customerN = reader.GetString(1);
                            }
                        });

                        Console.WriteLine($"Current Account Balance for {customerN}");
                        Console.WriteLine($"$ {balance}");

                        // pause command line before break statement to display balance
                        Console.WriteLine("Press Enter To Continue");
                        Console.Write(">");
                        Console.ReadLine();

                        break;


                }
            } while (choice != 5);



        }
    }
}
