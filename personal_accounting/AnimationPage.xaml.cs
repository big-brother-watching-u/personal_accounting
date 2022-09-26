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
using System.Windows.Media.Animation;
using System.Threading;

namespace personal_accounting
{
    /// <summary>
    /// Логика взаимодействия для AnimationPage.xaml
    /// </summary>
    public partial class AnimationPage : Page
    {
        public string rights { get; set; }
        public int emp_id { get; set; }
        public AnimationPage(string rights, int emp_id)
        {
            InitializeComponent();
            this.rights = rights;
            this.emp_id = emp_id;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animD = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(0.5)));
            animD.From = 50;
            animD.To = 500;
            animD.Completed += new EventHandler(DAnimUp_Completed);
            tbAnim.BeginAnimation(HeightProperty, animD);
        }
        public void DAnimUp_Completed(object sender, EventArgs e)
        {
            DoubleAnimation animD = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(0.5)));
            animD.From = 500;
            animD.To = 350;
            animD.Completed += new EventHandler(DAnimDown_Completed);
            tbAnim.BeginAnimation(HeightProperty, animD);
        }
        public void DAnimDown_Completed(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            if (rights == "Admin")
            {

                NavigationService.Navigate(new NavigatePageAdmin());

            }
            else
            {
                NavigationService.Navigate(new NavigatePage());
                Info_employees Iemp = null;
                personal_accounting_DBEntities db = new personal_accounting_DBEntities();
                Iemp = db.Info_employees.Where(d => d.employee_id == emp_id).FirstOrDefault();
                if (Iemp != null)
                {
                    NavigationService.Navigate(new NavigatePage());
                }
                else
                {
                    NavigationService.Navigate(new PageNotRights());
                }
            }

        }


    }
}
