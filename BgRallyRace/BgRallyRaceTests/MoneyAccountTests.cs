namespace BgRallyRaceTests
{
    using BgRallyRace.Models;
    using BgRallyRace.Services;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;
    using System;
    using BgRallyRace.Data;

    public class MoneyAccountTests
    {
        //MockTests
        [Fact]
        public void CorrectFindUserAsync()
        {
            var money = new Mock<IMoneyAccountServices>();
            money.Setup(x => x.FindUserAsync(It.IsAny<string>()))
                .Returns(new  MoneyAccount
                {
                     User = "Pesho",
                     Balance = 10000,
                     Id = 1,
                });

            var result = money.Object.FindUserAsync("Pesho");
            var resultUserName = result.User;
            var resultUserBalance = result.Balance;
            var resultUserId = result.Id;

            Assert.Equal("Pesho", resultUserName);
            Assert.Equal(10000, resultUserBalance);
            Assert.Equal(1, resultUserId);

            money.Verify(x => x.FindUserAsync("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectBalance()
        {
            var money = new Mock<IMoneyAccountServices>();
            money.Setup(x => x.GetBalanceAsync(It.IsAny<string>()))
                .Returns(1000);

            var result = money.Object.GetBalanceAsync("Pesho");

            Assert.Equal(1000, result);

            money.Verify(x => x.GetBalanceAsync("Pesho"), Times.Once);
        }
      
        [Fact]
        public void CorrectFindIdMoneyAccount()
        {
            var money = new Mock<IMoneyAccountServices>();
            money.Setup(x => x.FindIdMoneyAccountAsync(It.IsAny<string>()))
                .Returns(1);

            var result = money.Object.FindIdMoneyAccountAsync("Pesho");

            Assert.Equal(1, result);

            money.Verify(x => x.FindIdMoneyAccountAsync("Pesho"), Times.Once);
        }

        //InMemoryTest
        [Fact]
        public void CorrectCreateMoneyAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new MoneyAccountServices(repository);

            service.CreateMoneyAccount("Pesho");

            var resultBalance = service.GetBalanceAsync("Pesho");
            var resultUserName = service.FindUserAsync("Pesho").User;
            var resultId = service.FindIdMoneyAccountAsync("Pesho");

            Assert.Equal(10000, resultBalance);
            Assert.Equal("Pesho", resultUserName);
            Assert.Equal(1, resultId);
        }

        [Fact]
        public void CorrectRevenueAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new MoneyAccountServices(repository);

            service.CreateMoneyAccount("Pesho");
            service.RevenueAccountAsync(1000, "Pesho");

            var result = service.GetBalanceAsync("Pesho");

            Assert.Equal(11000, result);
        }

        [Fact]
        public void CorrectExpenseAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new MoneyAccountServices(repository);

            service.CreateMoneyAccount("Pesho");
            service.ExpenseAccountAsync(1000, "Pesho");

            var result = service.GetBalanceAsync("Pesho");

            Assert.Equal(9000, result);
        }
    }
}

