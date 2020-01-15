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
using System.Data.Entity;
using MyClasses.Entities;

namespace Final
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        GroomingDbContext dbContext = new GroomingDbContext();
        MainWindow window;
        public LoginWindow()
        {
            InitializeComponent();

            window = new MainWindow();
            dbContext.Users.Load();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            window.Show();
            this.Owner = window;
            window.IsEnabled = false;
            this.Activate();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void LoginAccess()
        {
            string UserName = UserTextBox.Text;
            string UserPass = PasswordTextBox.Password;

            foreach(User u in dbContext.Users)
            {
                if(u.UserName == UserName)
                {
                    if(u.Password == UserPass)
                    {
                        App.Current.Properties["username"] = UserTextBox.Text;
                        App.Current.Properties["IsAdmin"] = u.IsAdmin;

                        window.UserNameLabel.Content = App.Current.Properties["username"].ToString();
                        window.Effect = null;
                        window.ListViewMenu.SelectedItem = window.ListViewHome;
                        window.IsEnabled = true;
                        window.userIsAdmin = (bool)App.Current.Properties["IsAdmin"];
                        Close();
                    }
                }
            }
            UserTextBox.Clear();
            PasswordTextBox.Clear();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            LoginAccess();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Your password has been changed to: Finbó","Password Reset");
        }
    }
}
