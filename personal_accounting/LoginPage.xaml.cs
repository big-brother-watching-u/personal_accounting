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
using System.Data;
using System.Data.Entity;

namespace personal_accounting
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void ButtonRegistrationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void LoginButton_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string pass = PassBox.Password.Trim();
            if (login.Length < 5)
            {
                LoginBox.ToolTip = "Логин введен некорректно!";
                LoginBox.Background = Brushes.Red;
            }
            else if (pass.Length < 5)
            {
                PassBox.ToolTip = "Пароль введен некорректно!";
                PassBox.Background = Brushes.Red;
            }
            else
            {
                LoginBox.ToolTip = default;
                LoginBox.Background = Brushes.Transparent;
                PassBox.ToolTip = default;
                PassBox.Background = Brushes.Transparent;
                Employee emp = null;
                using (AppContext db = new AppContext())
                {
                    emp = db.Employees.Where(auth => auth.login == login).FirstOrDefault();
                }
                if (emp != null)
                {
                    LoginBox.ToolTip = default;
                    LoginBox.Background = Brushes.Transparent;
                    emp = null;
                    using (AppContext db = new AppContext())
                    {
                        emp = db.Employees.Where(auth => auth.login == login && auth.password == pass).FirstOrDefault();
                    }
                    if (emp != null)
                    {
                        PassBox.ToolTip = default;
                        PassBox.Background = Brushes.Transparent;
                        using (AppContext db = new AppContext())
                        {
                            emp = db.Employees.FirstOrDefault(auth => auth.login == login);
                        }
                        string rights = emp.rights;
                        int emp_id = emp.employee_id;

                        NavigationWindow navigation = new NavigationWindow(rights, emp_id);
                        Application.Current.MainWindow.Close();
                        navigation.Show();
                    }
                    else
                    {
                        PassBox.ToolTip = "Неверный пароль!";
                        PassBox.Background = Brushes.Red;
                    }
                }
                else
                {
                    LoginBox.ToolTip = "Пользователь с таким именем отсутствует!";
                    LoginBox.Background = Brushes.Red;
                }
            }
        }
    }
}
