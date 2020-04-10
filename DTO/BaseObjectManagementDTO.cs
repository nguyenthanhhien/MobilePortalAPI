using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BaseObjectManagementDTO<T>
    {
        public BaseObjectManagementDTO()
        {
            ObjectList = new List<T>();
        }
        public List<T> ObjectList { get; set; }
        public int TotalItems { get; set; }
    }
}
