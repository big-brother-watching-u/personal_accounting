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
    /// Логика взаимодействия для DismissalEmployeePageAdmin.xaml
    /// </summary>
    public partial class DismissalEmployeePageAdmin : Page
    {
        public DismissalEmployeePageAdmin()
        {
            InitializeComponent();
        }

        private void DGridEmployee_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                List<Employees> emp = db.Employees.ToList();
                List<Info_employees> Iemp = emp.SelectMany(c => c.Info_employees).Where(d => d.dismissal_date != null).ToList();
                DGridDismissal.ItemsSource = Iemp;
            }
        }

        private void BtnDisab_Click(object sender, RoutedEventArgs e)
        {
            using (personal_accounting_DBEntities db = new personal_accounting_DBEntities())
            {
                var Iemp = (sender as Button).DataContext as Info_employees;
                int employee_id = Convert.ToInt32(Iemp.employee_id);
                string Iemp_id = Convert.ToString(Iemp.id_info_employee);
                int positions_id = Convert.ToInt32(Iemp.position_id);
                int departament_id = Convert.ToInt32(Iemp.departament_id);
                Employees emp = db.Employees.Where(d => d.employee_id == employee_id).FirstOrDefault();
                Positions pos = db.Positions.Where(d => d.position_id == positions_id).FirstOrDefault();
                Departaments dep = db.Departaments.Where(d => d.departament_id == departament_id).FirstOrDefault();
                string emp_id = Convert.ToString(emp.employee_id);
                string surname = Convert.ToString(emp.surname);
                string name = Convert.ToString(emp.name);
                string patronymic = Convert.ToString(emp.patronymic);
                DateTime dateDisability = Convert.ToDateTime(Iemp.dismissal_date);
                string dateDis=Convert.ToString(dateDisability.ToString("MM.dd.yyyy"));
                string MM = Convert.ToString(dateDisability.ToString("MM"));
                string DD = Convert.ToString(dateDisability.ToString("dd"));
                string YY = Convert.ToString(dateDisability.ToString("yy"));
                string departament = Convert.ToString(dep.departament_name);
                string position = Convert.ToString(pos.positions_name);

                Documents _contextdoc = new Documents
                {
                    name_doc = "T-8-Приказ-об-увольнении"
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
                var helper = new WordHelper("T-8-Приказ-об-увольнении.doc");
                var items = new Dictionary<string, string>
            {
                { "<D_id>", Convert.ToString(document.id_doc)},
                { "<emp_id>", emp_id },
                {"<Iemp_id>",Iemp_id},
                { "<Surname>",surname },
                { "<Name>",name },
                { "<Patronymic>",patronymic },
                { "<DateDis>",dateDis },
                { "<Departament>",departament },
                { "<Position>",position },
                { "M",MM },
                { "D",DD },
                { "Y",YY },
            };
                helper.Process(items);
            }
        }
    }
}
