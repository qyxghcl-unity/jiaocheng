using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public static class StringExt
{
    public static int ToInt(this string str)
    {
        return int.Parse(str);
    }
    public static float ToFloat(this string str)
    {
        return float.Parse(str);
    }
    public static bool ToBool(this string str)
    {
        return bool.Parse(str);
    }
}
