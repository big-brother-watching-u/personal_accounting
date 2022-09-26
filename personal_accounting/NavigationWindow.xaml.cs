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
using System.Windows.Shapes;

namespace personal_accounting
{
    /// <summary>
    /// Логика взаимодействия для NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        public NavigationWindow(string rights, int emp_id)
        {
            InitializeComponent();
            NavigationFrame.Content = new AnimationPage(rights, emp_id);
        }
        public NavigationWindow(string login, string email, string password, string rights)
        {
            InitializeComponent();
            NavigationFrame.Content = new UserPage(login, email, password, rights);

        }
    }
}
