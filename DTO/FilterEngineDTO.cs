using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class FilterEngineDTO
    {
        public List<FilterObjectDTO> Filters { get; set; }
        public string Logic { get; set; }
    }
}
