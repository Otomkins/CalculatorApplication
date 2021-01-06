using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Code
{
    public class CalculatorService
    {
        // Calculator service layer created to have a seperation of concerns
        // These methods use only numerical values for calculation

        // The nullable doubles are used for verification within the application
        // The values are converted to string and back so that the result can be rounded
        // This is because certain decimal equations return an incorrect answer without this rounding
        // For example: '5.06 + 5' would return '10.05999999999...'

        public double? AddNumbers(double? a, double? b) => Math.Round(Convert.ToDouble((a + b).ToString()), 8);

        public double? SubtractNumbers(double? a, double? b) => Math.Round(Convert.ToDouble((a - b).ToString()), 8);

        public double? MultiplyNumbers(double? a, double? b) => Math.Round(Convert.ToDouble((a * b).ToString()), 8);

        public double? DivideNumbers(double? a, double? b) => b == 0 ? throw new DivideByZeroException("You cannot divide by zero")
            : Math.Round(Convert.ToDouble((a / b).ToString()), 12);
    }
}
