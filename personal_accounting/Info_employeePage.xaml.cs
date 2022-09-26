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
    /// Логика взаимодействия для Info_employeePage.xaml
    /// </summary>
    public partial class Info_employeePage : Page
    {
        public Info_employeePage()
        {
            InitializeComponent();
            DGridInfo_employee.ItemsSource = personal_accounting_DBEntities.GetContext().Info_employees.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DGridInfo_employee.ItemsSource = personal_accounting_DBEntities.GetContext().Info_employees.ToList();
        }
    }
}
