namespace BgRallyRaceTests
{
    using BgRallyRace.Services.Money;
    using Moq;
    using System.Collections.Generic;
    using Xunit;

    public class FinanceTests
    {
        //MockTests
        [Fact]
        public void CorrectGetExpense()
        {
            var input = new List<decimal>
                {
                     1000,
                     2000
                };

            var finance = new Mock<IFinanceServices>();
            finance.Setup(x => x.GetExpense(It.IsAny<string>()))
                .Returns(input);

            var result = finance.Object.GetExpense("Pesho");

            Assert.Equal(input,result);

            finance.Verify(x => x.GetExpense("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectGetTotalExpense()
        {
            var finance = new Mock<IFinanceServices>();
            finance.Setup(x => x.GetTotalExpense(It.IsAny<string>()))
                .Returns(5000);

            var result = finance.Object.GetTotalExpense("Pesho");

            Assert.Equal(5000, result);

            finance.Verify(x => x.GetTotalExpense("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectGetRevenue()
        {
            var input = new List<decimal>
                {
                     1000,
                     2000
                };

            var finance = new Mock<IFinanceServices>();
            finance.Setup(x => x.GetRevenue(It.IsAny<string>()))
                .Returns(input);

            var result = finance.Object.GetRevenue("Pesho");

            Assert.Equal(input, result);

            finance.Verify(x => x.GetRevenue("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectGetTotalRevenue()
        {
            var finance = new Mock<IFinanceServices>();
            finance.Setup(x => x.GetTotalRevenue(It.IsAny<string>()))
                .Returns(5000);

            var result = finance.Object.GetTotalRevenue("Pesho");

            Assert.Equal(5000, result);

            finance.Verify(x => x.GetTotalRevenue("Pesho"), Times.Once);
        }
    }
}
