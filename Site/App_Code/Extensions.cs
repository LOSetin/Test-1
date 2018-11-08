using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 
/// </summary>
public static class Extensions
{
    public static string NewLineCharToBr(this string str)
    {
        return str.Replace("\r\n", "<br/>").Replace("\n", "<br/>");
    }
}