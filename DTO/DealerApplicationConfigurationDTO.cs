using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DealerApplicationConfigurationDTO
    {
        public string DealerId { get; set; }
        public string OriginalApplication { get; set; }
        public string Application { get; set; }
        public string AllowAccess { get; set; }
        public string CreationEmpId { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public string DeviceId { get; set; }
        public int DealerApplicationConfigurationKey { get; set; }
        public string DeviceDescription { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool IsAllowAccess { get; set; }
        public string ExpiredDateString
        {
            get
            {
                if (ExpiredDate.HasValue)
                    return ExpiredDate.Value.ToShortDateString();
                return string.Empty;
            }
        }
    }
}
