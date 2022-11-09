using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public static class Extensions
{
    // this extension for convert string to color
    public static Color ToColor(this string hex)
    {
        Color color = new Color(0, 0, 0);

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }

        return color;
    }
}
