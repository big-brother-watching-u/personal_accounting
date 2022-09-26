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
    /// Логика взаимодействия для AddVacationPage.xaml
    /// </summary>
    public partial class AddVacationPage : Page
    {
        private Vacations _currentVacations = new Vacations();
        public AddVacationPage(Vacations selectedVacation)
        {
            InitializeComponent();
            if (selectedVacation != null)
            {
                _currentVacations = selectedVacation;
            }
            DataContext = _currentVacations;
            employee_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Employees.ToList();

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentVacations.vacation_id == 0)
            {
                personal_accounting_DBEntities.GetContext().Vacations.Add(_currentVacations);
            }
            try
            {
                personal_accounting_DBEntities.GetContext().SaveChanges();
                NavigationService.Navigate(new VacationPageAdmin());
                MessageBox.Show("Информация сохраниена");
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VacationPageAdmin());
        }
    }
}
