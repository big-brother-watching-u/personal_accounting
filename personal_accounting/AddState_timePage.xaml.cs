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
    /// Логика взаимодействия для AddState_timePage.xaml
    /// </summary>
    public partial class AddState_timePage : Page
    {
        private State_time _currentState_time = new State_time();
        public AddState_timePage(State_time selectedState_time)
        {
            if (selectedState_time != null)
            {
                _currentState_time = selectedState_time;
            }
            InitializeComponent();
            DataContext = _currentState_time;
            position_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Positions.ToList();
            departament_idBox.ItemsSource = personal_accounting_DBEntities.GetContext().Departaments.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentState_time.state_unit_id == 0)
            {
                personal_accounting_DBEntities.GetContext().State_time.Add(_currentState_time);
            }
            try
            {
                personal_accounting_DBEntities.GetContext().SaveChanges();
                NavigationService.Navigate(new State_timePageAdmin());
                MessageBox.Show("Информация сохраниена");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new State_timePageAdmin());
        }
    }
}
