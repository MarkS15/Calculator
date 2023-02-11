using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Numbers numbers = new Numbers(0, '+');
        public MainWindow()
        {
            InitializeComponent();
            LineResult.Text = string.Empty;
            LineOperating.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            string currentNumber = button.Name.Trim(new char[] { 'B','u','t','o','n'});

            if (((LineOperating.Text is null || LineOperating.Text == "" || LineOperating.Text == "0") && (currentNumber == "00" || currentNumber == "0")))
                LineOperating.Text = "0";
            else
                LineOperating.Text += currentNumber;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAction('+'))
                AddNumber('+');
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAction('-'))
                AddNumber('-');
        }

        private void ButtonMultiply_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAction('*'))
                AddNumber('*');
        }

        private void ButtonDivide_Click(object sender, RoutedEventArgs e)
        {
                if(CheckAction('÷'))
                AddNumber('÷');
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            AddNumber('=');
            LineResult.Text +=numbers.Result();
            LineOperating.Text = string.Empty;  
        }

        private void AddNumber(char action)
        {
            if (float.TryParse(LineOperating.Text, out float value))
            numbers.New_number(value, action);
             else
                if (float.TryParse(LineOperating.Text.Substring(0, LineOperating.Text.Length - 1), out value))
                    numbers.Proc(value, action);
                 else 
                    numbers.New_number((float)Math.Sqrt(float.Parse(LineOperating.Text.Substring(1))) , action);
            
            LineResult.Text += String.Concat(LineOperating.Text, action);
            LineOperating.Text = string.Empty;
        }

        private bool CheckAction(char action)
        {
            if (LineResult.Text.Length==0 || LineOperating.Text.Length > 0) return true;
            else
                switch (LineResult.Text[LineResult.Text.Length - 1])
                {
                    case '+': Change_Action(action); return false;
                    case '-': Change_Action(action); return false;
                    case '*': Change_Action(action); return false;
                    case '÷': Change_Action(action); return false;
                    default: return true;
                }
        }
        private void Change_Action(char action)
        {
            LineResult.Text =String.Concat(LineResult.Text.Substring(0,LineResult.Text.Length-1),action);
            numbers.New_action(action);
        }
        
        private void ButtonDOT_Click(object sender, RoutedEventArgs e)
        {
            if (LineOperating.Text[LineOperating.Text.Length - 1] != ',')
            LineOperating.Text += ',';
        }

        private void Delite_Click(object sender, RoutedEventArgs e)
        {
            numbers.Clear();
            LineResult.Text = string.Empty;
            LineOperating.Text = string.Empty;
        }

        private void ButtonSqrt_Click(object sender, RoutedEventArgs e)
        {
            LineOperating.Text += '√';
        }

        private void ButtonProc_Click(object sender, RoutedEventArgs e)
        {
            LineOperating.Text += '%';
        }
    }
}
