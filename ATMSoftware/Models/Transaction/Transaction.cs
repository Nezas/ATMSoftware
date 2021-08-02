using System;

namespace ATMSoftware.Models
{
    public class Transaction : ITransaction
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
