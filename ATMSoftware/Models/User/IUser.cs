using System;
using System.Collections.Generic;

namespace ATMSoftware.Models
{
    public interface IUser
    {
        Guid CardId { get; }
        int Pin { get; }
        decimal Balance { get; set; }
        List<ITransaction> Transactions { get; set; }
        int TransactionCount { get; set; }
    }
}