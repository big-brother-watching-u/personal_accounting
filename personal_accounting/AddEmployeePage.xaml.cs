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

namespace personal_accounting
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        private Employees _currentEmployees = new Employees();
        public AddEmployeePage(Employees selectedEmployee)
        {
            if (selectedEmployee != null)
            {
                _currentEmployees = selectedEmployee;
            }
            InitializeComponent();
            DataContext = _currentEmployees;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentEmployees.amount_children < 0)
            {
                errors.AppendLine("Некорректный ввод Количества детей");
            }
            if (_currentEmployees.password == null || _currentEmployees.password.Length < 5)
            {
                errors.AppendLine("Некорректный ввод пароля");
            }
            if (_currentEmployees.login == null || _currentEmployees.login.Length < 5)
            {
                errors.AppendLine("Некорректный ввод логина");
            }
            if (_currentEmployees.email==null ||_currentEmployees.email.Length < 5 || !_currentEmployees.email.Contains("@") || !_currentEmployees.email.Contains("."))
            {
                errors.AppendLine("Некорректный ввод email");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
            }
            else
            {
                if (_currentEmployees.employee_id == 0)
                {
                    personal_accounting_DBEntities.GetContext().Employees.Add(_currentEmployees);
                }
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new EmployeePageAdmin());
                try
                {
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Неверные данные");
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePageAdmin());
        }
    }
}
