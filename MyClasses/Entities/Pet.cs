using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses.Entities
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Person Owner { get; set; }
        virtual public Species Species { get; set; }
        public Breed Breed { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Allergy { get; set; }
        public string Health { get; set; }
        public string Preferences { get; set; }
        public string EmployeeNotes { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public bool IsListed { get; set; } = true;
    }
}
