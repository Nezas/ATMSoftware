using System;

namespace ATMSoftware.Models
{
    public interface ITransaction
    {
        decimal Amount { get; set; }
        DateTime Date { get; set; }
    }
}