using MyClasses.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using De.TorstenMandelkow.MetroChart;

namespace Final
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlInfograph : UserControl
    {
        GroomingDbContext dbContext = new GroomingDbContext();

        bool UserIsAdmin;

        public UserControlInfograph(bool _userIsAdmin)
        {
            UserIsAdmin = _userIsAdmin;
            InitializeComponent();

            dbContext.Appointments.Load();
            int qty = dbContext.Appointments.Count();

            DataContext = new DataViewModel();

            UserControlAppointment appointment = new UserControlAppointment(UserIsAdmin);

        }

        //Load info for pets chart
        private void RadialGaugeChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            int totalPets = dbContext.Pets.Count();

            De.TorstenMandelkow.MetroChart.RadialGaugeChart radialGaugeChart = sender as De.TorstenMandelkow.MetroChart.RadialGaugeChart;
            radialGaugeChart.ChartTitle = $"Total number of pets: {totalPets}";
            radialGaugeChart.ChartSubTitle = $"Pets per species";
            
            
        }

        //Load info for customers chart
        private void RadialGaugeChart_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            double totalCustomers = dbContext.People.Count();

            De.TorstenMandelkow.MetroChart.RadialGaugeChart radialGaugeChart = sender as De.TorstenMandelkow.MetroChart.RadialGaugeChart;
            radialGaugeChart.ChartTitle = $"Total number of customers: {totalCustomers}";
            radialGaugeChart.ChartSubTitle = $"Division by residential area";

        }

        //Load info for appointments
        private void RadialGaugeChart_Loaded_2(object sender, System.Windows.RoutedEventArgs e)
        {
            double totalAppointments = dbContext.Appointments.Count();

            De.TorstenMandelkow.MetroChart.RadialGaugeChart radialGaugeChart = sender as De.TorstenMandelkow.MetroChart.RadialGaugeChart;
            radialGaugeChart.ChartTitle = $"Total number of appointments: {totalAppointments}";
            radialGaugeChart.ChartSubTitle = $"Categorized";
        }
    }







}
