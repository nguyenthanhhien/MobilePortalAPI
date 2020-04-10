using System;
using System.Collections.Generic;

namespace Business.Models
{
    public partial class DealerApplicationConfiguration
    {
        public string DealerId { get; set; }
        public string Application { get; set; }
        public string AllowAccess { get; set; }
        public string CreationEmpId { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public string DeviceId { get; set; }
        public int DealerApplicationConfigurationKey { get; set; }
        public string DeviceDescription { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
