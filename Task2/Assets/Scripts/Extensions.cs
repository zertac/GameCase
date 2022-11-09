using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Extensions
{
    public static string ToColor(this Dictionary<string, string> o, string v)
    {
        return o.FirstOrDefault(x => x.Key == v).Value;
    }

    public static string ToValue(this Dictionary<string, string> o, string v)
    {
        return o.FirstOrDefault(x => x.Key == v).Value;
    }
}
