using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// JqueryMsg 的摘要说明
/// </summary>
public static class JqueryMsg
{
    /// <summary>
    /// 生成弹出信息的Jquery脚本
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static string CreateScript(string msg)
    {
        return "<script>$(document).ready(function(){$('#jsMsg').html('" + msg + "').show(300).delay(2000).hide(500);});</script >";
    }
}