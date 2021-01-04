using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Code
{
    public class CalculatorService
    {
        public double AddNumbers(double a, double b)
        {
            return a + b;
        }
        public double SubtractNumbers(double a, double b)
        {
            return a - b;
        }
        public double MultiplyNumbers(double a, double b)
        {
            return a * b;
        }
        public string DivideNumbers(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by 0");
            //return "Cannot divide by 0";
            else
                return Convert.ToString(Math.Round((a / b), 10));

            //try
            //{
            //    return Convert.ToString(Math.Round((a / b), 10));
            //}
            //catch(DivideByZeroException ex)
            //{
            //    return "Cannot divide by 0";
            //}
        }
    }
}
