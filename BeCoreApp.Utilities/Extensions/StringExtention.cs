using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Utilities.Extensions
{
    public static class StringExtention
    {
        public static bool EndWith(this string value, params string[] compareString)
        {
            foreach (var item in compareString)
            {
                var result = value.EndsWith(item);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Equals(this string value, params string[] compareString)
        {
            foreach (var item in compareString)
            {
                var result = value.Equals(item);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
