using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для VacationPageAdmin.xaml
    /// </summary>
    public partial class VacationPageAdmin : Page
    {
        public VacationPageAdmin()
        {
            InitializeComponent();
            DGridVacation.ItemsSource = personal_accounting_DBEntities.GetContext().Vacations.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVacationPage((sender as Button).DataContext as Vacations));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVacationPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var VacatForRemoving = DGridVacation.SelectedItems.Cast<Vacations>().ToList();
            string message = $"Вы точно хотите удалить данные в количестве {VacatForRemoving.Count} шт?";
            string caption = "Внимание";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    personal_accounting_DBEntities.GetContext().Vacations.RemoveRange(VacatForRemoving);
                    personal_accounting_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridVacation.ItemsSource = personal_accounting_DBEntities.GetContext().Vacations.ToList();
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
                DGridVacation.ItemsSource = personal_accounting_DBEntities.GetContext().Vacations.ToList();
            }
        }
        private void BtnVacation_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                Dictionary<string, string> items = new Dictionary<string, string>();

                DateTime dateNow = DateTime.Now;
                DateTime dateNowMax = dateNow.AddDays(+30);
                Vacations[] vacatMax = db.Vacations.Where(h => h.date_start_vacation > dateNow).ToArray();
                Vacations[] vacatMin = vacatMax.Where(h => h.date_start_vacation < dateNowMax).ToArray();
                string dateN = Convert.ToString(dateNow.ToString("MM.dd.yyyy"));
                string MM = Convert.ToString(dateNow.ToString("MM"));
                string DD = Convert.ToString(dateNow.ToString("dd"));
                string YY = Convert.ToString(dateNow.ToString("yy"));
                string YYYY = Convert.ToString(dateNow.ToString("yyyy"));
                string vacat_id = Convert.ToString(vacatMin);

                Documents _contextdoc = new Documents
                {
                    name_doc = "T-7-График-отпусков"
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


                var helper = new WordHelper("T-7-График-отпусков.doc");
                var document = db.Documents.OrderByDescending(x => x.id_doc).FirstOrDefault();
                for (int n = 0; n < 9; n++)
                {
                    if (n < vacatMin.Length)
                    {
                        int vac = Convert.ToInt32(vacatMin[n].employee_id);
                        Employees emp = db.Employees.Where(h => h.employee_id == vac).FirstOrDefault();
                        Info_employees Iemp = db.Info_employees.Where(h => h.employee_id == emp.employee_id).FirstOrDefault();
                        Departaments dep = db.Departaments.Where(h => h.departament_id == Iemp.departament_id).FirstOrDefault();
                        Positions pos = db.Positions.Where(h => h.position_id == Iemp.position_id).FirstOrDefault();

                        items.Add("<Departament" + n + ">", Convert.ToString(dep.departament_name));
                        items.Add("<Position" + n + ">", Convert.ToString(pos.positions_name));
                        items.Add("<Surname" + n + ">", Convert.ToString(emp.surname));
                        items.Add("<Name" + n + ">", Convert.ToString(emp.name));
                        items.Add("<Patronymic" + n + ">", Convert.ToString(emp.patronymic));
                        items.Add("<emp_id" + n + ">", Convert.ToString(emp.employee_id));
                        items.Add("<quant" + n + ">", Convert.ToString(vacatMin[n].quantity_day));
                        DateTime dateStart = Convert.ToDateTime(vacatMin[n].date_start_vacation);
                        items.Add("<date_st" + n + ">", Convert.ToString(dateStart.ToString("MM.dd.yyyy")));
                    }
                    else
                    {
                        items.Add("<Departament" + n + ">", null);
                        items.Add("<Position" + n + ">", null);
                        items.Add("<Surname" + n + ">", null);
                        items.Add("<Name" + n + ">", null);
                        items.Add("<Patronymic" + n + ">", null);
                        items.Add("<emp_id" + n + ">", null);
                        items.Add("<quant" + n + ">", null);
                        items.Add("<date_st" + n + ">", null);
                    }
                }
                items.Add("<DateNow>", Convert.ToString(dateN));
                items.Add("<D_id>", Convert.ToString(document.id_doc));
                items.Add("YY", Convert.ToString(YYYY));
                items.Add("Y", Convert.ToString(YY));
                items.Add("M", Convert.ToString(MM));
                items.Add("D", Convert.ToString(DD));
                helper.Process(items);
            }
        }
    }
}
