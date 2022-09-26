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
    /// Логика взаимодействия для AddDepartamentPage.xaml
    /// </summary>
    public partial class AddDepartamentPage : Page
    {
        private Departaments _currentDepartaments = new Departaments();
        public AddDepartamentPage(Departaments selectedDepartament)
        {
            if (selectedDepartament != null)
            {
                _currentDepartaments = selectedDepartament;
            }
            InitializeComponent();
            DataContext = _currentDepartaments;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentDepartaments.departament_id == 0)
            {
                personal_accounting_DBEntities.GetContext().Departaments.Add(_currentDepartaments);
            }
            try
            {
                personal_accounting_DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохраниена");
                NavigationService.Navigate(new DepartamentPageAdmin());
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartamentPageAdmin());
        }
    }
}
