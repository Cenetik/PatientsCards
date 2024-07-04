using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class StringExtension
    {
        public static bool IsNumeric(this string str) 
        {
            if (string.IsNullOrEmpty(str)) return true;

            return str.All(char.IsDigit);
        }
    }
}
