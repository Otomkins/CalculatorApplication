using System;
using System.Collections.Generic;
using System.Text;


namespace Calculator_Code
{
    public class CalculatorCode
    {
        readonly CalculatorService _cs = new CalculatorService(); // Used to interact with backend code in 'CalculatorService'

        // Methods pass double values into 'CalculatorService' to be calculated appropriately
        // Methods return string representations of this to be used with the .xaml.cs code and displayed

        public string Add(double? a, double? b) => _cs.AddNumbers(a, b).ToString();

        public string Subtract(double? a, double? b) => _cs.SubtractNumbers(a, b).ToString();

        public string Multiply(double? a, double? b) => _cs.MultiplyNumbers(a, b).ToString();

        public string Divide(double? a, double? b) => b == 0 ? "Cannot divide by zero" : _cs.DivideNumbers(a, b).ToString();
    }
}
