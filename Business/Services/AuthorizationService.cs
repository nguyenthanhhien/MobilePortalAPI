using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        public AuthorizationService(CommonContext commonContext) : base(commonContext)
        {

        }
        public int Authenticate(string commonServerName, string userId, string password)
        {
            var connectionString = CommonMethods.CreateCommonConnection(commonServerName, userId, password);
            CommonContext = new CommonContext(connectionString);
            try
            {
                if (CommonContext.Database.CanConnect())
                {
                    try
                    {
                        if (CommonContext.DealerApplicationConfiguration.Count() >= 0)
                            return (int)LoginStatus.Success;
                    }
                    catch (Exception)
                    {
                        return (int)LoginStatus.NoPermission;
                    }
                }
            }
            catch (Exception ex)
            {
                return (int)LoginStatus.Fail;
            }
            return (int)LoginStatus.Fail;

        }
    }
}
