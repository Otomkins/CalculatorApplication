using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    public class CalculatorMethods
    {
        public static string Add(double a, double b)
        {
            return (a + b).ToString();
        }
        public static string Subtract(double a, double b)
        {
            return (a - b).ToString();
        }
        public static string Multiply(double a, double b)
        {
            return (a * b).ToString();
        }
        public static string Divide(double a, double b)
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
