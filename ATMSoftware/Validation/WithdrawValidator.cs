using System;
using ATMSoftware.Models;

namespace ATMSoftware.Validation
{
    public class WithdrawValidator : IValidator
    {
        private readonly IUser _user;

        public WithdrawValidator(IUser user)
        {
            _user = user;
        }

        public bool Validate(int money)
        {
            if(money > _user.Balance)
            {
                Console.WriteLine("You don't have enough money in your account!");
                return false;
            }
            else if(money > 1000)
            {
                Console.WriteLine("The withdrawal limit is 1000$.");
                return false;
            }
            else if(_user.TransactionCount == 5)
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
    }
}
