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
    /// Логика взаимодействия для EmployeePageAdmin.xaml
    /// </summary>
    public partial class EmployeePageAdmin : Page
    {
        public EmployeePageAdmin()
        {
            InitializeComponent();
            DGridEmployee.ItemsSource = personal_accounting_DBEntities.GetContext().Employees.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeePage((sender as Button).DataContext as Employees));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeePage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var empForRemoving = DGridEmployee.SelectedItems.Cast<Employees>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {empForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().Employees.RemoveRange(empForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridEmployee.ItemsSource = personal_accounting_DBEntities.GetContext().Employees.ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить");
                }
            }
        }

        private void Page_is_visibled(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DGridEmployee.ItemsSource = personal_accounting_DBEntities.GetContext().Employees.ToList();
            }
        }

        private void BtnPersonalCard_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                var emp = (sender as Button).DataContext as Employees;
                int employee_id = Convert.ToInt32(emp.employee_id);
                Info_employees Iemp = db.Info_employees.Where(d => d.employee_id == employee_id).FirstOrDefault();
                string Iemp_id = Convert.ToString(Iemp.id_info_employee);
                string emp_id = Convert.ToString(emp.employee_id);
                string surname = Convert.ToString(emp.surname);
                string name = Convert.ToString(emp.name);
                string patronymic = Convert.ToString(emp.patronymic);
                DateTime dateReception = Convert.ToDateTime(Iemp.reception_date);
                string dateRec = Convert.ToString(dateReception.ToString("MM.dd.yyyy"));
                DateTime dateOfBirth = Convert.ToDateTime(emp.date_of_birth);
                string dateBirth = Convert.ToString(dateOfBirth.ToString("MM.dd.yyyy"));
                DateTime dateRegPass = Convert.ToDateTime(emp.date_reg_passport);
                string MM = Convert.ToString(dateRegPass.ToString("MM"));
                string DD = Convert.ToString(dateRegPass.ToString("dd"));
                string YY = Convert.ToString(dateRegPass.ToString("yy"));
                string orgPass = Convert.ToString(emp.organisation_passport);
                string numPass = Convert.ToString(emp.data_passport);
                string gender = Convert.ToString(emp.gender);
                string city = Convert.ToString(emp.address_city);
                string address = Convert.ToString(emp.home_address);
                string rateWork = Convert.ToString(Iemp.rate_work);
                string numbrTelephone = Convert.ToString(emp.number_of_telephone);

                Documents _contextdoc = new Documents
                {
                    name_doc = "T-2-Бланк-личная-карточка-работника"
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
                var helper = new WordHelper("T-2-Бланк-личная-карточка-работника.doc");
                var items = new Dictionary<string, string>
            {
                { "<D_id>", Convert.ToString(document.id_doc) },
                { "<emp_id>", emp_id },
                {"<Iemp_id>",Iemp_id},
                { "<Surname>",surname },
                { "<Name>",name },
                { "<Patronymic>",patronymic },
                { "<DateRec>",dateRec },
                { "<RateWork>",rateWork },
                {"<NumPass>",numPass },
                { "MP",MM },
                { "DP",DD },
                { "YP",YY },
                {"<Gend>",gender},
                {"<OrgPass>",orgPass},
                {"<DateBirth>",dateBirth},
                {"<City>", city},
                {"<numberTelephone>", numbrTelephone},
                {"<Address>",address}
            };
                helper.Process(items);
            }
        }

        private void BtnTreatment_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                var emp = (sender as Button).DataContext as Employees;
                int employee_id = Convert.ToInt32(emp.employee_id);
                Info_employees Iemp = db.Info_employees.Where(d => d.employee_id == employee_id).FirstOrDefault();
                string surname = Convert.ToString(emp.surname);
                string name = Convert.ToString(emp.name);
                string patronymic = Convert.ToString(emp.patronymic);
                DateTime dateReception = Convert.ToDateTime(Iemp.reception_date);
                string dateRec = Convert.ToString(dateReception.ToString("MM.dd.yyyy"));
                DateTime dateRegPass = Convert.ToDateTime(emp.date_reg_passport);
                string dateReg = Convert.ToString(dateRegPass.ToString("MM.dd.yyyy"));
                string orgPass = Convert.ToString(emp.organisation_passport);
                string numPass = Convert.ToString(emp.data_passport);
                string city = Convert.ToString(emp.address_city);
                string address = Convert.ToString(emp.home_address);
                var helper = new WordHelper("Бланк-заявления-о-согласии-на-обработку-персональных-данных.doc");
                var items = new Dictionary<string, string>
            {
                { "<Surname>",surname },
                { "<Name>",name },
                { "<Patronymic>",patronymic },
                { "<DateRec>",dateRec },
                {"<DataPass>",numPass },
                {"<OrgPass>",orgPass},
                {"<City>", city},
                {"<Address>", address},
                {"<DateRegPass>", dateReg},
            };
                helper.Process(items);
            }
        }
    }
}
