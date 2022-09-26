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
    /// Логика взаимодействия для AddInfo_employeePage.xaml
    /// </summary>
    public partial class AddInfo_employeePage : Page
    {
        private Info_employees _currentInfo_employees = new Info_employees();
        public AddInfo_employeePage(Info_employees selectedInfo_employee)
        {
            InitializeComponent();
            if (selectedInfo_employee != null)
            {
                _currentInfo_employees = selectedInfo_employee;
            }
            DataContext = _currentInfo_employees;
            employee_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Employees.ToList();
            position_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Positions.ToList();
            departament_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Departaments.ToList();

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentInfo_employees.id_info_employee == 0)
            {
                personal_accounting_DBEntities.GetContext().Info_employees.Add(_currentInfo_employees);
            }
            try
            {

                if (_currentInfo_employees.disability == null)
                {
                    _currentInfo_employees.disability = false;
                }
                personal_accounting_DBEntities.GetContext().SaveChanges();
                NavigationService.Navigate(new Info_employeePageAdmin());
                MessageBox.Show("Информация сохраниена");
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }


        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Info_employeePageAdmin());
        }

    }
}
