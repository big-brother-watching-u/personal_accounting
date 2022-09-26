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
    /// Логика взаимодействия для RegulationsPage.xaml
    /// </summary>
    public partial class RegulationsPage : Page
    {
        public RegulationsPage()
        {
            InitializeComponent();
        }

        private void routineInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var helper = new WordHelper("Правила-трудового-распорядка.doc");
            var items = new Dictionary<string, string> { };
            helper.Process(items);
        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            var helper = new WordHelper("Оплата-труда.doc");
            var items = new Dictionary<string, string> { };
            helper.Process(items);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NavigateEmployeesAdmin());
        }
    }
}
