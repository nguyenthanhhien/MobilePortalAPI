using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Business
{
    public abstract class BaseService
    {
        protected CommonContext CommonContext;
        protected readonly IMapper Mapper;

        protected BaseService(CommonContext context)
        {
            CommonContext = context;
        }
        protected BaseService(CommonContext context, IMapper mapper)
        {
            CommonContext = context;
            Mapper = mapper;
        }

        public void SetConnectionString(string commonServerName, string username, string password)
        {
            var connectionString = Utilities.CreateCommonConnection(commonServerName, username, password);
            CommonContext = new CommonContext(connectionString);
        }
    }
}
