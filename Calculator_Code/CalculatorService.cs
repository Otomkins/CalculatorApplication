using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Code
{
    public class CalculatorService
    {
        // Calculator service layer created to have a seperation of concerns
        // These methods use only numerical values for calculation

        public double? AddNumbers(double? a, double? b) => a + b;

        public double? SubtractNumbers(double? a, double? b) => a - b;

        public double? MultiplyNumbers(double? a, double? b) => a * b;

        public double? DivideNumbers(double? a, double? b) => b == 0 ? throw new DivideByZeroException("You cannot divide by zero") : a / b;
    }
}
