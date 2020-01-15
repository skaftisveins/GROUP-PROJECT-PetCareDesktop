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
using System.Data.Entity;
using MyClasses.Entities;
using Caliburn.Micro;

namespace Final
{
    /// <summary>
    /// Interaction logic for UserControlCustomers.xaml
    /// </summary>
    public partial class UserControlCustomers : UserControl
    {
        GroomingDbContext dbcontext = new GroomingDbContext();

        bool ShowIsListed = true;
        bool UserIsAdmin;

        public UserControlCustomers(bool _userIsAdmin)
        {
            UserIsAdmin = _userIsAdmin;
            InitializeComponent();

            dbcontext.People.Load();

            PersonSelectionComboBox.Items.Filter = new Predicate<object>(PersonIsListed);

            CollectionViewSource view = new CollectionViewSource();
            view.Source = dbcontext.People.Local;
            DataContext = view;

            SaveNewPersonBtn.Visibility = Visibility.Collapsed;
            AbortAddingPersonBtn.Visibility = Visibility.Collapsed;
            UpdatePersonBtn.Visibility = Visibility.Collapsed;
            AbortUpdatePersonBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PersonDeleteBtn.Visibility = Visibility.Collapsed;
            PersonRelistBtn.Visibility = Visibility.Collapsed;

            PersonDataGrids.IsEnabled = false;
        }

        //filter til að sýna eingöngu true / false persons
        // TODO: Gera checkbox sem breytir ShowIsListed í true or false
        public bool PersonIsListed(Object pers)
        {
            Person person = pers as Person;
            return (person.IsListed == ShowIsListed);
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            PersonDataGrids.DataContext = new Person();
            SaveNewPersonBtn.Visibility = Visibility.Visible;
            AbortAddingPersonBtn.Visibility = Visibility.Visible;
            PersonDataGrids.IsEnabled = true;
            PersonSelectionComboBox.IsEnabled = false;
            ListedCheckBox.IsEnabled = false;
            BottomPanelButtons.IsEnabled = false;

        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = PersonSelectionComboBox.SelectedItem as Person;
                if (person.IsListed == true)
                {
                    person.IsListed = false;
                    dbcontext.SaveChanges();
                    PersonSelectionComboBox.Items.Filter = new Predicate<object>(PersonIsListed);
                    PersonSelectionComboBox.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("This person is already unlisted, use the delete function instead.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No customer selected");
            }
        }

        private void RelistBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = PersonSelectionComboBox.SelectedItem as Person;
                if (person.IsListed != true)
                {
                    person.IsListed = true;
                    dbcontext.SaveChanges();
                    PersonSelectionComboBox.Items.Filter = new Predicate<object>(PersonIsListed);
                    PersonSelectionComboBox.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("This person is already listed.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No customer selected");
            }
        }


        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            UpdatePersonBtn.Visibility = Visibility.Visible;
            AbortUpdatePersonBtn.Visibility = Visibility.Visible;
            PersonDataGrids.IsEnabled = true;
            PersonSelectionComboBox.IsEnabled = false;
            ListedCheckBox.IsEnabled = false;
            NotesLabel.Visibility =   Visibility.Visible;
            NotesTextBox.Visibility = Visibility.Visible;
            BottomPanelButtons.IsEnabled = false;



        }

        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            Person person = PersonSelectionComboBox.SelectedItem as Person;
            if (person.IsListed == false)
            {
                
                foreach(Pet p in dbcontext.Pets)
                {
                    try
                    {
                        if (p.Owner.Kennitala == person.Kennitala)
                        {
                            Person dummy = dbcontext.People.FirstOrDefault(i => i.Kennitala.Equals("010101-0101")) as Person;
                            p.Owner = dummy;
                            p.IsListed = false;
                        }

                    }
                    catch (Exception)
                    {
                       
                    }
                }

                foreach(Appointment a in dbcontext.Appointments)
                {
                    try
                    {
                        if (a.Person.Kennitala == person.Kennitala)
                        {
                            Person dummy = dbcontext.People.Where(i => i.Kennitala.Equals("010101-0101")) as Person;
                            a.Person = dummy;
                        }

                    }
                    catch (Exception)
                    {
                    }
                }

                dbcontext.People.Remove(person);
                dbcontext.SaveChanges();


            }
            else
            {
                MessageBox.Show("This person is listed and therefore can not be deleted");
            }
        }

        private void PersonKennitalaComboBox(object sender, SelectionChangedEventArgs e)
        {
            
        }


        private void IsListed_Checked(object sender, RoutedEventArgs e)
        {
            PersonRemoveBtn.Visibility = Visibility.Collapsed;
            PersonRelistBtn.Visibility = Visibility.Visible;
            ShowIsListed = false;
            PersonSelectionComboBox.Items.Filter = new Predicate<object>(PersonIsListed);
            PersonSelectionComboBox.Items.Refresh();
            PersonSelectionComboBox.SelectedIndex = 0;
            ListedCheckBox.Content = "Unlisted";
            if(UserIsAdmin == true)
            {
                PersonDeleteBtn.Visibility = Visibility.Visible;

            }
        }

        private void IsListed_Unchecked(object sender, RoutedEventArgs e)
        {
            PersonRemoveBtn.Visibility = Visibility.Visible;
            PersonRelistBtn.Visibility = Visibility.Collapsed;
            PersonDeleteBtn.Visibility = Visibility.Collapsed;
            ShowIsListed = true;
            PersonSelectionComboBox.Items.Filter = new Predicate<object>(PersonIsListed);
            PersonSelectionComboBox.Items.Refresh();
            PersonSelectionComboBox.SelectedIndex = 0;
            ListedCheckBox.Content = "Listed";
        }

        private void SaveNewPersonBtnClick(object sender, RoutedEventArgs e)
        {
            Person newPerson = PersonDataGrids.DataContext as Person;
            dbcontext.People.Add(newPerson);
            dbcontext.SaveChanges();
            PersonDataGrids.DataContext = DataContext;
            BottomPanelButtons.IsEnabled = true;
            BaseVisibility();
        }



        private void AbortAddingPersonBtnClick(object sender, RoutedEventArgs e)
        {

            PersonDataGrids.DataContext = DataContext;
            BottomPanelButtons.IsEnabled = true;
            BaseVisibility();
        }

        private void UpdatePersonBtnClick(object sender, RoutedEventArgs e)
        {
            
            dbcontext.SaveChanges();
            BottomPanelButtons.IsEnabled = true;
            BaseVisibility();
        }

        private void AbortUpdatePersonBtnClick(object sender, RoutedEventArgs e)
        {
            BottomPanelButtons.IsEnabled = true;
            BaseVisibility();
            // TODO : Láta auto refresha


        }

        private void BaseVisibility()
        {
            SaveNewPersonBtn.Visibility = Visibility.Collapsed;
            AbortAddingPersonBtn.Visibility = Visibility.Collapsed;
            UpdatePersonBtn.Visibility = Visibility.Collapsed;
            AbortUpdatePersonBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility =   Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PersonDataGrids.IsEnabled = false;
            PersonSelectionComboBox.IsEnabled = true;
            ListedCheckBox.IsEnabled = true;
        }

        
    }
}
