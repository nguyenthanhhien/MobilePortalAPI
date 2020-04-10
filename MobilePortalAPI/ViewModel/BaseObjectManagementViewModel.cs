using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePortalAPI.ViewModel
{
    public class BaseObjectManagementViewModel<T>
    {
        public BaseObjectManagementViewModel()
        {
            ObjectList = new List<T>();
        }
        public List<T> ObjectList { get; set; }
        public int TotalItems { get; set; }
    }
}
