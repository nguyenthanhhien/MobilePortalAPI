using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePortalAPI.ViewModel
{
    public class SearchEngineViewModel
    {
        public object id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StartIndex { get; set; }
        public string Sort { get; set; }
        public string Search { get; set; }
        public int Type { get; set; }
        public int StatusCode { get; set; }
        public string Filters { get; set; }
    }
}
