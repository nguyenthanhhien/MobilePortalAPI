using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Constant
    {
        public const string AscSort = "asc";
        public const string DescSort = "desc";

        public const string EqualToFilter = "eq";
        public const string NotEqualToFilter = "neq";
        public const string EqualToNullFilter = "isnull";
        public const string NotEqualToNullFilter = "isnotnull";
        public const string LessThanFilter = "lt";
        public const string LessThanOrEqualToFilter = "lte";
        public const string GreaterThanFilter = "gt";
        public const string GreaterThanOrEqualToFilter = "gte";
        public const string StartsWithFilter = "startswith";
        public const string EndsWithFilter = "endswith";
        public const string ContainsToFilter = "contains";
        public const string DoesNotContainFilter = "doesnotcontain";
        public const string IsEmptyFilter = "isempty";
        public const string IsNotEmptyFilter = "isnotempty";
    }
}
