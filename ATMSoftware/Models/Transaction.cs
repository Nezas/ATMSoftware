using System;

namespace ATMSoftware.Models
{
    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }

        public Transaction(decimal amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }
    }
}
