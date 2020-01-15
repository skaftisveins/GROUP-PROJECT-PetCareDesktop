using MyClasses.Entities;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Final
{
    internal class DataViewModel
    {
        GroomingDbContext dbContext = new GroomingDbContext();

        public ObservableCollection<Appointment> Quantity { get; set; }
        public ObservableCollection<Person> Customers { get; set; }
        public ObservableCollection<Pet> Pets { get; set; }

        public DataViewModel()
        {
            dbContext.Appointments.Load();
            dbContext.People.Load();
            dbContext.Pets.Load();

            double totalPets = dbContext.Pets.Count();
            double totalAppointments = dbContext.Appointments.Count();
            double totalCustomers = dbContext.People.Count();

            #region Appointment chart context
            //Appointment chart
            Quantity = new ObservableCollection<Appointment>();

            int totalOne = dbContext.Appointments.Local.Where(o => o.Islisted.Equals(true)).Where(o => o.Completed.Equals(false)).Count();
            int totalTwo = dbContext.Appointments.Local.Where(o => o.Completed.Equals(true)).Count();
            int totalThree = dbContext.Appointments.Local.Where(o => o.Islisted.Equals(false)).Count();

            Quantity.Add(new Appointment()
            {
                Quantity = (int)Math.Round((dbContext.Appointments.Local.Where(o => o.Islisted.Equals(true)).Where(o => o.Completed.Equals(false)).Count() / totalAppointments) * 100),
                Comment = $"Active: {totalOne}"
            });

            Quantity.Add(new Appointment()
            {
                Quantity = (int)Math.Round((dbContext.Appointments.Local.Where(o => o.Completed.Equals(true)).Count() / totalAppointments) * 100),
                Comment = $"Completed: {totalTwo}"
            });

            Quantity.Add(new Appointment()
            {
                Quantity = (int)Math.Round((dbContext.Appointments.Local.Where(o => o.Islisted.Equals(false)).Count() / totalAppointments) * 100),
                Comment = $"Cancelled: {totalThree}"
            });

            #endregion

            #region Customer chart context
            //Customers chart

            int totalInCapital = dbContext.People.Where(o => o.Zip <= 221).Count();
            int totalInCountrySide = dbContext.People.Where(o => o.Zip > 221).Count();

            Customers = new ObservableCollection<Person>
            {
                new Person() { FirstName = $"Capital area: {totalInCapital}", Zip = (int)Math.Round((dbContext.People.Where(o=> o.Zip<=221).Count()/ totalCustomers) * 100)},
                new Person() { FirstName = $"Countryside: {totalInCountrySide}", Zip = (int)Math.Round((dbContext.People.Where(o=> o.Zip>221).Count()/ totalCustomers) * 100)},

            };


            #endregion

            #region Pets chart datacontext
            //Jebb, you guessed it, the Pets/Species chart
            Pets = new ObservableCollection<Pet>();

            foreach (Species species in dbContext.Species)
            {
                double totalSpecies = dbContext.Pets.Where(o => o.Species.SpeciesName == species.SpeciesName).Count();
                int d = (int)Math.Round((totalSpecies / totalPets) * 100);

                Pets.Add(new Pet() { Health = $"{species.SpeciesName}: {totalSpecies}", Age = d });
            };

            #endregion

        }


    }

}