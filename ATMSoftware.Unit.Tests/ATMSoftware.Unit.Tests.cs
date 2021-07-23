using Xunit;
using ATMSoftware.Models;

namespace ATMSoftware.Unit.Tests
{
    public class UnitTests
    {
        [Fact]
        public void ValidateWithdraw_NotEnoughMoneyInBalance()
        {
            var user = new User(1111, 100);
            var system = new System(user);

            bool validation = system.ValidateWithdraw(200);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_WithdrawMoneyLimit()
        {
            var user = new User(1111, 2000);
            var system = new System(user);

            bool validation = system.ValidateWithdraw(1200);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_TransactionCountLimit()
        {
            var user = new User(1111, 2000);
            user.TransactionCount = 5;
            var system = new System(user);

            bool validation = system.ValidateWithdraw(200);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_EnterZero()
        {
            var user = new User(1111, 2000);
            var system = new System(user);

            bool validation = system.ValidateWithdraw(0);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_EnterNegativeNumber()
        {
            var user = new User(1111, 2000);
            var system = new System(user);

            bool validation = system.ValidateWithdraw(-100);

            Assert.False(validation);
        }

        [Fact]
        public void WithdrawCash()
        {
            var user = new User(1111, 2000);
            var system = new System(user);

            system.WithdrawCash(500);

            Assert.Equal(1500, system.User.Balance);
        }

        [Fact]
        public void AddTransaction_TransactionCountIncrease()
        {
            var user = new User(1111, 2000);
            var system = new System(user);

            system.AddTransaction(500);

            Assert.Equal(1, system.User.TransactionCount);
        }

        [Fact]
        public void AddTransaction_SavedTransaction()
        {
            var user = new User(1111, 2000);
            var system = new System(user);

            system.AddTransaction(500);

            Assert.Equal(500, system.User.Transactions[0].Amount);
        }
    }
}
