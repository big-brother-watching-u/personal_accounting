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
    /// Логика взаимодействия для AddPositionPage.xaml
    /// </summary>
    public partial class AddPositionPage : Page
    {
        private Positions _currentPositions = new Positions();
        public AddPositionPage(Positions selectedPosition)
        {
            if (selectedPosition != null)
            {
                _currentPositions = selectedPosition;
            }
            InitializeComponent();
            DataContext = _currentPositions;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PositionPageAdmin());
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentPositions.position_id == 0)
            {
                personal_accounting_DBEntities.GetContext().Positions.Add(_currentPositions);
            }
            try
            {
                personal_accounting_DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохраниена");
                NavigationService.Navigate(new PositionPageAdmin());
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректно введены данные");
            }
        }
    }
}
