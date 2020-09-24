using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace DTO
{
    public class SearchEngineDTO
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
        public List<FilterEngineDTO> FilterEngines
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Filters))
                    return new List<FilterEngineDTO>();
                return System.Text.Json.JsonSerializer.Deserialize<List<FilterEngineDTO>>(Filters);
            }
        }
        //public List<FilterEngineDTO> FilterEngines { get; set; }
    }
}
