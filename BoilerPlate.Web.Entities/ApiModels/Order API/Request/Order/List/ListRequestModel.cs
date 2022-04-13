using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record ListRequestModel
    {
        [Range(1, 1000)]
        public int PageNumber { get; set; }

        [Range(1, 100)]
        public int PageItemCount { get; set; }

        public ListRequestModel()
        {
            PageNumber = 1;
            PageItemCount = 100;
        }
    }
}
