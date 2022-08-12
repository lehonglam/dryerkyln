using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdin20.tdinCode
{
    class convertText
    {

        public static double ToDouble(string str)
        {
            if (str == null) return 0;
            if (str.Length > 0)
            {
                double x = 0;
                if (double.TryParse(str, out x)) return x;
            }
            return 0;
        }
        public static float ToFloat(string str)
        {
            if (str == null) return 0;
            if (str.Length > 0)
            {
                float x = 0;
                if (float.TryParse(str, out x)) return x;
            }
            return 0;
        }
        public static int ToInt(string str)
        {
            if (str == null) return 0;
            if (str.Length > 0)
            {
                int x = 0;
                if (int.TryParse(str, out x))
                    return x;
            }
            return 0;
        }
        public static DateTime ToDateTime(string str)
        {
            if (str == null) return DateTime.Today;
            if (str.Length > 0)
            {
                DateTime x;
                if (DateTime.TryParse(str, out x))
                    return x;
            }
            return DateTime.Today;
        }
        public static string ToString(object str)
        {
            if (str == null) return string.Empty;
            return str.ToString();

        }

    }
}
