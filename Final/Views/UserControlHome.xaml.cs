using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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

namespace Final
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlHome : UserControl
    {

        GroomingDbContext dbContext = new GroomingDbContext();

        bool UserIsAdmin;
        public UserControlHome(bool _userIsAdmin)
        {
            UserIsAdmin = _userIsAdmin;
            InitializeComponent();
        }

        private void SwitchUserBtnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public void FirstCardBtnClick(object sender, RoutedEventArgs e)
        {
            //appointment.AddBtnClick(null, null);
        }

        private void SecondCardBtnClick(object sender, MouseButtonEventArgs e)
        {
            //window.ListViewMenu.SelectedItem = window.ListViewCustomers;

        }

        private void ThirdCardBtnClick(object sender, MouseButtonEventArgs e)
        {
            //window.ListViewMenu.SelectedItem = window.ListViewPets;

        }

        //private void FirstCardBtnClick(object sender, MouseButtonEventArgs e)
        //{

        //}
    }
}
