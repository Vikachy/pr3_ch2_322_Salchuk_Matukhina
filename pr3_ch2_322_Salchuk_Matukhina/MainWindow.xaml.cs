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

namespace pr3_ch2_322_Salchuk_Matukhina
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполненность текстовых полей
            if (string.IsNullOrWhiteSpace(Operand1TextBox.Text) || string.IsNullOrWhiteSpace(Operand2TextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все текстовые поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка на выбор операции
            if (!AdditionRadioButton.IsChecked.Value && !SubtractionRadioButton.IsChecked.Value &&
                !MultiplicationRadioButton.IsChecked.Value && !DivisionRadioButton.IsChecked.Value)
            {
                MessageBox.Show("Пожалуйста, выберите арифметическую операцию.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Попытка выполнить вычисление
            try
            {
                double operand1 = Convert.ToDouble(Operand1TextBox.Text);
                double operand2 = Convert.ToDouble(Operand2TextBox.Text);
                double result = 0;

                if (AdditionRadioButton.IsChecked == true)
                    result = operand1 + operand2;
                else if (SubtractionRadioButton.IsChecked == true)
                    result = operand1 - operand2;
                else if (MultiplicationRadioButton.IsChecked == true)
                    result = operand1 * operand2;
                else if (DivisionRadioButton.IsChecked == true)
                {
                    if (operand2 == 0)
                        throw new DivideByZeroException();
                    result = operand1 / operand2;
                }

                ResultTextBlock.Text = $"Результат: {result}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Ошибка: Деление на ноль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //проверка ввода данных

        private void Operand1TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Operand2TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '.' && c != '-')
                    return false;
            }
            return true;
        }
    }

}

