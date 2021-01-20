using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class CommonHelpers
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string Join<T>(this IEnumerable<T> objects, string seperator)
        {
            return string.Join(seperator, objects);
        }

        public static string Join(this IEnumerable<object> objects, string seperator)
        {
            return string.Join(seperator, objects);
        }

        public static string ToProperCase(this string str)
        {
            var sets = str.Split(" ");
            for (var i = 0; i < sets.Length; i++)
                sets[i] = sets[i][0].ToString().ToUpper() + sets[i].Substring(1);

            return sets.Join(" ");
        }

        public static IEnumerable<string> ToProperCase(this IEnumerable<string> str)
        {
            return str.Select(ToProperCase);
        }

    }
}
