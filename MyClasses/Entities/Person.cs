using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClasses.Entities
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Kennitala { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Preferences { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public bool Notification { get; set; } = true;
        public bool IsListed { get; set; } = true;
    }
}
