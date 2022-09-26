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
    /// Логика взаимодействия для Info_employeePageAdmin.xaml
    /// </summary>
    public partial class Info_employeePageAdmin : Page
    {
        public Info_employeePageAdmin()
        {
            InitializeComponent();
            DGridInfo_employee.ItemsSource = personal_accounting_DBEntities.GetContext().Info_employees.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddInfo_employeePage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var IempForRemoving = DGridInfo_employee.SelectedItems.Cast<Info_employees>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {IempForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().Info_employees.RemoveRange(IempForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridInfo_employee.ItemsSource = personal_accounting_DBEntities.GetContext().Info_employees.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddInfo_employeePage((sender as Button).DataContext as Info_employees));
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DGridInfo_employee.ItemsSource = personal_accounting_DBEntities.GetContext().Info_employees.ToList();
            }
        }

        private void BtnRecept_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                var Iemp = (sender as Button).DataContext as Info_employees;
                int employee_id = Convert.ToInt32(Iemp.employee_id);
                int positions_id = Convert.ToInt32(Iemp.position_id);
                string Iemp_id = Convert.ToString(Iemp.id_info_employee);
                int departament_id = Convert.ToInt32(Iemp.departament_id);
                Employees emp = db.Employees.Where(d => d.employee_id == employee_id).FirstOrDefault();
                Positions pos = db.Positions.Where(d => d.position_id == positions_id).FirstOrDefault();
                Departaments dep = db.Departaments.Where(d => d.departament_id == departament_id).FirstOrDefault();
                string emp_id = Convert.ToString(emp.employee_id);
                string surname = Convert.ToString(emp.surname);
                string name = Convert.ToString(emp.name);
                string patronymic = Convert.ToString(emp.patronymic);
                DateTime dateReception = Convert.ToDateTime(Iemp.reception_date);
                string dateRec = Convert.ToString(dateReception.ToString("MM.dd.yyyy"));
                string MM = Convert.ToString(dateReception.ToString("MM"));
                string DD = Convert.ToString(dateReception.ToString("dd"));
                string YY = Convert.ToString(dateReception.ToString("yy"));
                DateTime dateStartActive = Convert.ToDateTime(Iemp.start_activity);
                string dateStart = Convert.ToString(dateStartActive.ToString("MM.dd.yyyy"));
                string departament = Convert.ToString(dep.departament_name);
                string position = Convert.ToString(pos.positions_name);
                string rateWork = Convert.ToString(Iemp.rate_work);
                string money = Convert.ToString(Iemp.salary);

                Documents _contextdoc = new Documents
                {
                    name_doc = "T-1-Приказ-о-приеме-на-работу"
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
                var helper = new WordHelper("T-1-Приказ-о-приеме-на-работу.doc");
                var items = new Dictionary<string, string>
            {
                { "<D_id>", Convert.ToString(document.id_doc)},
                {"<Iemp_id>",Iemp_id},
                { "<Surname>",surname },
                { "<Name>",name },
                { "<empMoney>",money },
                { "<Patronymic>",patronymic },
                { "<DateStart>",dateStart },
                { "<RecDate>",dateRec },
                { "<Departament>",departament },
                { "<Position>",position },
                { "<RateWork>",rateWork },
                { "M",MM },
                { "D",DD },
                { "Y",YY },
            };
                helper.Process(items);
            }
        }
    }
}