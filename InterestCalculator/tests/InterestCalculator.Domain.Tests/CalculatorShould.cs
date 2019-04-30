using Xunit;

namespace InterestCalculator.Domain.Tests
{
    public class CalculatorShould
    {
        [Theory]
        [InlineData(100, 5)]
        [InlineData(30, 2)]
        [InlineData(12, 12)]
        public void PopulateResultPropertyWithPositiveValues(decimal initialValue, int months)
        {
            //Arrange
            Calculator calculator = new Calculator(initialValue, months);

            //Act
            calculator.Calculate();

            //Assert
            Assert.True(calculator.Result >= 0);
        }

        [Theory]
        [InlineData(-44, 3)]
        [InlineData(-256, 8)]
        [InlineData(-1000, 1)]
        public void PopulateResultPropertyWithNegativeValues(decimal initialValue, int months)
        {
            //Arrange
            Calculator calculator = new Calculator(initialValue, months);

            //Act
            calculator.Calculate();

            //Assert
            Assert.True(calculator.Result < 0);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(0, 0)]
        [InlineData(0, -3)]
        public void PopulateResultPropertyWithZero(decimal initialValue, int months)
        {
            //Arrange
            Calculator calculator = new Calculator(initialValue, months);

            //Act
            calculator.Calculate();

            //Assert
            Assert.True(calculator.Result == 0);
        }

        [Fact]
        public void PopulateResultPropertyWithExpectedValue()
        {
            //Arrange
            Calculator calculator = new Calculator(100, 5);

            //Act
            calculator.Calculate();

            //Assert
            Assert.Equal(105.1m, calculator.Result);
            Assert.Equal(100, calculator.InitialValue);
            Assert.Equal(5, calculator.Months);
        }

        [Fact]
        public void HaveOnePercentValueForInterestProperty()
        {
            //Arrange
            Calculator calculator = new Calculator(1, 1);

            //Act
            calculator.Calculate();

            //Assert
            Assert.Equal(0.01m, Calculator.INTEREST_VALUE);
            Assert.Equal(1, calculator.InitialValue);
            Assert.Equal(1, calculator.Months);
        }
    }
}
