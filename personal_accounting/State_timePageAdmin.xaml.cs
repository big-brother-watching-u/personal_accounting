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
    /// Логика взаимодействия для State_timePageAdmin.xaml
    /// </summary>
    public partial class State_timePageAdmin : Page
    {
        public State_timePageAdmin()
        {
            InitializeComponent();
            DGridState_time.ItemsSource = personal_accounting_DBEntities.GetContext().State_time.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddState_timePage((sender as Button).DataContext as State_time));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddState_timePage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var STForRemoving = DGridState_time.SelectedItems.Cast<State_time>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {STForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().State_time.RemoveRange(STForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridState_time.ItemsSource = personal_accounting_DBEntities.GetContext().State_time.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DGridState_time.ItemsSource = personal_accounting_DBEntities.GetContext().State_time.ToList();
            }
        }

        private void BtnStateTime_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                var Stime = (sender as Button).DataContext as State_time;
                int positions_id = Convert.ToInt32(Stime.position_id);
                int departament_id = Convert.ToInt32(Stime.departament_id);
                Positions pos = db.Positions.Where(d => d.position_id == positions_id).FirstOrDefault();
                Departaments dep = db.Departaments.Where(d => d.departament_id == departament_id).FirstOrDefault();
                string dateDis = Convert.ToString(DateTime.Now.ToString("MM.dd.yyyy"));
                string MM = Convert.ToString(DateTime.Now.ToString("MM"));
                string DD = Convert.ToString(DateTime.Now.ToString("dd"));
                string YY = Convert.ToString(DateTime.Now.ToString("yy"));
                string stateUnit_id = Convert.ToString(Stime.state_unit_id);
                string stateAmount = Convert.ToString(Stime.quantity_state_unit);
                string departament = Convert.ToString(dep.departament_name);
                string position = Convert.ToString(pos.positions_name);

                Documents _contextdoc = new Documents
                {
                    name_doc = "Приказ-на-утверждение-штата"
                };
                personal_accounting_DBEntities.GetContext().Documents.Add(_contextdoc);
                try
                {
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                var document = db.Documents.OrderByDescending(x => x.id_doc).FirstOrDefault();
                var helper = new WordHelper("Приказ-на-утверждение-штата.doc");
                var items = new Dictionary<string, string>
            {
                { "<D_id>", Convert.ToString(document.id_doc) },
                { "<SAm>",stateAmount },
                { "<Positions>",position },
                { "MN",MM },
                { "DN",DD },
                { "YN",YY },
            };
                helper.Process(items);
            }
        }
    }
}
