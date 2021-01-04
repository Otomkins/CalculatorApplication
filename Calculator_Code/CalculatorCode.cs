using System;
using System.Collections.Generic;
using System.Text;


namespace Calculator_Code
{
    public class CalculatorCode
    {
        readonly CalculatorService _cs = new CalculatorService();
        public string Add(double? a, double? b)
        {
            return _cs.AddNumbers(a, b).ToString();
        }
        public string Subtract(double? a, double? b)
        {
            return _cs.SubtractNumbers(a, b).ToString();
        }
        public string Multiply(double? a, double? b)
        {
            return _cs.MultiplyNumbers(a, b).ToString();
        }
        public string Divide(double? a, double? b)
        {
            if(b == 0)
                return "Cannot divide by zero";
            else
                return _cs.DivideNumbers(a, b).ToString();
        }
    }
}
