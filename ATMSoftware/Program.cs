using System.Collections.Generic;
using ATMSoftware.Models;

namespace ATMSoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new();

            users.Add(new User(1111, 100));
            users.Add(new User(2222, 500));
            users.Add(new User(3333, 1000));

            var system = new System(users[1]);
            system.ValidatePin();
        }
    }
}
