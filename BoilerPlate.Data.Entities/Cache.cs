using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Data.Entities
{
    public class Cache
    {
        [StringLength(1000)]
        public string Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset? ExpiresAtTime { get; set; }
        public long? SlidingExpirationInSeconds { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
    }
}
