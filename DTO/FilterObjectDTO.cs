using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class FilterObjectDTO
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }
}
