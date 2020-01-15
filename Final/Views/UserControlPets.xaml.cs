using MyClasses.Entities;
using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Final
{
    /// <summary>
    /// Interaction logic for UserControlPets.xaml
    /// </summary>
    public partial class UserControlPets : UserControl
    {
        GroomingDbContext dbContext = new GroomingDbContext();

        bool ShowIsListed = true;
        bool UserIsAdmin;

        public UserControlPets(bool _userIsAdmin)
        {
            UserIsAdmin = _userIsAdmin;
            InitializeComponent();

            dbContext.Pets.Load();
            dbContext.People.Load();
            dbContext.Breeds.Load();
            dbContext.Species.Load();

            PetSelectionComboBox.Items.Filter = new Predicate<object>(PetIsListed);

            CollectionViewSource view = new CollectionViewSource();
            view.Source = dbContext.Pets.Local;
            DataContext = view;

            CollectionViewSource PeopleView = new CollectionViewSource();
            PeopleView.Source = dbContext.People.Local;
            //til að geta bara búið til pet fyrir listed person
            //OwnerSelectionComboBox.Items.Filter = new Predicate<object>(PersonIsListed);
            OwnerSelectionComboBox.DataContext = PeopleView;

            CollectionViewSource SpeciesView = new CollectionViewSource();
            SpeciesView.Source = dbContext.Species.Local;
            SpeciesSelectionComboBox.DataContext = SpeciesView;

            OnWindowLoadVisibility();

            PetDataGrids.IsEnabled = false;
        }

        private void OnWindowLoadVisibility()
        {
            PetRelistBtn.Visibility = Visibility.Collapsed;
            SaveNewPetBtn.Visibility = Visibility.Collapsed;
            AbortAddingPetBtn.Visibility = Visibility.Collapsed;
            UpdatePetBtn.Visibility = Visibility.Collapsed;
            AbortUpdatePetBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            OwnerSelectionComboBox.Visibility = Visibility.Collapsed;
            BreedSelectionComboBox.Visibility = Visibility.Collapsed;
            SpeciesSelectionComboBox.Visibility = Visibility.Collapsed;
            PetDeleteBtn.Visibility = Visibility.Collapsed;
            
        }

        public bool PetIsListed(Object pets)
        {
            Pet pet = pets as Pet;
            return (pet.IsListed == ShowIsListed);
        }

        public bool BreedBelongsToSpecies(Object breeds)
        {
            Breed breed = breeds as Breed;
            return (breed.Species == SpeciesSelectionComboBox.SelectedItem);
        }

        public bool PersonIsListed(Object people)
        {
            Person person = people as Person;
            return (person.IsListed == ShowIsListed);
        }

        public void AddBtnClick(object sender, RoutedEventArgs e)
        {
            ListedCheckBox.Visibility = Visibility.Collapsed;
            PetSelectionComboBox.Visibility = Visibility.Collapsed;
            PetDataGrids.DataContext = new Pet();
            SaveNewPetBtn.Visibility = Visibility.Visible;
            AbortAddingPetBtn.Visibility = Visibility.Visible;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PetDataGrids.IsEnabled = true;
            OwnerSelectionComboBox.Visibility = Visibility.Visible;
            BreedSelectionComboBox.Visibility = Visibility.Visible;
            SpeciesSelectionComboBox.Visibility = Visibility.Visible;
            BottomPanelButtons.IsEnabled = false;
        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Pet pet = PetSelectionComboBox.SelectedItem as Pet;
                if (pet.IsListed == true)
                {
                    pet.IsListed = false;
                    dbContext.SaveChanges();
                    PetSelectionComboBox.Items.Filter = new Predicate<object>(PetIsListed);
                    PetSelectionComboBox.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("This pet is already unlisted, use the delete function instead.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No pet selected","No Pet");               
            }
        }

        private void RelistBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Pet pet = PetSelectionComboBox.SelectedItem as Pet;
                if (pet.IsListed != true)
                {
                    pet.IsListed = true;
                    dbContext.SaveChanges();
                    PetSelectionComboBox.Items.Filter = new Predicate<object>(PetIsListed);
                    PetSelectionComboBox.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("This pet is already unlisted, use the delete function instead.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No pet selected", "No Pet");
            }
        }

        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            UpdatePetBtn.Visibility = Visibility.Visible;
            AbortUpdatePetBtn.Visibility = Visibility.Visible;
            NotesLabel.Visibility = Visibility.Visible;
            NotesTextBox.Visibility = Visibility.Visible;
            PetDataGrids.IsEnabled = true;
            ListedCheckBox.IsEnabled = false;
            PetSelectionComboBox.IsEnabled = false;
            BottomPanelButtons.IsEnabled = false;
        }

        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            Pet pet = PetSelectionComboBox.SelectedItem as Pet;

            if (pet.IsListed == false)
            {
                dbContext.Pets.Remove(pet);
                dbContext.SaveChanges();
            }
            else
            {
                MessageBox.Show("This person is listed and therefore can not be deleted");
            }
        }

        private void PetNameComboBox(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveNewPetBtnClick(object sender, RoutedEventArgs e)
        {
            Pet newPet = PetDataGrids.DataContext as Pet;
            newPet.Owner = OwnerSelectionComboBox.SelectedItem as Person;
            newPet.Species = SpeciesSelectionComboBox.SelectedItem as Species;
            newPet.Breed = BreedSelectionComboBox.SelectedItem as Breed;
            dbContext.Pets.Add(newPet);
            dbContext.SaveChanges();
            SaveNewPetBtn.Visibility = Visibility.Collapsed;
            AbortAddingPetBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PetDataGrids.DataContext = DataContext;
            PetDataGrids.IsEnabled = false;
            PetSelectionComboBox.Visibility = Visibility.Visible;
            OwnerSelectionComboBox.Visibility = Visibility.Collapsed;
            BreedSelectionComboBox.Visibility = Visibility.Collapsed;
            SpeciesSelectionComboBox.Visibility = Visibility.Collapsed;
            ListedCheckBox.Visibility = Visibility.Visible;
            BottomPanelButtons.IsEnabled = true;
        }

        private void AbortAddingPetBtnClick(object sender, RoutedEventArgs e)
        {
            PetDataGrids.DataContext = DataContext;
            AbortAddingPetBtn.Visibility = Visibility.Collapsed;
            SaveNewPetBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PetDataGrids.IsEnabled = false;
            PetSelectionComboBox.Visibility = Visibility.Visible;
            OwnerSelectionComboBox.Visibility = Visibility.Collapsed;
            BreedSelectionComboBox.Visibility = Visibility.Collapsed;
            SpeciesSelectionComboBox.Visibility = Visibility.Collapsed;
            ListedCheckBox.Visibility = Visibility.Visible;
            BottomPanelButtons.IsEnabled = true;
        }

        private void UpdatePetBtnClick(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            ListedCheckBox.IsEnabled = false;
            UpdatePetBtn.Visibility = Visibility.Collapsed;
            AbortUpdatePetBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PetDataGrids.IsEnabled = false;
            ListedCheckBox.IsEnabled = true;
            PetSelectionComboBox.IsEnabled = true;
            BottomPanelButtons.IsEnabled = true;

        }

        private void AbortUpdatePetBtnClick(object sender, RoutedEventArgs e)
        {
            UpdatePetBtn.Visibility = Visibility.Collapsed;
            AbortUpdatePetBtn.Visibility = Visibility.Collapsed;
            NotesLabel.Visibility = Visibility.Collapsed;
            NotesTextBox.Visibility = Visibility.Collapsed;
            PetDataGrids.IsEnabled = false;
            ListedCheckBox.IsEnabled = true;
            PetSelectionComboBox.IsEnabled = true;
            BottomPanelButtons.IsEnabled = true;
        }

        private void SpeciesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource BreedView = new CollectionViewSource();
            BreedView.Source = dbContext.Breeds.Local;
            BreedSelectionComboBox.Items.Filter = new Predicate<object>(BreedBelongsToSpecies);
            BreedSelectionComboBox.DataContext = BreedView;
        }

        private void IsListed_Checked(object sender, RoutedEventArgs e)
        {
            PetRemoveBtn.Visibility = Visibility.Collapsed;
            PetRelistBtn.Visibility = Visibility.Visible;
            ShowIsListed = false;
            PetSelectionComboBox.Items.Filter = new Predicate<object>(PetIsListed);
            PetSelectionComboBox.Items.Refresh();
            PetSelectionComboBox.SelectedIndex = 0;
            ListedCheckBox.Content = "Unlisted";
            if(UserIsAdmin == true)
            {
                PetDeleteBtn.Visibility = Visibility.Visible;
            }
        }

        private void IsListed_Unchecked(object sender, RoutedEventArgs e)
        {
            PetRemoveBtn.Visibility = Visibility.Visible;
            PetRelistBtn.Visibility = Visibility.Collapsed;
            PetDeleteBtn.Visibility = Visibility.Collapsed;
            ShowIsListed = true;
            PetSelectionComboBox.Items.Filter = new Predicate<object>(PetIsListed);
            PetSelectionComboBox.Items.Refresh();
            PetSelectionComboBox.SelectedIndex = 0;
            ListedCheckBox.Content = "Listed";
        }

        
    }
}
