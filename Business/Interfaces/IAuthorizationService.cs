using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IAuthorizationService
    {
        int Authenticate(string commonServerName, string userId, string password);
    }
}
