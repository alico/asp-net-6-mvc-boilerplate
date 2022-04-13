using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Data.Entities
{
    [Serializable]
    public class CountryBaseEntity<T> : BaseEntity<T>
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
