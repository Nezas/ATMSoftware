using System;
using ATMSoftware.Models;
using ATMSoftware.Validation;
using ATMSoftware.Writer;

namespace ATMSoftware
{
    public class System
    {
        public IUser User;
        private readonly IValidator _pinValidator;
        private readonly IValidator _withdrawValidator;
        private readonly IConsoleWriter _writer;

        public System(IUser user)
        {
            User = user;
        }

        public System(IUser user, IValidator pinValidator, IValidator withdrawValidator, IConsoleWriter writer)
        {
            User = user;
            _pinValidator = pinValidator;
            _withdrawValidator = withdrawValidator;
            _writer = writer;
        }

        public void Start()
        {
            int attempts = 0;

            while(attempts < 3)
            {
                _writer.Write("Enter your PIN: ");
                try
                {
                    int enteredPin = Convert.ToInt32(Console.ReadLine());
                    if(_pinValidator.Validate(enteredPin) == true)
                    {
                        ShowMenu();
                    }
                    else
                    {
                        attempts++;
                        if(attempts == 1)
                        {
                            _writer.Write($"Wrong Pin! You have {3 - attempts} more attempts.\n");
                        }
                        else if(attempts == 2)
                        {
                            _writer.Write($"Wrong Pin! You have {3 - attempts} more attempt.\n");
                        }
                    }
                }
                catch(FormatException ex)
                {
                    _writer.Write($"{ex.Message}\n");
                }
            }
            _writer.Write("\nCard has been blocked for safety. Contact your bank.\n");
        }

        public void ShowMenu()
        {
            _writer.Clear();
            _writer.Write("1 - Balance\n");
            _writer.Write("2 - Last 5 transactions\n");
            _writer.Write("3 - Withdraw cash\n");
            _writer.Write("4 - Exit\n");
            _writer.Write("\nEnter your option: ");

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
                            if(_withdrawValidator.Validate(enteredMoney))
                            {
                                WithdrawCash(enteredMoney);
                                AddTransaction(enteredMoney);
                            }
                            ContinueToMenu();
                            break;
                        }
                    case "4":
                        {
                            _writer.Write("Don't forget to take your card!\n");
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            _writer.Write("Invalid input!\n");
                            _writer.Write("\nEnter your option: ");
                            break;
                        }
                }
            }
        }
        public int GetUserInput()
        {
            _writer.Clear();
            int money;
            while(true)
            {
                try
                {
                    _writer.Write("Enter the amount of money you want to withdraw: ");
                    money = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch(FormatException ex)
                {
                    _writer.Write($"{ex.Message}\n");
                }
            }
            return money;
        }

        public void ContinueToMenu()
        {
            _writer.Write("\nPress any key to continue.");
            Console.ReadKey();
            ShowMenu();
        }

        public void ShowBalance()
        {
            _writer.Clear();
            _writer.Write($"Your balance: {User.Balance}$\n");
        }

        public void ShowPreviousTransactions()
        {
            _writer.Clear();
            _writer.Write("Your 5 previous transactions\n");
            _writer.Write("\n");
            foreach(ITransaction transaction in User.Transactions)
            {
                _writer.Write($"{transaction.Amount}$ on {transaction.Date:d} at {transaction.Date:t}\n");
            }
        }

        public void WithdrawCash(int money)
        {
            User.Balance -= money;
            _writer.Write($"You successfully withdrawn {money}$.\n");
        }

        public void AddTransaction(int money)
        {
            User.TransactionCount++;
            ITransaction transaction = new Transaction();
            transaction.Amount = money;
            transaction.Date = DateTime.Now;
            User.Transactions.Add(transaction);
        }
    }
}
