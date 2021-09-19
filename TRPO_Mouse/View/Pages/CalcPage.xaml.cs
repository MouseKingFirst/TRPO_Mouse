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

namespace TRPO_Mouse.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CalcPage.xaml
    /// </summary>
    public partial class CalcPage : Page
    {

        double resultValue = 0;
        string operationPerformed = "";
        bool isOperationPerformed = false;
        bool isOperationDown = false;

        public CalcPage()
        {
            InitializeComponent();

        }
        private void button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if ((textBox_Result.Text == "0") && (button.Content.ToString() != ",") || (isOperationPerformed))
            {
                textBox_Result.Clear();
                textBox_Result.Text = "0";
            }

            isOperationPerformed = false;
            if (button.Content.ToString() == ",")
            {
                if (!textBox_Result.Text.Contains(','))
                {
                    textBox_Result.Text = textBox_Result.Text + button.Content.ToString();
                }
                else if (isOperationPerformed == true)
                {
                    textBox_Result.Text = textBox_Result.Text + "0" + button.Content.ToString();
                }
                    
            }
            else if (button.Content.ToString() != "," && isOperationDown != true)
            {
                if (textBox_Result.Text != "0")
                {
                    textBox_Result.Text = textBox_Result.Text + button.Content.ToString();
                }
                else
                {
                    textBox_Result.Text = button.Content.ToString();
                }  
            }
                
            else if (isOperationDown == true)
            {
                isOperationDown = false;
                textBox_Result.Text = button.Content.ToString();
            }
        }

        private void operator_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (resultValue != 0)
            {
                ButtonRavno_Click(ButtonRavno, null);
                operationPerformed = button.Content.ToString();
                isOperationPerformed = true;
                textBox_Result.Text = resultValue + " " + operationPerformed;
            }
            else
            {

                operationPerformed = button.Content.ToString();
                resultValue = double.Parse(textBox_Result.Text);
                isOperationPerformed = true;
                textBox_Result.Text = resultValue + " " + operationPerformed;
            }
        }

        private void ClearCe_Click(object sender, RoutedEventArgs e)
        {
            textBox_Result.Text = "0";
        }

        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            textBox_Result.Text = "0";
            resultValue = 0;
            textBox_Result.Text = "";
        }

        private void ButtonRavno_Click(object sender, RoutedEventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    textBox_Result.Text = (resultValue + double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "-":
                    textBox_Result.Text = (resultValue - double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "\u00D7":
                    textBox_Result.Text = (resultValue * double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "\u00F7":
                    textBox_Result.Text = (resultValue / double.Parse(textBox_Result.Text)).ToString();
                    break;
                default:
                    break;
            }
            operationPerformed = "";
            isOperationDown = true;
            resultValue = double.Parse(textBox_Result.Text);
            

        }


        private void pow2(object sender, RoutedEventArgs e)
        {
            isOperationPerformed = true;
            double temp = double.Parse(textBox_Result.Text);
            textBox_Result.Text = (Math.Pow(double.Parse(textBox_Result.Text), 2)).ToString();
            resultValue = double.Parse(textBox_Result.Text);
        }

        private void pow3(object sender, RoutedEventArgs e)
        {
            isOperationPerformed = true;
            double temp = double.Parse(textBox_Result.Text);
            textBox_Result.Text = (Math.Pow(double.Parse(textBox_Result.Text), 3)).ToString();
            resultValue = double.Parse(textBox_Result.Text);

        }

        private void sqrtkor(object sender, RoutedEventArgs e)
        {
            isOperationPerformed = true;
            if (double.Parse(textBox_Result.Text) > 0)
            {
                double temp = double.Parse(textBox_Result.Text);
                textBox_Result.Text = (Math.Sqrt(double.Parse(textBox_Result.Text))).ToString();
                resultValue = double.Parse(textBox_Result.Text);
            }
        }


        private void operator_plusminus(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(textBox_Result.Text);
            if (a != 0)
            {
                a = (-1) * a;
                string q = textBox_Result.Text;
                if (a > 0)
                {
                    textBox_Result.Text = q.Substring(1, q.Length - 1);
                }
                else
                {
                    textBox_Result.Text = "-" + textBox_Result.Text;
                }
                resultValue = double.Parse(textBox_Result.Text);
            }
        }
    }
}