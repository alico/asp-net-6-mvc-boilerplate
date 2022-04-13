using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Data.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifyDate { get; set; }
    }
}
