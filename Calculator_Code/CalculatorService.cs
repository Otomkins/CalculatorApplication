using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Code
{
    public class CalculatorService
    {
        public double? AddNumbers(double? a, double? b)
        {
            return a + b;
        }
        public double? SubtractNumbers(double? a, double? b)
        {
            return a - b;
        }
        public double? MultiplyNumbers(double? a, double? b)
        {
            return a * b;
        }
        public double? DivideNumbers(double? a, double? b)
        {
            try
            {
                return a / b;
            }
            catch(DivideByZeroException ex)
            {
                throw new DivideByZeroException($"You cannot divide by zero. Exception Message: {ex.Message}");
            }
        }
    }
}
