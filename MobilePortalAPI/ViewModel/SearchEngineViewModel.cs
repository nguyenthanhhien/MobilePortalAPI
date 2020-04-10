using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePortalAPI.ViewModel
{
    public class SearchEngineViewModel
    {
        public int id { get; set; }
        public int? page { get; set; }
        public int? itemsPerPage { get; set; }
        public string sort { get; set; }
        public string search { get; set; }
    }
}
