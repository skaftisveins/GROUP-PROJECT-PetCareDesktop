using MyClasses.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Final
{
    /// <summary>
    /// Interaction logic for UserControlSettings.xaml
    /// </summary>
    public partial class UserControlControls : UserControl
    {
        GroomingDbContext dbContext = new GroomingDbContext();
        bool UserIsAdmin;

        //Main function
        public UserControlControls(bool _userIsAdmin)
        {
            UserIsAdmin = _userIsAdmin;
            InitializeComponent();

            dbContext.Services.Load();
            dbContext.Species.Load();
            dbContext.Breeds.Load();
            dbContext.Users.Load();

            ControlDetailServices.Visibility = Visibility.Collapsed;
            ControlsDeleteBtn.Visibility = Visibility.Collapsed;

            ShowBottomPanelIfAdmin();
        }

        #region Service Group Button Click functions

        //Species
        private void SpeciesBtnClick(object sender, RoutedEventArgs e)
        {

            CollectionViewSource viewService = new CollectionViewSource()
            {
                Source = dbContext.Species.Local.Where(i => i.IsListed == ServiceIsListedChkBox.IsChecked).OrderBy(o => o.SpeciesName)
            };
            ControlsDataGrid_Species.DataContext = viewService;
            ControlsDataGrid_Species.SelectedIndex = 0;

            ControlDetailServices.Visibility = Visibility.Collapsed;
            ControlDetailBreed.Visibility = Visibility.Collapsed;
            ControlDetailUsers.Visibility = Visibility.Collapsed;
            ControlDetailSpecies.Visibility = Visibility.Visible;


            ControlsDataGrid_Services.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Species.Visibility = Visibility.Visible;
            ControlsDataGrid_Breed.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Users.Visibility = Visibility.Collapsed;

        }

        //Users
        private void UsersBtnClick(object sender, RoutedEventArgs e)
        {

            CollectionViewSource viewService = new CollectionViewSource()
            {
                Source = dbContext.Users.Local.Where(i => i.IsListed == ServiceIsListedChkBox.IsChecked).OrderBy(o => o.UserName)
            };
            ControlsDataGrid_Users.DataContext = viewService;
            ControlsDataGrid_Users.SelectedIndex = 0;

            ControlDetailServices.Visibility = Visibility.Collapsed;
            ControlDetailBreed.Visibility = Visibility.Collapsed;
            ControlDetailUsers.Visibility = Visibility.Visible;
            ControlDetailSpecies.Visibility = Visibility.Collapsed;

            ControlsDataGrid_Services.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Species.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Breed.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Users.Visibility = Visibility.Visible;
        }

        //Services
        private void ServicesBtnClick(object sender, RoutedEventArgs e)
        {

            CollectionViewSource viewService = new CollectionViewSource()
            {
                Source = dbContext.Services.Local.Where(i => i.IsListed == ServiceIsListedChkBox.IsChecked).OrderBy(o => o.ServiceType)
            };
            ControlsDataGrid_Services.DataContext = viewService;
            ControlsDataGrid_Services.SelectedIndex = 0;

            ControlDetailServices.Visibility = Visibility.Visible;
            ControlDetailBreed.Visibility = Visibility.Collapsed;
            ControlDetailUsers.Visibility = Visibility.Collapsed;
            ControlDetailSpecies.Visibility = Visibility.Collapsed;


            ControlsDataGrid_Services.Visibility = Visibility.Visible;
            ControlsDataGrid_Species.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Breed.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Users.Visibility = Visibility.Collapsed;
        }

        //Breed
        private void BreedBtnClick(object sender, RoutedEventArgs e)
        {

            CollectionViewSource viewService = new CollectionViewSource()
            {
                Source = dbContext.Breeds.Local.Where(i => i.IsListed == ServiceIsListedChkBox.IsChecked)
                                             .OrderBy(o => o.BreedName).OrderBy(p => p.Species.SpeciesName)
            };
            ControlsDataGrid_Breed.DataContext = viewService;
            ControlsDataGrid_Breed.SelectedIndex = 0;

            ControlDetailBreed.Visibility = Visibility.Visible;
            ControlDetailServices.Visibility = Visibility.Collapsed;
            ControlDetailUsers.Visibility = Visibility.Collapsed;
            ControlDetailSpecies.Visibility = Visibility.Collapsed;

            ControlsDataGrid_Services.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Species.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Breed.Visibility = Visibility.Visible;
            ControlsDataGrid_Users.Visibility = Visibility.Collapsed;

        }

        #endregion

        #region Save and Abort Button functions
        private void Controls_SaveBtn(object sender, RoutedEventArgs e)
        {
            while (true)
            {

                //Save function determains which entity to save based on which
                //control detail view is visible. It then saves either a new item
                //or updates the currently selected item in the datagrid if it is
                //the same as the one in the detail view.

                //Breed
                if (ControlDetailBreed.Visibility == Visibility.Visible)
                {
                    DataGrid dg = ControlsDataGrid_Breed as DataGrid;

                    if (dg.SelectedItem != ControlDetailBreed.DataContext)
                    {
                        Breed newBreed = ControlDetailBreed.DataContext as Breed;
                        newBreed.Species = ServicesBreedCmbBox.SelectedItem as Species;

                        if (String.IsNullOrEmpty(newBreed.BreedName) ||
                            String.IsNullOrWhiteSpace(newBreed.BreedName) ||
                            newBreed.Species == null)
                        {
                            MessageBox.Show("Both Breed name and Species must have a value");
                            break;
                        }

                        dbContext.Breeds.Add(newBreed);
                        ControlsDataGrid_Breed.IsEnabled = true;

                    }
                    else
                    {
                        dg.SelectedItem = ControlDetailBreed.DataContext as Breed;
                    }

                    ControlDetailBreed.IsEnabled = false;
                }

                //Species
                else if (ControlDetailSpecies.Visibility == Visibility.Visible)
                {
                    DataGrid dg = ControlsDataGrid_Species as DataGrid;

                    if (dg.SelectedItem != ControlDetailSpecies.DataContext)
                    {
                        Species newSpecies = ControlDetailSpecies.DataContext as Species;

                        if (String.IsNullOrEmpty(newSpecies.SpeciesName) ||
                            String.IsNullOrWhiteSpace(newSpecies.SpeciesName))
                        {
                            MessageBox.Show("Species name must have a value");
                            break;
                        }

                        dbContext.Species.Add(newSpecies);
                        ControlsDataGrid_Species.IsEnabled = true;
                    }
                    else
                    {
                        dg.SelectedItem = ControlDetailSpecies.DataContext as Species;
                    }

                    ControlDetailSpecies.IsEnabled = false;
                }

                //Users
                else if (ControlDetailUsers.Visibility == Visibility.Visible)
                {
                    DataGrid dg = ControlsDataGrid_Users as DataGrid;

                    if (dg.SelectedItem != ControlDetailUsers.DataContext)
                    {
                        User newUser = ControlDetailUsers.DataContext as User;

                        newUser.Password = PasswordTxtBox.Password;

                        if (String.IsNullOrEmpty(newUser.UserName) ||
                            String.IsNullOrWhiteSpace(newUser.UserName) ||
                            String.IsNullOrEmpty(newUser.Password) ||
                            String.IsNullOrWhiteSpace(newUser.Password))

                        {
                            MessageBox.Show("Both User name and password must have a value");
                            break;
                        }

                        dbContext.Users.Add(newUser);
                        ControlsDataGrid_Users.IsEnabled = true;
                    }
                    else
                    {
                        dg.SelectedItem = ControlDetailUsers.DataContext as User;
                    }

                    ControlDetailUsers.IsEnabled = false;
                    UsersBtnClick(null, null);
                }

                //Services
                else if (ControlDetailServices.Visibility == Visibility.Visible)
                {
                    DataGrid dg = ControlsDataGrid_Services as DataGrid;

                    if (dg.SelectedItem != ControlDetailServices.DataContext)
                    {
                        Service newService = ControlDetailServices.DataContext as Service;
                        newService.Species = ServicesSpeciesCmbBox.SelectedItem as Species;

                        if (String.IsNullOrEmpty(newService.ServiceType) ||
                            String.IsNullOrWhiteSpace(newService.ServiceType) ||
                            newService.Species == null)
                        {
                            MessageBox.Show("Both Service Type and Species must have a value");
                            break;
                        }

                        dbContext.Services.Add(newService);
                        ControlsDataGrid_Services.IsEnabled = true;
                    }
                    else
                    {
                        dg.SelectedItem = ControlDetailServices.DataContext as Service;
                    }

                    ControlDetailServices.IsEnabled = false;
                }

                dbContext.SaveChanges();

                ShowBottomPanelIfAdmin();
                //BottomPanelButtons.Visibility = Visibility.Visible;
                SaveAbortButtons.Visibility = Visibility.Collapsed;
                ControlSidePanelBtn.IsEnabled = true;
                ControlsDataGrid_Services.Visibility = Visibility.Visible;
                ControlsMiddleRightMainGrid.IsEnabled = true;
                break;
            }


        }

        private void Controls_AbortSaveBtn(object sender, RoutedEventArgs e)
        {
            //Breed
            if (ControlDetailBreed.Visibility == Visibility.Visible)
            {
                ControlsDataGrid_Breed.IsEnabled = true;
                ControlDetailBreed.IsEnabled = false;
                ControlsDataGrid_Breed.SelectedIndex = 0;
                ControlDetailBreed.DataContext = ControlsDataGrid_Breed.SelectedItem;
            }
            //Species
            else if (ControlDetailSpecies.Visibility == Visibility.Visible)
            {
                ControlsDataGrid_Species.IsEnabled = true;
                ControlDetailSpecies.IsEnabled = false;
                ControlsDataGrid_Species.SelectedIndex = 0;
                ControlDetailSpecies.DataContext = ControlsDataGrid_Species.SelectedItem;
            }
            //Users
            else if (ControlDetailUsers.Visibility == Visibility.Visible)
            {
                ControlsDataGrid_Users.IsEnabled = true;
                ControlDetailUsers.IsEnabled = false;
                ControlsDataGrid_Users.SelectedIndex = 0;
                ControlDetailUsers.DataContext = ControlsDataGrid_Users.SelectedItem;
            }
            //Services
            else if (ControlDetailServices.Visibility == Visibility.Visible)
            {
                ControlsDataGrid_Services.IsEnabled = true;
                ControlDetailServices.IsEnabled = false;
                ControlsDataGrid_Services.SelectedIndex = 0;
                ControlDetailServices.DataContext = ControlsDataGrid_Services.SelectedItem;
            }

            ShowBottomPanelIfAdmin();
            //BottomPanelButtons.Visibility = Visibility.Visible;
            SaveAbortButtons.Visibility = Visibility.Collapsed;
            ControlsDataGrid_Services.Visibility = Visibility.Visible;

            ControlSidePanelBtn.IsEnabled = true;
            ControlsMiddleRightMainGrid.IsEnabled = true;


        }
        #endregion

        #region Add/Remove/Update/Delete Button functions
        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            ControlSidePanelBtn.IsEnabled = false;
            ShowBottomPanelIfAdmin();
            BottomPanelButtons.Visibility = Visibility.Collapsed;
            SaveAbortButtons.Visibility = Visibility.Visible;

            if (ControlsDataGrid_Breed.Visibility == Visibility.Visible)
            {
                AddBreed();
            }
            else if (ControlsDataGrid_Species.Visibility == Visibility.Visible)
            {
                AddSpecies();
            }
            else if (ControlsDataGrid_Users.Visibility == Visibility.Visible)
            {
                AddUsers();
            }
            else if (ControlsDataGrid_Services.Visibility == Visibility.Visible)
            {
                AddServices();
            }
        }

        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            ControlSidePanelBtn.IsEnabled = false;


            if (ControlsDataGrid_Breed.Visibility == Visibility.Visible)
            {
                DeleteBreed();
            }
            else if (ControlsDataGrid_Species.Visibility == Visibility.Visible)
            {
                DeleteSpecies();
            }
            else if (ControlsDataGrid_Users.Visibility == Visibility.Visible)
            {
                DeleteUsers();
            }
            else if (ControlsDataGrid_Services.Visibility == Visibility.Visible)
            {
                DeleteServices();
            }
        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            //ControlSidePanelBtn.IsEnabled = false;

            if (ControlsDataGrid_Breed.Visibility == Visibility.Visible)
            {
                RemoveBreed();
            }
            else if (ControlsDataGrid_Species.Visibility == Visibility.Visible)
            {
                RemoveSpecies();
            }
            else if (ControlsDataGrid_Users.Visibility == Visibility.Visible)
            {
                RemoveUsers();
            }
            else if (ControlsDataGrid_Services.Visibility == Visibility.Visible)
            {
                RemoveServices();
            }

        }

        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            while (true)
            {

                if (ControlsDataGrid_Breed.Visibility == Visibility.Visible)
                {
                    if (ServiceIsListedChkBox.IsChecked.Equals(false))
                    {
                        if (ControlsDataGrid_Breed.HasItems.Equals(false))
                        {
                            MessageBox.Show("Nothing to update!");
                            break;
                        }
                    }

                    UpdateBreed();
                }
                else if (ControlsDataGrid_Species.Visibility == Visibility.Visible)
                {
                    UpdateSpecies();
                }
                else if (ControlsDataGrid_Users.Visibility == Visibility.Visible)
                {
                    UpdateUsers();
                }
                else if (ControlsDataGrid_Services.Visibility == Visibility.Visible)
                {
                    UpdateServices();
                }

                ControlSidePanelBtn.IsEnabled = false;
                ControlsMiddleRightMainGrid.IsEnabled = false;

                ShowBottomPanelIfAdmin();
                BottomPanelButtons.Visibility = Visibility.Collapsed;
                SaveAbortButtons.Visibility = Visibility.Visible;

                break;
            }
        }
        #endregion

        #region Add functions for control items
        //Services
        private void AddServices()
        {
            ControlsDataGrid_Services.IsEnabled = false;
            Service service = new Service();

            ControlDetailServices.DataContext = service;

            ServicesSpeciesCmbBox.DataContext = dbContext.Species.Local;

            ControlsDataGrid_Services.Visibility = Visibility.Collapsed;

            ServicesHomeCareChkBox.Content = "No";

            ControlDetailServices.IsEnabled = true;

        }

        //Users
        private void AddUsers()
        {
            ControlsDataGrid_Users.IsEnabled = false;
            User newUser = new User();

            PasswordTxtBox.Password = "";

            ControlDetailUsers.DataContext = newUser;

            ControlDetailUsers.IsEnabled = true;

            PasswordResetBtn.IsEnabled = false;
        }

        //Species
        private void AddSpecies()
        {
            ControlsDataGrid_Species.IsEnabled = false;
            Species newSpecies = new Species();

            ControlDetailSpecies.DataContext = newSpecies;

            ControlDetailSpecies.IsEnabled = true;
        }

        //Breed
        private void AddBreed()
        {
            ControlsDataGrid_Breed.IsEnabled = false;
            Breed newBreed = new Breed();

            ControlDetailBreed.DataContext = newBreed;

            ServicesBreedCmbBox.DataContext = dbContext.Species.Local;

            ControlDetailBreed.IsEnabled = true;
        }
        #endregion

        #region Remove/IsListed functions for control items
        //Breed
        private void RemoveBreed()
        {
            try
            {
                DataGrid dg = ControlsDataGrid_Breed as DataGrid;

                Breed breed = dg.SelectedItem as Breed;

                if (breed.IsListed)
                {
                    breed.IsListed = false;

                    dbContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Breed already removed, try Delete instead.");
                }
                BreedBtnClick(null, null);

            }
            catch (Exception)
            {

                MessageBox.Show("No breed selected");
            }
        }

        //Species
        private void RemoveSpecies()
        {
            try
            {
                DataGrid dg = ControlsDataGrid_Species as DataGrid;

                Species species = dg.SelectedItem as Species;

                if (species.IsListed)
                {
                    species.IsListed = false;

                    dbContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Species already removed, try Delete instead.");
                }
                SpeciesBtnClick(null, null);

            }
            catch (Exception)
            {

                MessageBox.Show("No species selected");
            }
        }

        //Users
        private void RemoveUsers()
        {
            try
            {
                DataGrid dg = ControlsDataGrid_Users as DataGrid;

                User user = dg.SelectedItem as User;

                if (user.IsListed)
                {
                    user.IsListed = false;

                    dbContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("User already removed, try Delete instead.");
                }
                UsersBtnClick(null, null);

            }
            catch (Exception)
            {

                MessageBox.Show("No user selected");
            }
        }

        //Services
        private void RemoveServices()
        {
            try
            {
                DataGrid dg = ControlsDataGrid_Services as DataGrid;

                Service service = dg.SelectedItem as Service;

                if (service.IsListed)
                {
                    service.IsListed = false;

                    dbContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Service already removed, try Delete instead.");
                }
                ServicesBtnClick(null, null);

            }
            catch (Exception)
            {

                MessageBox.Show("No service selected");
            }
        }
        #endregion

        #region Update functions for control items


        private void UpdateServices()
        {
            ControlDetailServices.IsEnabled = true;
        }

        private void UpdateBreed()
        {
            ControlDetailBreed.IsEnabled = true;
        }

        private void UpdateSpecies()
        {
            ControlDetailSpecies.IsEnabled = true;
        }

        private void UpdateUsers()
        {
            ControlDetailUsers.IsEnabled = true;
        }
        #endregion

        #region Delete functions for control items
        private void DeleteBreed()
        {
            MessageBox.Show("This operation is not possible at the moment");

        }

        private void DeleteSpecies()
        {
            MessageBox.Show("This operation is not possible at the moment");

        }

        private void DeleteUsers()
        {
            MessageBox.Show("This operation is not possible at the moment");

        }

        private void DeleteServices()
        {
            MessageBox.Show("This operation is not possible at the moment");
        }
        #endregion

        #region Selection Changes in DataGrid functions

        //Services
        private void ControlsDataGrid_Services_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (GroomingDbContext db = new GroomingDbContext())
            {
                db.Species.Load();

                DataGrid dg = sender as DataGrid;


                if (dg.SelectedItem is Service service)
                {

                    ControlDetailServices.DataContext = service;
                    if (service.Species != null)
                    {
                        ServicesSpeciesCmbBox.DataContext = db.Species.Local.Where(o => o.Id == service.Species.Id);
                        ServicesSpeciesCmbBox.SelectedIndex = 0;
                    }


                    if (service.HomeCare)
                    {
                        ServicesHomeCareChkBox.Content = "Yes";
                    }
                    else
                    {
                        ServicesHomeCareChkBox.Content = "No";
                    }
                }
            }
        }

        //Breed
        private void ControlsDataGrid_Breed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            Breed breed = new Breed();
            breed = dg.SelectedItem as Breed;

            if (breed != null)
            {
                ControlDetailBreed.DataContext = breed;

                //Combobox does not seem to want to capture the binding from the total context.
                //As a workaround I have explicitly told it what to show and where to get it
                ServicesBreedCmbBox.DataContext = dbContext.Species.Local.Where(o => o.Id == breed.Species.Id);
                //Default behaviour is to show index -1 aka "nothing", in this case index 0 is the item you have fetched.
                ServicesBreedCmbBox.SelectedIndex = 0;
            }
        }

        //Users
        private void ControlsDataGrid_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedItem is User selectedUser)
            {
                ControlDetailUsers.DataContext = selectedUser;

                PasswordTxtBox.Password = "xxxxxxxx";
            }
        }

        //Species
        private void ControlsDataGrid_Species_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            Species selectedSpecies = dg.SelectedItem as Species;

            ControlDetailSpecies.DataContext = selectedSpecies;
        }
        #endregion

        #region IsListed show/hide funtion for all controls

        private void ServiceIsListedChkBox_Click(object sender, RoutedEventArgs e)
        {
            sender.GetType();

            if (ServiceIsListedChkBox.IsChecked == true)
            {
                ServiceIsListedChkBox.Foreground = new SolidColorBrush(Colors.Black);
                ServiceIsListedChkBox.Content = "Showing Listed";
                ShowBottomPanelIfAdmin();
                BottomPanelButtons.IsEnabled = true;
                ServicesRelistChkBox.Visibility = Visibility.Hidden;

                ServicesBreedRelistChkBox.Visibility = Visibility.Hidden;
                ServicesSpeciesRelistChkBox.Visibility = Visibility.Hidden;
                ServicesUsersRelistChkBox.Visibility = Visibility.Hidden;
                RelistFunctionVistibilityChange(false);


            }
            else
            {
                ServiceIsListedChkBox.Foreground = new SolidColorBrush(Colors.Red);
                ServiceIsListedChkBox.Content = "Showing Unlisted";
                //BottomPanelButtons.IsEnabled = false;

                ServicesRelistChkBox.Visibility = Visibility.Visible;
                ServicesBreedRelistChkBox.Visibility = Visibility.Visible;
                ServicesSpeciesRelistChkBox.Visibility = Visibility.Visible;
                ServicesUsersRelistChkBox.Visibility = Visibility.Visible;
                RelistFunctionVistibilityChange(true);

            }

            if (ControlsDataGrid_Breed.Visibility == Visibility.Visible)
            {
                BreedBtnClick(null, e);
            }
            else if (ControlsDataGrid_Species.Visibility == Visibility.Visible)
            {
                SpeciesBtnClick(sender, e);
            }
            else if (ControlsDataGrid_Users.Visibility == Visibility.Visible)
            {
                UsersBtnClick(sender, e);
            }
            else if (ControlsDataGrid_Services.Visibility == Visibility.Visible)
            {
                ServicesBtnClick(sender, e);
            }

        }

        #endregion

        #region Password reset for users
        //Users password reset to 'petcare'
        private void ResetUserPassWordBtn(object sender, RoutedEventArgs e)
        {
            DataGrid dg = ControlsDataGrid_Users as DataGrid;
            User resetUser = new User();
            resetUser = dg.SelectedItem as User;

            string messageReset = $"Password for user: {resetUser.UserName}" +
                                    Environment.NewLine + "has been reset to: petcare" + Environment.NewLine + Environment.NewLine +
                                    "Please do NOT forget to change it";

            resetUser.Password = "petcare";

            MessageBox.Show(messageReset, "Password Change", MessageBoxButton.OK, MessageBoxImage.Information);

            dbContext.SaveChanges();
        }

        #endregion

        #region Miscellaneous functions

        //Check if user is admin to show buttons
        private void ShowBottomPanelIfAdmin()
        {
            BottomPanelButtons.Visibility = Visibility.Hidden;

            if (UserIsAdmin)
            {
                BottomPanelButtons.Visibility = Visibility.Visible;
            }
        }

        //Homecare option check for a servicetype
        private void ServicesHomeCareChkBox_Click(object sender, RoutedEventArgs e)
        {

            if (ServicesHomeCareChkBox.IsChecked == true)
            {
                ServicesHomeCareChkBox.Content = "Yes";
            }
            else
            {
                ServicesHomeCareChkBox.Content = "No";
            }
        }

        //Visibility change if re-listing of an item is needed
        private void RelistFunctionVistibilityChange(bool EnableUpdate)
        {
            ControlsReListBtn.Visibility = Visibility.Visible;
            ControlsUpdateBtn.IsEnabled = false;
            ControlsAddBtn.IsEnabled = false;
            ControlsRemoveBtn.IsEnabled = false;

            if (!EnableUpdate)
            {
                ControlsReListBtn.Visibility = Visibility.Collapsed;
                ControlsUpdateBtn.IsEnabled = true;
                ControlsAddBtn.IsEnabled = true;
                ControlsRemoveBtn.IsEnabled = true;

            }
        }

        //Re-listing of items
        private void ReListBtnClick(object sender, RoutedEventArgs e)
        {

            if (ControlsDataGrid_Breed.Visibility == Visibility.Visible)
            {
                ServicesBreedRelistChkBox.IsChecked = true;
                dbContext.SaveChanges();
                BreedBtnClick(null, null);
            }
            else if (ControlsDataGrid_Species.Visibility == Visibility.Visible)
            {
                ServicesSpeciesRelistChkBox.IsChecked = true;
                dbContext.SaveChanges();
                SpeciesBtnClick(null, null);
            }

            else if (ControlsDataGrid_Users.Visibility == Visibility.Visible)
            {
                ServicesUsersRelistChkBox.IsChecked = true;
                dbContext.SaveChanges();
                UsersBtnClick(null, null);
            }
            else if (ControlsDataGrid_Services.Visibility == Visibility.Visible)
            {
                ServicesRelistChkBox.IsChecked = true;
                dbContext.SaveChanges();
                ServicesBtnClick(null, null);
            }


        }

        #endregion

        //Test of formatting at the moment input is open
        private void TextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.IsEnabled == true)
            {
                textBox.BorderThickness = new Thickness(1, 1, 1, 1);
            }
            else
            {
                textBox.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }


    }
}
