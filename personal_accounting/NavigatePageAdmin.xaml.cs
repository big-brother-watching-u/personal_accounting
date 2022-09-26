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
    /// Логика взаимодействия для NavigatePageAdmin.xaml
    /// </summary>
    public partial class NavigatePageAdmin : Page
    {
        public NavigatePageAdmin()
        {
            InitializeComponent();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NavigateEmployeesAdmin());
        }

        private void VacanciesButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new VacancyPageAdmin();
            VacationButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            PossitionsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            DepartamentsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            StateTimeButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            VacanciesButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
        }

        private void PossitionsButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new PositionPageAdmin();
            VacationButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            PossitionsButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            DepartamentsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            StateTimeButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            VacanciesButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }

        private void DepartamentsButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new DepartamentPageAdmin();
            VacationButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            PossitionsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            DepartamentsButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            StateTimeButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            VacanciesButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }

        private void StateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigation.Content = new State_timePageAdmin();
            VacationButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            PossitionsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            DepartamentsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            StateTimeButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            VacanciesButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }

        private void WaitButton_Click(object sender, RoutedEventArgs e)
        {
            string message = $"Вы действительно хотите выйти?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                var win = Application.Current.Windows;
                win[0].Close();
            }
        }

        private void VacationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NavigateVacationPage());
            VacationButton.Background = (Brush)new BrushConverter().ConvertFrom("#9c62f1");
            PossitionsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            DepartamentsButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            StateTimeButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
            VacanciesButton.Background = (Brush)new BrushConverter().ConvertFrom("#673ab7");
        }
    }
}
