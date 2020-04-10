using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Http;

namespace MobilePortalAPI.Controllers
{
    public class BaseController
    {
        public BaseController(BaseService baseService, IHttpContextAccessor httpContextAccessor)
        {
            var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Username.ToString()).Value;
            var password = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Password.ToString()).Value;
            var commonServerName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.CommonServerName.ToString()).Value;
            baseService.SetConnectionString(commonServerName, username, password);

        }
    }
}
