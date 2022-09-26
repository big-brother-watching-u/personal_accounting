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
    /// Логика взаимодействия для PositionPageAdmin.xaml
    /// </summary>
    public partial class PositionPageAdmin : Page
    {
        public PositionPageAdmin()
        {
            InitializeComponent();
            DGridPosition.ItemsSource = personal_accounting_DBEntities.GetContext().Positions.ToList();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPositionPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var posForRemoving = DGridPosition.SelectedItems.Cast<Positions>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {posForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().Positions.RemoveRange(posForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridPosition.ItemsSource = personal_accounting_DBEntities.GetContext().Positions.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPositionPage((sender as Button).DataContext as Positions));
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DGridPosition.ItemsSource = personal_accounting_DBEntities.GetContext().Positions.ToList();
            }
        }
    }
}
