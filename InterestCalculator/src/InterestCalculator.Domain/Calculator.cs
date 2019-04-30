using System;

namespace InterestCalculator.Domain
{
    public class Calculator
    {
        public const decimal INTEREST_VALUE = 0.01m;

        public Calculator(decimal initialValue, int months)
        {
            InitialValue = initialValue;
            Months = months;
        }

        public decimal InitialValue { get; private set; }
        public int Months { get; private set; }
        public decimal Result { get; private set; }

        public void Calculate()
        {
            double _interestValue = Convert.ToDouble((1 + INTEREST_VALUE));
            double _exponentialValue = Math.Pow(_interestValue, Months);
            double _finalValue = Convert.ToDouble(InitialValue) * _exponentialValue;

            // truncate result value to show 2 decimal places precision without rounding the value
            Result = Math.Truncate(Convert.ToDecimal(_finalValue) * 100) / 100;
        }
    }
}
