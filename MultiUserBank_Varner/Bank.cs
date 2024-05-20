using System;

namespace BankApplication
{
    public class Bank
    {
        private decimal bankBalance = 10000;
        private string currentUsername;
        private decimal currentUserBalance;

        // Dictionary to store user credentials and initial balances
        private System.Collections.Generic.Dictionary<string, Tuple<string, decimal>> userAccounts = new System.Collections.Generic.Dictionary<string, Tuple<string, decimal>>()
        {
            { "jlennon", new Tuple<string, decimal>("johnny", 1250) },
            { "pmccartney", new Tuple<string, decimal>("pauly", 2500) },
            { "gharrison", new Tuple<string, decimal>("georgy", 3000) },
            { "rstarr", new Tuple<string, decimal>("ringoy", 1001) }
        };

        public decimal BankBalance
        {
            get { return bankBalance; }
        }

        public bool Login(string username, string password)
        {
            if (userAccounts.ContainsKey(username))
            {
                if (userAccounts[username].Item1 == password)
                {
                    currentUsername = username;
                    currentUserBalance = userAccounts[username].Item2;
                    Console.WriteLine($"Welcome, {currentUsername}! Your current balance is {currentUserBalance:C}");
                    return true;
                }
            }
            Console.WriteLine("Invalid username or password.");
            return false;
        }

        public void Deposit(decimal amount)
        {
            currentUserBalance += amount;
            bankBalance += amount;
            Console.WriteLine($"Deposit successful. Your new balance is {currentUserBalance:C}");
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 500)
            {
                amount = 500;
                Console.WriteLine("Withdrawal limit is $500. Withdrawing $500.");
            }

            if (amount > currentUserBalance)
            {
                amount = currentUserBalance;
                Console.WriteLine($"Insufficient funds. Withdrawing {amount:C}");
            }

            currentUserBalance -= amount;
            bankBalance -= amount;
            Console.WriteLine($"Withdrawal successful. Your new balance is {currentUserBalance:C}");
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Your current balance is {currentUserBalance:C}");
        }

        public void Logout()
        {
            Console.WriteLine($"Goodbye, {currentUsername}!");
            currentUsername = null;
            currentUserBalance = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            Console.WriteLine($"Welcome to the Bank! The bank's initial balance is {bank.BankBalance:C}");

            while (true)
            {
                if (bank.Login(GetUsername(), GetPassword()))
                {
                    while (true)
                    {
                        DisplayLoggedInMenu();
                        int choice = GetIntegerInput("Enter your choice: ");

                        switch (choice)
                        {
                            case 1:
                                bank.CheckBalance();
                                break;
                            case 2:
                                bank.Deposit(GetDecimalInput("Enter deposit amount: "));
                                break;
                            case 3:
                                bank.Withdraw(GetDecimalInput("Enter withdrawal amount: "));
                                break;
                            case 4:
                                bank.Logout();
                                Console.WriteLine($"The bank's final balance is {bank.BankBalance:C}");
                                goto endOfLoggedInLoop;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                endOfLoggedInLoop:;
                }
            }
        }

        static void DisplayLoggedInMenu()
        {
            Console.WriteLine("\nLogged In Menu:");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Logout");
        }

        static string GetUsername()
        {
            Console.Write("Enter username: ");
            return Console.ReadLine();
        }

        static string GetPassword()
        {
            Console.Write("Enter password: ");
            return Console.ReadLine();
        }

        static decimal GetDecimalInput(string prompt)
        {
            decimal input;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out input))
                {
                    return input;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static int GetIntegerInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    return input;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}