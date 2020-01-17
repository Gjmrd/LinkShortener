using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            if (s == null)
                return true;

            if (s.Trim().Length == 0)
                return true;

            return false;
        }
    }
}
