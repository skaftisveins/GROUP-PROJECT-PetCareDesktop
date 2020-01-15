using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControlHome homeControl;
        UserControlAppointment appointmentControl;
        UserControlCustomers customersControl;
        UserControlPets petsControl;
        UserControlInfograph infographControl;
        UserControlControls controlControl;

        public bool userIsAdmin;

        public MainWindow()
        {
            InitializeComponent();
            StartupBlurEffect();
            
            
        }

        private void StartupBlurEffect()
        {
            BlurEffect blur = new BlurEffect();
            Brush current = Background;
            blur.Radius = 5;
            Background = new SolidColorBrush(Colors.DarkGray);
            Effect = blur;
            Background = current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["username"] = "";
        }

        #region ListView Menu
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            GridSelectedItem(index);

            switch (index)
            {
                case 0:
                    GridUsrCtrl.Children.Clear();
                    GridUsrCtrl.Children.Add(homeControl = new UserControlHome(userIsAdmin));
                    break;
                //case 1:
                //    GridUsrCtrl.Children.Clear();
                //    GridUsrCtrl.Children.Add(new UserControlService());
                //    break;
                case 1:
                    GridUsrCtrl.Children.Clear();
                    GridUsrCtrl.Children.Add(appointmentControl = new UserControlAppointment(userIsAdmin));
                    break;
                case 2:
                    GridUsrCtrl.Children.Clear();
                    GridUsrCtrl.Children.Add(customersControl = new UserControlCustomers(userIsAdmin));
                    break;
                case 3:
                    GridUsrCtrl.Children.Clear();
                    GridUsrCtrl.Children.Add(petsControl = new UserControlPets(userIsAdmin));
                    break;
                case 4:
                    GridUsrCtrl.Children.Clear();
                    GridUsrCtrl.Children.Add(infographControl = new UserControlInfograph(userIsAdmin));
                    break;

                case 5:
                    GridUsrCtrl.Children.Clear();
                    GridUsrCtrl.Children.Add(controlControl = new UserControlControls(userIsAdmin));
                    break;
                case 6:
                    Application.Current.Shutdown();
                    break;

                default:
                    GridUsrCtrl.Children.Add(homeControl = new UserControlHome(userIsAdmin));
                    break;
            }
        }

        private void GridSelectedItem(int index)
        {
            TransitioningContent.OnApplyTemplate();
            LeftGridSelectedItem.Margin = new Thickness(0, (80 + (60 * index)), 0, 0);
        }

        public void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion

        private void ListViewMenu_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //e.Handled = true;
        }
    }
}
