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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        AppContext db;
        public string rights { get; set; }
        public int emp_id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public UserPage(string login, string email, string password, string rights)
        {
            db = new AppContext();
            this.login = login;
            this.password = password;
            this.email = email;
            this.rights = rights;
            InitializeComponent();
        }
        private void ButtonBackPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            var win = Application.Current.Windows;
            win[0].Close();
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string surname = surnameBox.Text.Trim();
                string name = nameBox.Text.Trim();
                string patronymic = patronymicBox.Text.Trim();
                DateTime date_of_birth = date_of_birthBox.SelectedDate.Value;
                string home_address = home_addressBox.Text.Trim();
                string marital_status = marital_statusBox.Text.Trim();
                string gender = genderBox.Text.Trim();
                int amount_children = Convert.ToInt32(amount_childrenBox.Text.Trim());
                string data_passport = data_passportBox.Text.Trim();
                string number_of_telephone = number_of_telephoneBox.Text.Trim();
                Employee employee = new Employee(surname, name, patronymic, login, email, password, rights, date_of_birth, 
                    home_address, marital_status, gender, 
                    amount_children, data_passport, number_of_telephone);
                db.Employees.Add(employee);
                db.SaveChanges();
                int emp_id = employee.employee_id;
                NavigationService.Navigate(new AnimationPage(rights, emp_id));
            }
            catch (Exception)
            {
                MessageBox.Show("Неверно введены данные");
            }
        }
    }
}
