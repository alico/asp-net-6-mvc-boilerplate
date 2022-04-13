using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Data.Entities
{
    public class ApplicationLogs : BaseEntity<Guid>
    {
        [StringLength(50)]
        public Guid RequestId { get; set; }

        [StringLength(5000)]
        public string Message { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        [StringLength(5000)]
        public string Exception { get; set; }

        [StringLength(5000)]
        public string Properties { get; set; }
    }
}
