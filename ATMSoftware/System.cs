using System;
using ATMSoftware.Models;

namespace ATMSoftware
{
    public class System
    {
        public User User;

        public System(User user)
        {
            User = user;
        }

        public void ValidatePin()
        {
            int enteredPin = 0;
            int attempts = 0;
            do
            {
                Console.Write("Enter your PIN: ");
                try
                {
                    enteredPin = Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                attempts++;
                if(enteredPin == User.Pin)
                {
                    ShowMenu();
                }

                if(attempts == 1)
                {
                    Console.WriteLine($"Wrong PIN! You have {3 - attempts} more attempts.");
                }
                else if(attempts == 2)
                {
                    Console.WriteLine($"Wrong PIN! You have {3 - attempts} more attempt.");
                }
            } while(attempts < 3);

            Console.WriteLine("\nCard has been blocked for safety. Contact your bank.");
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Balance");
            Console.WriteLine("2 - Last 5 transactions");
            Console.WriteLine("3 - Withdraw cash");
            Console.WriteLine("4 - Exit");
            Console.Write("\nEnter your option: ");

            while(true)
            {
                switch(Console.ReadLine())
                {
                    case "1":
                        {
                            ShowBalance();
                            ContinueToMenu();
                            break;
                        }
                    case "2":
                        {
                            ShowPreviousTransactions();
                            ContinueToMenu();
                            break;
                        }
                    case "3":
                        {
                            int enteredMoney = GetUserInput();
                            if(ValidateWithdraw(enteredMoney))
                            {
                                WithdrawCash(enteredMoney);
                                AddTransaction(enteredMoney);
                            }
                            ContinueToMenu();
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Don't forget to take your card!");
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input!");
                            Console.Write("\nEnter your option: ");
                            break;
                        }
                }
            }
        }

        public int GetUserInput()
        {
            Console.Clear();
            int money;
            while(true)
            {
                try
                {
                    Console.Write("Enter the amount of money you want to withdraw: ");
                    money = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return money;
        }

        public void ContinueToMenu()
        {
            Console.Write("\nPress any key to continue.");
            Console.ReadKey();
            ShowMenu();
        }

        public void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine($"Your balance: {User.Balance}$");
        }

        public void ShowPreviousTransactions()
        {
            Console.Clear();
            Console.WriteLine("Your 5 previous transactions\n");
            foreach(Transaction transaction in User.Transactions)
            {
                Console.WriteLine($"{transaction.Amount}$ on {transaction.Date:d} at {transaction.Date:t}");
            }
        }

        public bool ValidateWithdraw(int money)
        {
            if(money > User.Balance)
            {
                Console.WriteLine("You don't have enough money in your account!");
                return false;
            }
            else if(money > 1000)
            {
                Console.WriteLine("The withdrawal limit is 1000$.");
                return false;
            }
            else if(User.TransactionCount == 5)
            {
                Console.WriteLine("You can only withdraw money 5 times a day.");
                return false;
            }
            else if(money <= 0)
            {
                Console.WriteLine("The minimum amount you can withdraw is 1$.");
                return false;
            }
            return true;
        }

        public void WithdrawCash(int money)
        {
            User.Balance -= money;
            Console.WriteLine($"You successfully withdrawn {money}$.");
        }

        public void AddTransaction(int money)
        {
            User.TransactionCount++;
            var transaction = new Transaction(money, DateTime.Now);
            User.Transactions.Add(transaction);
        }
    }
}
