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
    /// Логика взаимодействия для NavigateEmployeesAdmin.xaml
    /// </summary>
    public partial class NavigateEmployeesAdmin : Page
    {
        public NavigateEmployeesAdmin()
        {
            InitializeComponent();
        }
        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new EmployeePageAdmin();
            EmployeeButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            EmployeeInfoButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            EmployeeDismissalButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }

        private void EmployeeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new Info_employeePageAdmin();
            EmployeeButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            EmployeeInfoButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            EmployeeDismissalButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }

        private void EmployeeDismissalButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new DismissalEmployeePageAdmin();
            EmployeeButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            EmployeeInfoButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            EmployeeDismissalButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NavigatePageAdmin());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FrameNavigation.Content = new EmployeePageAdmin();
            EmployeeButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            EmployeeInfoButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            EmployeeDismissalButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                NavigationService.Navigate(new RegulationsPage());
            }
        }
    }
}
