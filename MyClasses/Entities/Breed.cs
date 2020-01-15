using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClasses.Entities
{
    public class Breed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string BreedName { get; set; }
        public Species Species { get; set; }
        public bool IsListed { get; set; } = true;
    }
}
