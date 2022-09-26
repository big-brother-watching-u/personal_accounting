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
    /// Логика взаимодействия для AddVacancyPage.xaml
    /// </summary>
    public partial class AddVacancyPage : Page
    {
        private Vacancies _currentVacancies = new Vacancies();
        public AddVacancyPage(Vacancies selectedVacancy)
        {
            if (selectedVacancy != null)
            {
                _currentVacancies = selectedVacancy;
            }
            InitializeComponent();
            DataContext = _currentVacancies;
            position_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Positions.ToList();
            departament_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Departaments.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentVacancies.vacancies_id == 0)
            {
                personal_accounting_DBEntities.GetContext().Vacancies.Add(_currentVacancies);
            }
            MessageBox.Show("Информация сохраниена");
            NavigationService.Navigate(new VacancyPageAdmin());
            try
            {
                personal_accounting_DBEntities.GetContext().SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VacancyPageAdmin());
        }
    }
}
