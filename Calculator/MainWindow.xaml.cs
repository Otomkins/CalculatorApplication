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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double number1 = 0;
        double number2 = 0;
        string operation = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            operation = "+";
            txtDisplay.Text = "+";
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            operation = "-";
            txtDisplay.Text = "-";
        }

        private void btnMultiply_Click(object sender, RoutedEventArgs e)
        {
            operation = "*";
            txtDisplay.Text = "*";
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            operation = "/";
            txtDisplay.Text = "/";
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            switch(operation)
            {
                case "+":
                    txtDisplay.Text = CalculatorMethods.Add(number1, number2); // Displays answer through method
                    txtDisplay2.Text = $"{number1} {operation} {number2} ="; // Displays equation

                    number1 = Convert.ToDouble(CalculatorMethods.Add(number1, number2)); // Changes number1 to result for further calculations
                    number2 = 0;
                    operation = "";
                    break;
                case "-":
                    txtDisplay.Text = CalculatorMethods.Subtract(number1, number2);
                    txtDisplay2.Text = $"{number1} {operation} {number2} =";

                    number1 = Convert.ToDouble(CalculatorMethods.Subtract(number1, number2));
                    number2 = 0;
                    operation = "";
                    break;
                case "*":
                    txtDisplay.Text = CalculatorMethods.Multiply(number1, number2);
                    txtDisplay2.Text = $"{number1} {operation} {number2} =";

                    number1 = Convert.ToDouble(CalculatorMethods.Multiply(number1, number2));
                    number2 = 0;
                    operation = "";
                    break;
                case "/":
                    txtDisplay.Text = CalculatorMethods.Divide(number1, number2);
                    txtDisplay2.Text = $"{number1} {operation} {number2} =";

                    number1 = Convert.ToDouble(CalculatorMethods.Divide(number1, number2));
                    number2 = 0;
                    operation = "";
                    break;
            }
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if(operation == "")
            {
                number1 = 0;
                txtDisplay2.Text = "";
            }
            else
            {
                number2 = 0;
                txtDisplay2.Text = "";
            }
            txtDisplay.Text = "0";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            number1 = 0;
            number2 = 0;
            operation = "";
            txtDisplay.Text = "0";
            txtDisplay2.Text = "";
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "0")
            {
                return; // If the value is empty, does nothing
            }
            if (operation == "") // Handles digit deletion for first value
            {
                var num1 = number1.ToString();
                if (num1.Length == 1)
                {
                    number1 = 0;
                    txtDisplay.Text = number1.ToString();
                }
                else
                {
                    number1 = Convert.ToDouble(number1.ToString().Substring(0, txtDisplay.Text.Length - 1));
                    txtDisplay.Text = number1.ToString();
                }
            }
            else  // Handles digit deletion for second value
            {
                var num2 = number2.ToString();
                if (num2.Length == 1)
                {
                    number2 = 0;
                    txtDisplay.Text = number2.ToString();
                }
                else
                {
                    number2 = Convert.ToDouble(number2.ToString().Substring(0, txtDisplay.Text.Length - 1));
                    txtDisplay.Text = number2.ToString();
                }
            }
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 *= -1;
                txtDisplay.Text = number1.ToString();
            }
            else
            {
                number2 *= -1;
                txtDisplay.Text = number2.ToString();
            }
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button bc = (Button)sender;

            if (operation == "")
            {
                number1 = (number1 * 10) + Convert.ToDouble(bc.Content);
                txtDisplay.Text = number1.ToString();
            }
            else
            {
                number2 = (number2 * 10) + Convert.ToDouble(bc.Content);
                txtDisplay.Text = number2.ToString();
            }
        }

        private void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
