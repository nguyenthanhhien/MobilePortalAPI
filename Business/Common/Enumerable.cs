using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public enum LoginStatus
    {
        Fail = -1,
        Success = 1,
        NoPermission = 2 
    }

    public enum ClaimTypes { 
        CommonServerName,
        Username,
        Password
    }

    public enum SortTypes
    {
        Desc,
        Asc
    }

    public enum DealerApplicationConfigFields
    {
        Application,
        OriginalApplication,
        DealerId,
        AllowAccess,
        DeviceId,
        DeviceDescription,
        ExpiredDateString,
        ExpiredDate
    }

    public enum LogicFilters
    {
        And,
        Or
    }

}
