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
        readonly CalculatorCode _cc = new CalculatorCode(); // 'CalculatorCode' interacts with backend code in 'CalculatorService'

        // EQUATION VARIABLES
        double? _number1 = null; // Values used for equations and to be displayed in calculator history text
        double? _number2 = null; // Null used in conditional statements for verification

        string _operation = ""; // Used to display selected operation in calculator history text

        // CONDITIONAL VARIABLES
        bool _equationComplete = false;  // Removes ability to use backspace and edit end equation result (When true) 
                                        // Clear or further calculations (Using +, -, /, *) reset this

        bool _divideByZeroLock = false; // Features of the calculator are locked when there is a divide by zero (When true)
                                       // Only the clear 'C' key resets this

        public MainWindow()
        {
            InitializeComponent();
        }


        // OPERATION BUTTON CLICK EVENTS & METHODS
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

        private void DetermineOperation(string op) // Determines which operation will be used in the equation
        {                                         // Displays this on the calculator once selected
            if (_divideByZeroLock == false)
            {
                _operation = $"{op}"; // Assigns operation by selected button

                HistoryTextDisplay.Text = $"{_number1} {op}"; // Displays the selected operation in the calculator history text
                ResultTextDisplay.Text = $"{op}"; // Displays selected operation

                _equationComplete = false; // Resumes normal use of the backspace button
            }                             // Allows for further equations to be made using the previous result
        }


        // EQUATION BUTTON CLICK EVENTS & METHODS
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_number2 == null) // Result cannot be created from equation if it is not complete
                return;
            else
            {
                switch (_operation) // Determines what operation is being used and calls the required method
                {
                    case "+":
                        var r1 = _cc.Add(_number1, _number2);
                        FullEquationHistoryDisplay(r1); // Displays the full equation in the calculator history text
                        break;

                    case "-":
                        var r2 = _cc.Subtract(_number1, _number2);
                        FullEquationHistoryDisplay(r2);
                        break;

                    case "*":
                        var r3 = _cc.Multiply(_number1, _number2);
                        FullEquationHistoryDisplay(r3);
                        break;

                    case "/":
                        var r4 = _cc.Divide(_number1, _number2);
                        if (r4 != "Cannot divide by zero") // If there is no devide by zero error, code functions as normal
                            FullEquationHistoryDisplay(r4);
                        else // Dividing by zero displays an error on the calculator and locks most of the functions, requiring clearance
                        {
                            ResultTextDisplay.Text = "Press 'C' to clear";
                            HistoryTextDisplay.Text = r4;
                            _divideByZeroLock = true;
                            _number1 = null;
                        }
                        break;
                }
                FinishEquation();
            }
        }

        private void FinishEquation() // Common actions used after totalling an equation
        {
            _number2 = null;
            _operation = "";
            _equationComplete = true;
        }
       
        private void FullEquationHistoryDisplay(string result) // Common actions to display equation and result after totalling
        {
            ResultTextDisplay.Text = result;
            HistoryTextDisplay.Text = $"{_number1} {_operation} {_number2} =";
            _number1 = Convert.ToDouble(result);  // Result can be used in the next equation or cleared
        }


        // CLEAR & BACKSPACE BUTTON CLICK EVENTS
        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if(_divideByZeroLock == false)
            {
                if (_operation == "") // Indicates that the first value is the entry that will be cleared
                {
                    _number1 = null;
                    HistoryTextDisplay.Text = "";
                }
                else // Indicates that the second value is the entry that will be cleared
                {
                    _number2 = null;
                    HistoryTextDisplay.Text = $"{_number1} {_operation}";
                }
                ResultTextDisplay.Text = "0"; // Results display reflects that the entry has been cleared
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e) // Resets the calculator to a neutral state
        {
            _number1 = null;
            _number2 = null;
            _operation = "";
            ResultTextDisplay.Text = "0";
            HistoryTextDisplay.Text = "";
            _divideByZeroLock = false;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if(_divideByZeroLock == false)
            {
                if (_equationComplete == true) // Removes ability to use backspace on the equation result
                    return;

                if (ResultTextDisplay.Text == "0") // If the value is empty, backspacing is not possible
                    return;                       // Text display represents the value of the number being used

                if (_operation == "") // Handles digit deletion for first value
                {
                    var num1 = _number1.ToString(); // Converted to evaluate length
                    if (num1.Length == 1) // Backspacing with one value left will always result in a zero
                    {
                        _number1 = 0;
                        ResultTextDisplay.Text = _number1.ToString();
                        HistoryTextDisplay.Text = _number1.ToString();
                    }
                    else // Uses Substring to return string value with one less index. Then converts this back to a double value
                    {
                        _number1 = Convert.ToDouble(_number1.ToString().Substring(0, ResultTextDisplay.Text.Length - 1));
                        ResultTextDisplay.Text = _number1.ToString();
                        HistoryTextDisplay.Text = _number1.ToString();
                    }
                }
                else  // Handles digit deletion for second value. Uses same functionality as for the first value.
                {
                    if (_number2 == null)
                        return;

                    var num2 = _number2.ToString();
                    if (num2.Length == 1)
                    {
                        _number2 = 0;
                        ResultTextDisplay.Text = _number2.ToString();
                        HistoryTextDisplay.Text = $"{_number1} {_operation} {_number2}";
                    }
                    else
                    {
                        _number2 = Convert.ToDouble(_number2.ToString().Substring(0, ResultTextDisplay.Text.Length - 1));
                        ResultTextDisplay.Text = _number2.ToString();
                        HistoryTextDisplay.Text = $"{_number1} {_operation} {_number2}";
                    }
                }
            }
        }


        // ADDITIONAL BUTTON CLICK EVENTS
        private void PositiveNegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(_divideByZeroLock == false)
            {
                if (_operation == "") // Indicates that the first value will changed between positive and negative
                {
                    _number1 *= -1;
                    ResultTextDisplay.Text = _number1.ToString();
                }
                else // Indicates that the second value will changed between positive and negative
                {
                    _number2 *= -1;
                    ResultTextDisplay.Text = _number2.ToString();
                }
            }
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e) // NOT IMPLEMENTED
        {
            if(_divideByZeroLock == false)
                return;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e) // Registers pressed digits to be used in equations
        {
            if (_divideByZeroLock == false)
            {
                Button b = (Button)sender;

                if (_number2 == null && _equationComplete == true) // Used to verify that the equation has been completed
                {                                                 // Starts a new equation if the result is not used for further calculations
                    ResultTextDisplay.Text = "";
                    HistoryTextDisplay.Text = $"{b.Content}";
                    _equationComplete = false;
                    _number1 = null;
                }

                if (_number1 == null) // If the first value has not been assigned, it is given a value. Avoids null checks
                    _number1 = 0;

                if (_operation == "") // Indicated that button events effect the first value
                {
                    _number1 = (_number1 * 10) + Convert.ToDouble(b.Content); // Allows additional numbers to be added in sequence
                    ResultTextDisplay.Text = _number1.ToString();
                    HistoryTextDisplay.Text = _number1.ToString();
                }
                else // Indicated that button events effect the second value. Uses same functionality as for the first value.
                {
                    if (_number2 == null)
                    {
                        HistoryTextDisplay.Text = $"{_number1} {_operation} {b.Content}";
                        _number2 = 0;
                    }
                    _number2 = (_number2 * 10) + Convert.ToDouble(b.Content);
                    ResultTextDisplay.Text = _number2.ToString();
                    HistoryTextDisplay.Text = $"{_number1} {_operation} {_number2}";
                }
            }
        }
    }
}
