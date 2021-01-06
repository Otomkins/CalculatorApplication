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

                if (_number2 == null) HistoryTextDisplay.Text = $"{_number1} {op}"; // Displays the selected operation in the calculator history text
                else HistoryTextDisplay.Text = $"{_number1} {op} {_number2}"; // Displays changes of operation at the end of the equation

                ResultTextDisplay.Text = $"{op}"; // Displays selected operation

                _equationComplete = false; // Resumes normal use of the backspace button
            }                             // Allows for further equations to be made using the previous result
        }


        // EQUATION BUTTON CLICK EVENTS & METHODS
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_number2 == null) return; // Result cannot be created from equation if it is not complete   
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
            _equationComplete = false;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if(_divideByZeroLock == false)
            {
                if (_equationComplete == true) return; // Removes ability to use backspace on the equation result        

                if (ResultTextDisplay.Text == "0") return; // Backspacing is not possible with empty values

                if (_operation == "") // Handles digit deletion for first value
                {
                    var num1String = _number1.ToString(); // Converted to evaluate length
                    var textboxString = HistoryTextDisplay.Text.ToString(); // Variables to reference length of values
                    var resultTextboxString = ResultTextDisplay.Text.ToString(); // Used to maintain decimal in the value when backspacing

                    if (num1String.Length == 1) // Backspacing with one value left will always result in a zero
                    {
                        if (textboxString.Contains('.')) // Maintains first digit when followed by a decimal and backspace is used
                        {
                            ResultTextDisplay.Text = resultTextboxString.Substring(0, textboxString.Length - 1);
                            HistoryTextDisplay.Text = textboxString.Substring(0, textboxString.Length - 1);
                        }
                        else // Normal functionality for deleting a single digit value. Value becomes zero
                        {
                            _number1 = 0;
                            ResultTextDisplay.Text = "0";
                            HistoryTextDisplay.Text = "0";
                        }
                    }
                    else // Uses Substring to return string value with one less index. Then converts this back to a double value
                    {
                        if (textboxString[num1String.Length - 2] == '.') // Maintains decimal in value when deleting the following digit
                        {                                               // This string is used to assign the numerical value with a decimal within
                            ResultTextDisplay.Text = resultTextboxString.Substring(0, textboxString.Length - 1);
                            HistoryTextDisplay.Text = textboxString.Substring(0, textboxString.Length - 1);
                        }
                        else // Normal functionality to remove digits from a value
                        {
                            _number1 = Convert.ToDouble(num1String.Substring(0, ResultTextDisplay.Text.Length - 1));
                            ResultTextDisplay.Text = _number1.ToString();
                            HistoryTextDisplay.Text = _number1.ToString();
                        }
                    }
                }
                else  // Handles digit deletion for second value. Uses similar functionality as for the first value
                {
                    if (_number2 == null) return;
                        
                    var num2String = _number2.ToString();
                    var textboxString = HistoryTextDisplay.Text.ToString();
                    var resultTextboxString = ResultTextDisplay.Text.ToString();

                    if (num2String.Length == 1)
                    {
                        if (_number1.ToString().Count(c => c == '.') != textboxString.Count(c => c == '.')) // Second value verification
                        {                                           // Maintains first digit when followed by a decimal and backspace is used
                            if(num2String == "0") // Removing a decimal in front of a zero has the same effect as using backspace on a zero
                            {
                                ResultTextDisplay.Text = $"{_operation}";
                                HistoryTextDisplay.Text = $"{_number1} {_operation}";
                            }
                            else
                            {
                                ResultTextDisplay.Text = num2String;
                                HistoryTextDisplay.Text = textboxString.Substring(0, textboxString.Length - 1);
                            }
                            _number2 = null;
                        }
                        else // Normal functionality for deleting a single digit for the second value. Returns to operation selection
                        {
                            _number2 = null;
                            ResultTextDisplay.Text = $"{_operation}";
                            HistoryTextDisplay.Text = $"{_number1} {_operation}";
                        }
                    }
                    else
                    {
                        if (resultTextboxString[num2String.Length - 2] == '.') // Maintains decimal in value when deleting the following digit
                        {                                               // This string is used to assign the numerical value with a decimal within
                            _number2 = Convert.ToDouble(num2String.Substring(0, ResultTextDisplay.Text.Length - 1));
                            ResultTextDisplay.Text = resultTextboxString.Substring(0, resultTextboxString.Length - 1);
                            HistoryTextDisplay.Text = textboxString.Substring(0, textboxString.Length - 1);
                        }
                        else // Normal functionality to remove digits from a value
                        {
                            _number2 = Convert.ToDouble(num2String.Substring(0, ResultTextDisplay.Text.Length - 1));
                            ResultTextDisplay.Text = _number2.ToString();
                            HistoryTextDisplay.Text = $"{_number1} {_operation} {_number2}";
                        }
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

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if(_divideByZeroLock == false)
            {
                if(_operation == "") // Indicates that the button event affects the first value
                {
                    if (_number2 == null && _equationComplete == true) // Used to verify that the equation has been completed
                    {                                                 // Value represents '0.' when the decimal button is first to be pressed
                        ResultTextDisplay.Text = "";
                        HistoryTextDisplay.Text = "0";
                        _equationComplete = false;
                        _number1 = null;
                    }

                    if (!HistoryTextDisplay.Text.Contains(".")) // Allows addition of a decimal point if the value does not include one
                    {                                          // Number button click events respond differently to this
                        if(_number1 is null)
                        {
                            _number1 = 0; // Assigned when the decimal button is pressed first so that it displays correctly
                            HistoryTextDisplay.Text += "0.";
                        }
                        else HistoryTextDisplay.Text += ".";

                        ResultTextDisplay.Text = $"{_number1}.";
                        
                    }
                }
                else // Indicates that the button event affects the second value. Uses similar functionality as for the first value
                {
                    if(_number1.ToString().Count(c => c == '.') == HistoryTextDisplay.Text.ToString().Count(c => c == '.')) // Second value verification
                    {                                                   // Allows addition of a decimal point if the value does not include one 
                        if (_number2 is null)
                        {
                            _number2 = 0;
                            HistoryTextDisplay.Text += " 0";
                        }

                        ResultTextDisplay.Text = $"{_number2}.";
                        HistoryTextDisplay.Text += ".";
                    }
                }
            }
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

                if (_number1 == null) _number1 = 0; // If the first value has not been assigned, it is given a value. Avoids null checks
                   
                if (_operation == "") // Indicates that the button event affects the first value
                {
                    if(HistoryTextDisplay.Text.Contains(".")) // Allows additonal numbers after a decimal point
                    {
                        ResultTextDisplay.Text += $"{b.Content}";
                        HistoryTextDisplay.Text += $"{b.Content}";

                        if (b.Content.ToString() != "0") // The zero after the decimal will not register once converted
                            _number1 = Convert.ToDouble(HistoryTextDisplay.Text); // The zero remains within the string to be converted after another entry
                    }
                    else
                    {
                        _number1 = (_number1 * 10) + Convert.ToDouble(b.Content); // Allows additional numbers to be added in sequence
                        ResultTextDisplay.Text = _number1.ToString();
                        HistoryTextDisplay.Text = _number1.ToString();
                    }
                }
                else // Indicates that the button event affects the second value. Uses similar functionality as for the first value
                {
                    if (_number2 == null) _number2 = 0;
                       
                    // The following code verifies that the second value will be used and in what scenario
                    // Two conditional statements check for decimal points and handle the code accordingly
                    // These check two of the scenarios: 1. value 1 has a decimal and value 2 doesnt and 2. Both values have decimals within them

                    if(HistoryTextDisplay.Text.Contains(".") && _number1.ToString().Contains(".") == false) // Verifies that the second value has a decimal
                    {                                                                       // Following code allows digits to be added afterwards
                        ResultTextDisplay.Text += $"{b.Content}";
                        HistoryTextDisplay.Text += $"{b.Content}";

                        if (b.Content.ToString() != "0") // Handles zero's entered anytime after a decimal point
                        {
                            var findDec = HistoryTextDisplay.Text.ToString();
                            int index = findDec.IndexOf($"{_operation}");
                            var decimalSecondNum = findDec.Remove(0, index + 1); // Removes all characters in the string before the second value
                            _number2 = Convert.ToDouble(decimalSecondNum); 
                        }
                    }
                    else if(HistoryTextDisplay.Text.Count(c => c == '.') == 2) // Verifies that the second value has a decimal
                    {                                                         // Verifies during second possible outcome of both values including decimals
                        ResultTextDisplay.Text += $"{b.Content}";
                        HistoryTextDisplay.Text += $"{b.Content}";

                        if (b.Content.ToString() != "0")
                        {
                            var findDec = HistoryTextDisplay.Text.ToString();
                            int index = findDec.IndexOf($"{_operation}");
                            var decimalSecondNum = findDec.Remove(0, index + 1);
                            _number2 = Convert.ToDouble(decimalSecondNum);
                        }
                    }
                    else // Normal scenario of adding digits to the second value with no decimals involved
                    {
                        _number2 = (_number2 * 10) + Convert.ToDouble(b.Content);
                        ResultTextDisplay.Text = _number2.ToString();
                        HistoryTextDisplay.Text = $"{_number1} {_operation} {_number2}";
                    }

                }
            }
        }
    }
}
