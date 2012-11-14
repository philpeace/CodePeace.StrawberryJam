using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodePeace.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToFormat(this string value, params object[] args)
        {
            return string.Format(value, args);
        }
    }
}
