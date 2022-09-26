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
    /// Логика взаимодействия для State_timePage.xaml
    /// </summary>
    public partial class State_timePage : Page
    {
        public State_timePage()
        {
            InitializeComponent();
            DGridState_time.ItemsSource = personal_accounting_DBEntities.GetContext().State_time.ToList();
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            DGridState_time.ItemsSource = personal_accounting_DBEntities.GetContext().State_time.ToList();
        }
    }
}
