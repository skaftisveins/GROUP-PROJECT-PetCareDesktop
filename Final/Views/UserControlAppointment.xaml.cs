using MyClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Final
{
    /// <summary>
    /// Interaction logic for UserControlAppointment.xaml
    /// </summary>
    public partial class UserControlAppointment : UserControl
    {
        GroomingDbContext dbcontext = new GroomingDbContext();

        bool ShowIsListed = true;


        bool UserIsAdmin;


        #region Main function of window                
        public UserControlAppointment(bool _isAdmin)
        {
            InitializeComponent();
            UserIsAdmin = _isAdmin;
            dbcontext.People.Load();
            dbcontext.Appointments.Load();
            dbcontext.Services.Load();
            dbcontext.Pets.Load();

            AppPopulatePersonCombobox();
            AppShowUppComingAppointments(true);
            AppointmentDateCheckBox.IsChecked = false;
            ShowOrHideDateCheckBox(true);


        }

        //Load the person combobox that starts it all :)
        private void AppPopulatePersonCombobox()
        {
            try
            {
                AppPersonCombobox.DataContext = dbcontext.People.Local;
                AppPersonCombobox.SelectedIndex = -1;

                //Filterar keyrðir til að populera rétta uppl. í boxin
                AppPersonCombobox.Items.Filter = new Predicate<object>(PersonIsListed);
            }
            catch (Exception)
            {

            }
        }


        #endregion

        #region Add/Remove/Update/Delete buttons
        //Add
        public void AddBtnClick(object sender, RoutedEventArgs e)
        {
            AppNewOrUpdateLbl.Content = "a";
            SubTitleText(true, "");

            AppPopulatePersonCombobox();

            AppPersonCombobox.IsEnabled = true;

            AppNewAppointment.DataContext = new Appointment();

            AppMiddleLeftPanel.Visibility = Visibility.Visible;

            AppSave_Abort_Btn.Visibility = Visibility.Visible;
            AppListOfAppointments.Visibility = Visibility.Collapsed;
            BottomPanelButtons.Visibility = Visibility.Hidden;
            AppIsListedChkBox.Visibility = Visibility.Collapsed;
            ShowOrHideDateCheckBox(false);

            AppDatePicker.DisplayDate = DateTime.Today;
            AppDatePicker.SelectedDate = null;
            AppDatePicker.DisplayDateStart = DateTime.UtcNow;
            AppDatePicker.DisplayDateEnd = DateTime.UtcNow.AddMonths(6);
        }
        //Remove
        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SubTitleText(true, "");

                Appointment appointmentRemove = AppListOfAppointments.SelectedItem as Appointment;

                appointmentRemove.Islisted = false;

                dbcontext.SaveChanges();

                AppListOfAppointments.Visibility = Visibility.Collapsed;

                AppShowUppComingAppointments(true);


                AppListOfAppointments.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("No appointment selected");
            }


        }
        //Update
        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SubTitleText(true, "");

                AppNewOrUpdateLbl.Content = "u";
                Appointment appointmentUpdate = AppListOfAppointments.SelectedItem as Appointment;

                AppPersonCombobox.SelectedItem = appointmentUpdate.Person;
                AppPetCombobox.SelectedItem = appointmentUpdate.Pet;
                AppServicesCombobox.SelectedItem = appointmentUpdate.Service;

                AppPersonCombobox.IsEnabled = false;

                AppDetailGrid.DataContext = appointmentUpdate;

                AppMiddleLeftPanel.Visibility = Visibility.Visible;
                AppSave_Abort_Btn.Visibility = Visibility.Visible;
                AppListOfAppointments.Visibility = Visibility.Collapsed;
                BottomPanelButtons.Visibility = Visibility.Hidden;
                AppIsListedChkBox.Visibility = Visibility.Collapsed;
                AppCompletedChkBox.Visibility = Visibility.Visible;
                ShowOrHideDateCheckBox(false);


                AppDatePicker.DisplayDate = appointmentUpdate.BookedDate;
                AppDatePicker.SelectedDate = appointmentUpdate.BookedDate;
                AppDatePicker.DisplayDateStart = DateTime.Now;
                AppDatePicker.DisplayDateEnd = DateTime.Now.AddMonths(6);

            }
            catch (Exception)
            {

                MessageBox.Show("No appointment selected");
            }
        }

        //Delete
        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SubTitleText(true, "");

                Appointment delAppointment = AppListOfAppointments.SelectedItem as Appointment;

                if (delAppointment.Completed.Equals(!true))
                {
                    dbcontext.Appointments.Remove(AppListOfAppointments.SelectedItem as Appointment);

                    dbcontext.SaveChanges();
                }

                AppShowUppComingAppointments(false);

                AppListOfAppointments.SelectedIndex = 0;
                ShowOrHideDateCheckBox(false);

            }
            catch (Exception)
            {
                MessageBox.Show("No appointment selected");
            }
        }
        #endregion

        #region Filterar á combobox
        //filter til að sýna eingöngu true / false persons
        public bool PersonIsListed(Object pers)
        {
            Person person = pers as Person;
            return (person.IsListed == ShowIsListed);
        }

        //Filtera á gæludýr miðað við person combobox
        public bool PetHasOwner(Object pets)
        {
            Pet pet = pets as Pet;
            Person person = new Person();
            person = AppPersonCombobox.SelectedItem as Person;
            try
            {
                return (pet.Owner.Id == person.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Filter á services miðað við species
        public bool ServiceForSpecies(Object services)
        {
            Service service = services as Service;
            Pet pet = AppPetCombobox.SelectedItem as Pet;
            try
            {
                return (service.Species.Id == pet.Species.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Selection changed functions
        //Filterar keyrðir aftur eftir að hafa valið í einhverju boxi
        private void AppPersonCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person person = AppPersonCombobox.SelectedItem as Person;

            CollectionViewSource viewPet = new CollectionViewSource()
            {
                Source = dbcontext.Pets.Local.OrderBy(o => o.Name)
            };

            AppPetCombobox.DataContext = viewPet;
            AppPetCombobox.SelectedIndex = -1;

            try
            {
                AppOwnerTxtBox.Text = dbcontext.People.Local.FirstOrDefault(o => o == AppPersonCombobox.SelectedItem).FirstName;
                AppPetTxtBox.Text = "";
                AppSpeciesTxtBox.Text = "";
                AppServiceTxtBox.Text = "";

            }
            catch (Exception)
            {
                AppPersonCombobox.SelectedIndex = -1;
            }

            AppPetCombobox.Items.Filter = new Predicate<object>(PetHasOwner);
        }

        private void AppPetCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pet pet = AppPetCombobox.SelectedItem as Pet;

            CollectionViewSource viewService = new CollectionViewSource()
            {
                Source = dbcontext.Services.Local.OrderBy(o => o.ServiceType)
            };
            AppServicesCombobox.DataContext = viewService;
            AppServicesCombobox.SelectedIndex = -1;
            AppServiceTxtBox.Text = "";

            try
            {
                AppPetTxtBox.Text = dbcontext.Pets.Local.FirstOrDefault(o => o == AppPetCombobox.SelectedItem).Name;
                AppSpeciesTxtBox.Text = dbcontext.Pets.Local.FirstOrDefault(o => o == AppPetCombobox.SelectedItem).Species.SpeciesName;
                AppServiceTxtBox.Text = "";

            }
            catch (Exception)
            {
                AppPetCombobox.SelectedIndex = -1;

            }

            AppServicesCombobox.Items.Filter = new Predicate<object>(ServiceForSpecies);

        }

        private void AppServiceCombobos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service service = AppServicesCombobox.SelectedItem as Service;



            try
            {
                AppServiceTxtBox.Text = dbcontext.Services.Local.FirstOrDefault(o => o == AppServicesCombobox.SelectedItem).ServiceType;
            }
            catch (Exception)
            {
                AppServicesCombobox.SelectedItem = -1;
            }
        }
        #endregion

        #region Save button function
        private void App_SaveBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AppNewOrUpdateLbl.Content.Equals("u"))
                {

                    Appointment updateApp = new Appointment();
                    updateApp = AppDetailGrid.DataContext as Appointment;
                    List<Appointment> appList = dbcontext.Appointments.Where(o => o.Islisted == true).ToList();

                    if (appList.Exists(o => o.Id == updateApp.Id))
                    {

                        updateApp = appList.Find(o => o.Id == updateApp.Id) as Appointment;

                        updateApp.BookedDate = AppDatePicker.SelectedDate.Value;
                        updateApp.Completed = AppCompletedChkBox.IsChecked.Value;
                        updateApp.Comment = AppCommentTxtBox.Text;

                        AppDetailGrid.DataContext = updateApp;

                        dbcontext.SaveChanges();


                    }

                    AppSave_Abort_Btn.Visibility = Visibility.Collapsed;
                    AppMiddleLeftPanel.Visibility = Visibility.Collapsed;
                    BottomPanelButtons.Visibility = Visibility.Visible;
                    AppListOfAppointments.Visibility = Visibility.Visible;
                }
                else
                {
                    if (AppDatePicker.SelectedDate.HasValue)
                    {

                        Appointment newAppointment = new Appointment
                        {
                            Person = AppPersonCombobox.SelectedItem as Person,
                            Pet = AppPetCombobox.SelectedItem as Pet,
                            Service = AppServicesCombobox.SelectedItem as Service,
                            BookedDate = AppDatePicker.SelectedDate.Value
                        };

                        if (dbcontext.Appointments.FirstOrDefault(o => o.BookedDate == newAppointment.BookedDate
                                                                    && o.Pet.Id == newAppointment.Pet.Id) != null)
                        {
                            MessageBox.Show("This pet already has an appointment for this date!");
                        }
                        else
                        {
                            dbcontext.Appointments.Add(newAppointment);
                            dbcontext.SaveChanges();

                            MessageBox.Show($"New appointment for {newAppointment.Pet.Name} at {newAppointment.BookedDate} is confirmed");
                        }

                        AppSave_Abort_Btn.Visibility = Visibility.Collapsed;
                        AppMiddleLeftPanel.Visibility = Visibility.Collapsed;
                        BottomPanelButtons.Visibility = Visibility.Visible;
                        AppListOfAppointments.Visibility = Visibility.Visible;

                        AppShowUppComingAppointments(true);
                    }
                    else
                    {
                        MessageBox.Show("No date was selected");
                    }
                }
            }
            catch (Exception mess)
            {
                MessageBox.Show(mess.ToString());
            }


        }
        #endregion

        #region Abort Adding function
        private void App_AbortAddingAppointmentBtn(object sender, RoutedEventArgs e)
        {
            AppMiddleLeftPanel.Visibility = Visibility.Collapsed;
            AppSave_Abort_Btn.Visibility = Visibility.Collapsed;
            BottomPanelButtons.Visibility = Visibility.Visible;
            AppCompletedChkBox.Visibility = Visibility.Collapsed;
            AppListOfAppointments.Visibility = Visibility.Visible;

            ShowOrHideDateCheckBox(true);
            AppShowUppComingAppointments(true);

        }
        #endregion

        #region List box function
        private void ListBtnClick(object sender, RoutedEventArgs e)
        {
            AppointmentDateCheckBox.Visibility = Visibility.Collapsed;

            if (AppointmentListBtn.Content.Equals("Close"))
            {
                AppointmentListBtn.Content = "History";
                SubTitleText(true, "");

                AppShowOrHideActionButtons(true, true, true, true, false);

                AppIsListedChkBox.Visibility = Visibility.Collapsed;
                AppMiddleLeftPanel.Visibility = Visibility.Collapsed;

                AppListOfAppointments.Visibility = Visibility.Collapsed;

                AppShowUppComingAppointments(true);
                AppointmentDateCheckBox.Visibility = Visibility.Visible;


            }
            else
            {
                AppointmentListBtn.Content = "Close";

                AppShowOrHideActionButtons(false, false, true, false, false);

                AppIsListedChkBox.Visibility = Visibility.Visible;

                AppMiddleLeftPanel.Visibility = Visibility.Collapsed;

                AppShowUppComingAppointments(false);

            }

            AppListOfAppointments.SelectedIndex = 0;
        }
        #endregion

        #region Funtion to set sub title text on left or right side on top of window
        public void SubTitleText(bool LeftOrRight, string TextToDisplay)
        {
            if (LeftOrRight)
            {
                SubTitleLeft.Text = TextToDisplay;
            }
            else
            {
                SubTitleRight.Text = TextToDisplay;
            };

        }
        #endregion

        #region Checking to see if listed
        //Checking to see if to show listed or unlisted
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            if (SubTitleLeft.Text.Equals("Showing upcoming appointments"))
            {
                AppShowUppComingAppointments(true);

                if (AppIsListedChkBox.IsChecked == true) { AppointmentDeleteBtn.Visibility = Visibility.Visible; }
            }
            else
            {
                AppointmentDeleteBtn.Visibility = Visibility.Collapsed;
                AppShowUppComingAppointments(false);
            }
        }
        #endregion

        #region Show or hide the comment box for completed appointments
        //Showing or hiding the comment box that is available when completing the appointment
        private void AppCompletedChkBox_Click(object sender, RoutedEventArgs e)
        {
            if (AppCompletedChkBox.IsChecked == true)
            {
                AppCommentLbl.Visibility = Visibility.Visible;
                AppCommentTxtBox.Visibility = Visibility.Visible;
            }
            else
            {
                AppCommentLbl.Visibility = Visibility.Collapsed;
                AppCommentTxtBox.Visibility = Visibility.Collapsed;
                AppCommentTxtBox.Text = "";
            }
        }
        #endregion

        #region Which datacontext to load into App..list
        //Deciding which dataconext to load for Appointmentlist


        private void AppShowUppComingAppointments(bool UpcomingOrHistory)
        {

            if (UpcomingOrHistory)
            {
                SubTitleText(true, "Showing upcoming appointments");
                AppIsListedChkBox.IsEnabled = false;
                AppIsListedChkBox.IsChecked = true;
                AppShowOrHideActionButtons(true, true, true, true, false);

                if (AppointmentDateCheckBox.IsChecked == true)
                {

                    AppListOfAppointments.DataContext = dbcontext.Appointments.Local.Where(o => o.Islisted == true)
                                                             .Where(o => o.Completed == false)
                                                             .OrderBy(o => o.BookedDate)
                                                             .Where(o => o.BookedDate <= DateTime.UtcNow.Date.AddDays(7));
                    SubTitleText(true, "Showing upcoming 7 days of appointments");

                }

                else if (AppointmentDateCheckBox.IsChecked != true)
                {
                    AppListOfAppointments.DataContext = dbcontext.Appointments.Local.Where(o => o.Islisted == true)
                                                            .Where(o => o.Completed == false)
                                                            .OrderBy(o => o.BookedDate);
                    SubTitleText(true, "Showing upcoming appointments");

                }

                ShowOrHideDateCheckBox(true);
            }

            else
            {
                bool b = (bool)AppIsListedChkBox.IsChecked;

                SubTitleText(true, "Showing history of completed appointments");

                AppIsListedChkBox.IsEnabled = true;

                AppListOfAppointments.DataContext = dbcontext.Appointments.Local.Where(o => o.Completed == b).OrderByDescending(o => o.BookedDate);

                if (UserIsAdmin)
                {
                    AppointmentUpdateBtn.Visibility = Visibility.Visible;
                }

                if (!b)
                {
                    SubTitleText(true, "Showing history of uncompleted/cancelled appointments");

                    if (UserIsAdmin)
                    {
                        AppointmentDeleteBtn.Visibility = Visibility.Visible; AppointmentUpdateBtn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            AppListOfAppointments.Visibility = Visibility.Visible;
        }
        #endregion

        #region Collapse or show action buttons
        //Collapsing or showing action buttons
        private void AppShowOrHideActionButtons(bool showAdd, bool showRem, bool showList, bool showUpdate, bool showDel)
        {
            if (showAdd)
            {
                AppointmentAddBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AppointmentAddBtn.Visibility = Visibility.Collapsed;

            }

            if (showRem)
            {
                AppointmentRemoveBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AppointmentRemoveBtn.Visibility = Visibility.Collapsed;

            }

            if (showList)
            {
                AppointmentListBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AppointmentListBtn.Visibility = Visibility.Collapsed;

            }

            if (showUpdate)
            {
                AppointmentUpdateBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AppointmentUpdateBtn.Visibility = Visibility.Collapsed;

            }

            if (showDel)
            {
                AppointmentDeleteBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AppointmentDeleteBtn.Visibility = Visibility.Collapsed;

            }

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

        //Checkbox to show only appointments for the next 7 days.
        private void AppointmentDateCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            AppListOfAppointments.DataContext = dbcontext.Appointments.Local.Where(o => o.Islisted == true)
                                                             .Where(o => o.Completed == false)
                                                             .OrderBy(o => o.BookedDate)
                                                             .Where(o => o.BookedDate <= DateTime.UtcNow.Date.AddDays(7));
            AppListOfAppointments.Items.Refresh();
            AppListOfAppointments.SelectedIndex = 0;

            SubTitleText(true, "Showing upcoming 7 days of appointments");
        }

        //Checkbox to show all active appointments
        private void AppointmentDateCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            AppListOfAppointments.DataContext = dbcontext.Appointments.Local.Where(o => o.Islisted == true)
                                                         .Where(o => o.Completed == false)
                                                         .OrderBy(o => o.BookedDate);

            AppListOfAppointments.Items.Refresh();
            AppListOfAppointments.SelectedIndex = 0;
            SubTitleText(true, "Showing upcoming appointments");


        }

        //Show or hide the date checkbox
        private void ShowOrHideDateCheckBox(bool ShowOrHide)
        {
            AppointmentDateCheckBox.Visibility = Visibility.Visible;

            if (!ShowOrHide)
            {
                AppointmentDateCheckBox.Visibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}