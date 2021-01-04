using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator_Code;
using System.Text.RegularExpressions;

namespace Calculator_WPF
{
    public partial class MainWindow : Window
    {
        readonly CalculatorCode _cc = new CalculatorCode();

        double? number1 = null;
        double? number2 = null;
        string operation = "";
        bool equationComplete = false;
        bool divideByZeroLock = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DetermineOperation("+");
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            DetermineOperation("-");
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            DetermineOperation("*");
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            DetermineOperation("/");
        }

        private void DetermineOperation(string op)
        {
            if (divideByZeroLock == false)
            {
                if (operation == "")
                    HistoryTextDisplay.Text = $"{number1} {op}";

                operation = $"{op}";
                ResultTextDisplay.Text = $"{op}";
                equationComplete = false;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (number2 == null)
                return;
            else
            {
                switch (operation)
                {
                    case "+":
                        var r1 = _cc.Add(number1, number2);
                        FullEquationHistoryDisplay(r1);
                        break;

                    case "-":
                        var r2 = _cc.Subtract(number1, number2);
                        FullEquationHistoryDisplay(r2);
                        break;

                    case "*":
                        var r3 = _cc.Multiply(number1, number2);
                        FullEquationHistoryDisplay(r3);
                        break;

                    case "/":
                        var r4 = _cc.Divide(number1, number2);
                        if (r4 != "Cannot divide by zero")
                        {
                            FullEquationHistoryDisplay(r4);
                        }
                        else
                        {
                            ResultTextDisplay.Text = "Press 'C' to clear";
                            HistoryTextDisplay.Text = r4;
                            divideByZeroLock = true;
                            number1 = null;
                        }
                        break;
                }
                FinishEquation();
            }
        }

        private void FinishEquation()
        {
            number2 = null;
            operation = "";
            equationComplete = true;
        }
        private void FullEquationHistoryDisplay(string result)
        {
            ResultTextDisplay.Text = result;  // Displays answer through method
            HistoryTextDisplay.Text = $"{number1} {operation} {number2} =";  // Displays equation
            number1 = Convert.ToDouble(result);  // Changes number1 to result for further calculations
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if(divideByZeroLock == false)
            {
                if (operation == "")
                {
                    number1 = null;
                    HistoryTextDisplay.Text = "";
                }
                else
                {
                    number2 = null;
                    HistoryTextDisplay.Text = $"{number1} {operation}";
                }
                ResultTextDisplay.Text = "0";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            number1 = null;
            number2 = null;
            operation = "";
            ResultTextDisplay.Text = "0";
            HistoryTextDisplay.Text = "";
            divideByZeroLock = false;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if(divideByZeroLock == false)
            {
                if (equationComplete == true)
                    return;
                if (ResultTextDisplay.Text == "0")
                    return; // If the value is empty, does nothing

                //if (Regex.IsMatch(ResultTextDisplay.Text, @"[+\-/*]") && operation != "")
                //    operation = "";

                if (operation != "" && number2 == null)
                    return;
                else if (operation == "") // Handles digit deletion for first value
                {
                    var num1 = number1.ToString();
                    if (num1.Length == 1)
                    {
                        number1 = 0;
                        ResultTextDisplay.Text = number1.ToString();
                        HistoryTextDisplay.Text = number1.ToString();
                    }
                    else
                    {
                        number1 = Convert.ToDouble(number1.ToString().Substring(0, ResultTextDisplay.Text.Length - 1));
                        ResultTextDisplay.Text = number1.ToString();
                        HistoryTextDisplay.Text = number1.ToString();
                    }
                }
                else  // Handles digit deletion for second value
                {
                    if (number2 == null)
                        return;
                    var num2 = number2.ToString();
                    if (num2.Length == 1)
                    {
                        number2 = 0;
                        ResultTextDisplay.Text = number2.ToString();
                        HistoryTextDisplay.Text = $"{number1} {operation} {number2}";
                    }
                    else
                    {
                        number2 = Convert.ToDouble(number2.ToString().Substring(0, ResultTextDisplay.Text.Length - 1));
                        ResultTextDisplay.Text = number2.ToString();
                        HistoryTextDisplay.Text = $"{number1} {operation} {number2}";
                    }
                }
            }
        }

        private void PositiveNegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(divideByZeroLock == false)
            {
                if (operation == "")
                {
                    number1 *= -1;
                    ResultTextDisplay.Text = number1.ToString();
                }
                else
                {
                    number2 *= -1;
                    ResultTextDisplay.Text = number2.ToString();
                }
            }
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if(divideByZeroLock == false)
                return;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (divideByZeroLock == false)
            {
                Button b = (Button)sender;

                if (number2 == null && equationComplete == true)
                {
                    ResultTextDisplay.Text = "";
                    HistoryTextDisplay.Text = $"{b.Content}";
                    equationComplete = false;
                    number1 = null;
                }

                if (number1 == null)
                    number1 = 0;

                if (operation == "")
                {
                    number1 = (number1 * 10) + Convert.ToDouble(b.Content);
                    ResultTextDisplay.Text = number1.ToString();
                    HistoryTextDisplay.Text = number1.ToString();
                }
                else
                {
                    if (number2 == null)
                    {
                        HistoryTextDisplay.Text = $"{number1} {operation} {b.Content}";
                        number2 = 0;
                    }
                    number2 = (number2 * 10) + Convert.ToDouble(b.Content);
                    ResultTextDisplay.Text = number2.ToString();
                    HistoryTextDisplay.Text = $"{number1} {operation} {number2}";
                }
            }
        }

        //private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
