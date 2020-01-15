using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClasses.Entities
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ServiceType { get; set; }
        virtual public Species Species { get; set; }
        public bool HomeCare { get; set; }
        public string Description { get; set; }
        public bool IsListed { get; set; } = true;
    }
}
