using System;
using System.Collections.Generic;

namespace ATMSoftware.Models
{
    public class User
    {
        public Guid CardId { get; }
        public int Pin { get; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int TransactionCount { get; set; }

        public User(int pin, decimal initialBalance)
        {
            CardId = Guid.NewGuid();
            Pin = pin;
            Balance = initialBalance;
            Transactions = new();
            TransactionCount = 0;
        }
    }
}
