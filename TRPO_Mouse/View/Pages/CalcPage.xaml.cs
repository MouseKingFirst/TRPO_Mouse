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

        string leftop = ""; // Левый операнд
        string operation = ""; // Знак операции
        string rightop = ""; // Правый операнд

        public CalcPage()
        {
            InitializeComponent();
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текст кнопки
            string s = (string)((Button)e.OriginalSource).Content;
            // Добавляем его в текстовое поле
            textBlock.Text += s;
            int num;
            // Пытаемся преобразовать его в число
            bool result = Int32.TryParse(s, out num);
            // Если текст - это число
            if (result == true)
            {
                // Если операция не задана
                if (operation == "")
                {
                    // Добавляем к левому операнду
                    leftop += s;
                }
                else
                {
                    // Иначе к правому операнду
                    rightop += s;
                }
            }
            // Если было введено не число
            else
            {
                // Если равно, то выводим результат операции
                if (s == "=")
                {
                    Update_RightOp();
                    textBlock.Text += rightop;
                    operation = "";
                }
                // Очищаем поле и переменные
                else if (s == "CLEAR")
                {
                    leftop = "";
                    rightop = "";
                    operation = "";
                    textBlock.Text = "";
                }
                // Получаем операцию
                else
                {
                    // Если правый операнд уже имеется, то присваиваем его значение левому
                    // операнду, а правый операнд очищаем
                    if (rightop != "")
                    {
                        Update_RightOp();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }
        // Обновляем значение правого операнда
        private void Update_RightOp(string lastOper = "")
        {
            int num1, num2;
            bool FsNum = Int32.TryParse(leftop, out num1);
            bool ScNum = Int32.TryParse(leftop, out num2);


            // И выполняем операцию
            switch (operation)
            {
                case "+":
                    rightop = (num1 + num2).ToString();
                    break;
                case "-":
                    rightop = (num1 - num2).ToString();
                    break;
                case "*":
                    rightop = (num1 * num2).ToString();
                    break;
                case "/":
                    rightop = (num1 / num2).ToString();
                    break;
                case "√":
                    rightop = Math.Ceiling(Math.Sqrt(num2)).ToString();
                    break;
                case "log":
                    rightop = Math.Ceiling(Math.Log(num2)).ToString();
                    break;
                case "ln":
                    rightop = Math.Ceiling(Math.Log(num2, Math.E)).ToString();
                    break;
                case "10^x":
                    rightop = Math.Ceiling(Math.Pow(10, num2)).ToString();
                    break;
                    
            }
        }
        static int Factorial(int x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }
    }
}
