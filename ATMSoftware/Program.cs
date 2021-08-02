using System.Collections.Generic;
using ATMSoftware.Models;
using ATMSoftware.Validation;
using ATMSoftware.Writer;

namespace ATMSoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IUser> users = new();

            users.Add(new User(1111, 100));
            users.Add(new User(2222, 500));
            users.Add(new User(3333, 1000));

            var system = new System(users[1], new PinValidator(users[1]), new WithdrawValidator(users[1]), new ConsoleWriter());
            system.Start();
        }
    }
}
