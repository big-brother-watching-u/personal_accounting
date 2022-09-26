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
    /// Логика взаимодействия для VacationPage.xaml
    /// </summary>
    public partial class VacationPage : Page
    {
        public VacationPage()
        {
            InitializeComponent();
            DGridVacation.ItemsSource = personal_accounting_DBEntities.GetContext().Vacations.ToList();
        }
    }
}
