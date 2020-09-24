using System;
using System.Collections.Generic;

namespace Business.Models
{
    public partial class DealerConfiguration
    {
        public string DealerId { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public int? CountryCode { get; set; }
        public string LatestReleasedVersionNo { get; set; }
        public string CurrentReleasedVersionNo { get; set; }
        public int? AddressServerKey { get; set; }
        public int ServerTimeZoneOffsetHour { get; set; }
        public int ServerTimeZoneOffsetMinute { get; set; }
        public string IsDaylightSaving { get; set; }
        public string IsTitanDatabase { get; set; }
        public string TitanSysadPassword { get; set; }
        public string IsProduction { get; set; }
        public string IsTraining { get; set; }
        public int DaylightSavingMinute { get; set; }
        public int? DaylightSavingMonthFrom { get; set; }
        public int? DaylightSavingMonthTo { get; set; }
        public int? DaylightSavingHourFrom { get; set; }
        public int? DaylightSavingHourTo { get; set; }
        public int? DaylightSavingMinuteFrom { get; set; }
        public int? DaylightSavingMinuteTo { get; set; }
        public int DaylightSavingWeekFrom { get; set; }
        public int DaylightSavingWeekTo { get; set; }
        public int DaylightSavingDayOfWeek { get; set; }
        public string TimeZoneName { get; set; }
        public string OverrideDatabaseName { get; set; }
        public int? EmailServerKey { get; set; }
        public int? MobileServerKey { get; set; }
        public int? NoOfLicenses { get; set; }
        public string DealershipNotificationEmailAddress { get; set; }
        public string TitanNotificationEmailAddress { get; set; }
        public decimal TimeZoneOffSet { get; set; }
        public string UseLicensingControl { get; set; }
        public int? NumberOfExtendedDay { get; set; }
        public DateTime? StartCountingDate { get; set; }
        public string LockExceededLogIn { get; set; }
        public int? LicencingControlUserLimit { get; set; }
        public string BypassIpValidation { get; set; }
    }
}
