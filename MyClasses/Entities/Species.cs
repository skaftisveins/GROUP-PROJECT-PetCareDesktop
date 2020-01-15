using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyClasses.Entities
{
    public class Species
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string SpeciesName { get; set; }
        public ICollection<Breed> Breeds { get; set; }
        public bool IsListed { get; set; } = true;
    }
}
