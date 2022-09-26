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
using System.Data.SqlClient;

namespace personal_accounting
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        AppContext db;
        public RegistrationPage()
        {
            InitializeComponent();
            db = new AppContext();
        }
        private void ButtonLoginPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string password = PassBox.Password.Trim();
            string passRep = PassBoxRep.Password.Trim();
            string rights = "User";
            if (login.Length < 5)
            {
                LoginBox.ToolTip = "Логин введен некорректно!";
                LoginBox.Background = Brushes.Red;
            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                EmailBox.ToolTip = "Email введен некорректно!";
                EmailBox.Background = Brushes.Red;
            }
            else if (password.Length < 5)
            {
                PassBox.ToolTip = "Пароль введен некорректно!";
                PassBox.Background = Brushes.Red;
            }
            else if (passRep != password)
            {
                PassBoxRep.ToolTip = "Пароль введен некорректно!";
                PassBoxRep.Background = Brushes.Red;
            }
            else
            {
                Employee emp = null;
                using (AppContext db = new AppContext())
                {
                    emp = db.Employees.Where(auth => auth.login == login).FirstOrDefault();
                }
                if (emp == null)
                {
                    LoginBox.ToolTip = default;
                    LoginBox.Background = Brushes.Transparent;
                    emp = null;
                    emp = db.Employees.Where(auth => auth.email == email).FirstOrDefault();
                    if (emp == null)
                    {

                        EmailBox.ToolTip = default;
                        EmailBox.Background = Brushes.Transparent;
                        PassBox.ToolTip = default;
                        PassBox.Background = Brushes.Transparent;
                        PassBoxRep.ToolTip = default;
                        PassBoxRep.Background = Brushes.Transparent;
                        NavigationWindow navigation = new NavigationWindow(login, email, password, rights);
                        navigation.Show();
                        Application.Current.MainWindow.Close();
                    }
                    else
                    {
                        EmailBox.ToolTip = "Этот email занят";
                        EmailBox.Background = Brushes.Red;
                    }

                }
                else
                {
                    LoginBox.ToolTip = "Этот логин занят";
                    LoginBox.Background = Brushes.Red;
                }
            }
        }
    }
}
