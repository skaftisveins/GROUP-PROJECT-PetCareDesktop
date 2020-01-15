using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClasses.Entities
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Person Person { get; set; }
        public Pet Pet { get; set; }
        public Service Service { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow.Date;
        public DateTime BookedDate { get; set; }
        public bool Islisted { get; set; } = true;
        public bool Completed { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        
    }
}