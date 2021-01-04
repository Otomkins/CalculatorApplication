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

namespace Calculator_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculatorCode _cc = new CalculatorCode();

        double number1 = 0;
        double number2 = 0;
        string operation = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            operation = "+";
            ResultTextDisplay.Text = "+";
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            operation = "-";
            ResultTextDisplay.Text = "-";
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            operation = "*";
            ResultTextDisplay.Text = "*";
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            operation = "/";
            ResultTextDisplay.Text = "/";
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            switch (operation)
            {
                case "+":
                    ResultTextDisplay.Text = _cc.Add(number1, number2); // Displays answer through method
                    HistoryTextDisplay.Text = $"{number1} {operation} {number2} ="; // Displays equation

                    number1 = Convert.ToDouble(_cc.Add(number1, number2)); // Changes number1 to result for further calculations
                    number2 = 0;
                    operation = "";
                    break;
                case "-":
                    ResultTextDisplay.Text = _cc.Subtract(number1, number2);
                    HistoryTextDisplay.Text = $"{number1} {operation} {number2} =";

                    number1 = Convert.ToDouble(_cc.Subtract(number1, number2));
                    number2 = 0;
                    operation = "";
                    break;
                case "*":
                    ResultTextDisplay.Text = _cc.Multiply(number1, number2);
                    HistoryTextDisplay.Text = $"{number1} {operation} {number2} =";

                    number1 = Convert.ToDouble(_cc.Multiply(number1, number2));
                    number2 = 0;
                    operation = "";
                    break;
                case "/":
                    ResultTextDisplay.Text = _cc.Divide(number1, number2);
                    HistoryTextDisplay.Text = $"{number1} {operation} {number2} =";

                    number1 = Convert.ToDouble(_cc.Divide(number1, number2));
                    number2 = 0;
                    operation = "";
                    break;
            }
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 = 0;
                HistoryTextDisplay.Text = "";
            }
            else
            {
                number2 = 0;
                HistoryTextDisplay.Text = "";
            }
            ResultTextDisplay.Text = "0";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            number1 = 0;
            number2 = 0;
            operation = "";
            ResultTextDisplay.Text = "0";
            HistoryTextDisplay.Text = "";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextDisplay.Text == "0")
            {
                return; // If the value is empty, does nothing
            }
            if (operation == "") // Handles digit deletion for first value
            {
                var num1 = number1.ToString();
                if (num1.Length == 1)
                {
                    number1 = 0;
                    ResultTextDisplay.Text = number1.ToString();
                }
                else
                {
                    number1 = Convert.ToDouble(number1.ToString().Substring(0, ResultTextDisplay.Text.Length - 1));
                    ResultTextDisplay.Text = number1.ToString();
                }
            }
            else  // Handles digit deletion for second value
            {
                var num2 = number2.ToString();
                if (num2.Length == 1)
                {
                    number2 = 0;
                    ResultTextDisplay.Text = number2.ToString();
                }
                else
                {
                    number2 = Convert.ToDouble(number2.ToString().Substring(0, ResultTextDisplay.Text.Length - 1));
                    ResultTextDisplay.Text = number2.ToString();
                }
            }
        }

        private void PositiveNegativeButton_Click(object sender, RoutedEventArgs e)
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

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button bc = (Button)sender;

            if (operation == "")
            {
                number1 = (number1 * 10) + Convert.ToDouble(bc.Content);
                ResultTextDisplay.Text = number1.ToString();
            }
            else
            {
                number2 = (number2 * 10) + Convert.ToDouble(bc.Content);
                ResultTextDisplay.Text = number2.ToString();
            }
        }

        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
