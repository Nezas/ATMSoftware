using System;
using ATMSoftware.Models;

namespace ATMSoftware.Validation
{
    public class PinValidator : IValidator
    {
        private readonly IUser _user;

        public PinValidator(IUser user)
        {
            _user = user;
        }

        public bool Validate(int enteredPin)
        {
            if (enteredPin == _user.Pin)
            {
                return true;
            }
            return false;
        }
    }
}
