using Xunit;
using ATMSoftware.Models;
using ATMSoftware.Validation;
using ATMSoftware.Writer;

namespace ATMSoftware.Unit.Tests
{
    public class UnitTests
    {
        [Fact]
        public void ValidatePin_EnterWrongPin_ReturnsFalse()
        {
            IUser user = new User(1111, 100);
            IValidator validator = new PinValidator(user);

            bool validation = validator.Validate(1234);

            Assert.False(validation);
        }

        [Fact]
        public void ValidatePin_EnterCorrectPin_ReturnsTrue()
        {
            IUser user = new User(1111, 100);
            IValidator validator = new PinValidator(user);

            bool validation = validator.Validate(1111);

            Assert.True(validation);
        }

        [Fact]
        public void ValidateWithdraw_NotEnoughMoneyInBalance_ReturnsFalse()
        {
            IUser user = new User(1111, 100);
            IValidator validator = new WithdrawValidator(user);

            bool validation = validator.Validate(500);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_WithdrawMoneyLimit_ReturnsFalse()
        {
            IUser user = new User(1111, 2000);
            IValidator validator = new WithdrawValidator(user);

            bool validation = validator.Validate(1200);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_TransactionCountLimit_ReturnsFalse()
        {
            IUser user = new User(1111, 2000);
            user.TransactionCount = 5;
            IValidator validator = new WithdrawValidator(user);

            bool validation = validator.Validate(200);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_EnterZero_ReturnsFalse()
        {
            IUser user = new User(1111, 2000);
            IValidator validator = new WithdrawValidator(user);

            bool validation = validator.Validate(0);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateWithdraw_EnterNegativeNumber_ReturnsFalse()
        {
            IUser user = new User(1111, 2000);
            IValidator validator = new WithdrawValidator(user);

            bool validation = validator.Validate(-200);

            Assert.False(validation);
        }

        [Fact]
        public void WithdrawCash_EnterGoodNumber_ReturnsCorrectBalance()
        {
            IUser user = new User(1111, 2000);
            var system = new System(user, new PinValidator(user), new WithdrawValidator(user), new ConsoleWriter());

            system.WithdrawCash(500);

            Assert.Equal(1500, system.User.Balance);
        }

        [Fact]
        public void AddTransaction_AddNewTransaction_TransactionCountIncrease()
        {
            IUser user = new User(1111, 2000);
            var system = new System(user);

            system.AddTransaction(500);

            Assert.Equal(1, system.User.TransactionCount);
        }

        [Fact]
        public void AddTransaction_AddNewTransaction_TransactionIsSaved()
        {
            IUser user = new User(1111, 2000);
            var system = new System(user);

            system.AddTransaction(500);

            Assert.Equal(500, system.User.Transactions[0].Amount);
        }
    }
}
