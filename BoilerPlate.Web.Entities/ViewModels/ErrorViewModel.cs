using System;

namespace BoilerPlate.Web.Entities
{
    public record ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
