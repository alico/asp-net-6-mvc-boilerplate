using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Data.Entities
{
    [Serializable]
    public class Setting : CountryBaseEntity<Guid>
    {
        public short? GroupId { get; set; }

        [StringLength(50)]
        public string Key { get; set; }

        [StringLength(1000)]
        public string Value { get; set; }

        public short Status { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifyDate { get; set; }
    }
}
