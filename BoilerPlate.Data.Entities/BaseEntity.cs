using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Data.Entities
{
    [Serializable]
    public class BaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}
