using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework.Extensions;
using System.Web.UI;

/// <summary>
/// JsBox 的摘要说明
/// </summary>
public static class JsBox
{
    /// <summary>
    /// 弹出jsbox
    /// </summary>
    /// <param name="msg">弹出信息</param>
    /// <param name="url">如果为空，则不跳转，如果为-1，则返回上一页，否则跳转到指定的url</param>
    public static void Alert(string msg,string url="")
    {
        if (url.IsNullOrEmpty())
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');</script>");
        else
        {
            if(url=="-1")
            {
                HttpContext.Current.Response.Write("<script>alert('" + msg + "');history.go(-1);</script>");
            }
            else
            {
                HttpContext.Current.Response.Write("<script>alert('" + msg + "');window.location.href='" + url + "';</script>");
            }
        }
    }

    /// <summary>
    /// 跳转到指定的url，如果为-1，则表示返回上一页
    /// </summary>
    /// <param name="url"></param>
    public static void Jump(string url)
    {
        if(url=="-1")
        {
            HttpContext.Current.Response.Write("<script>history.go(-1);</script>");
        }
        else
        {
            HttpContext.Current.Response.Write("<script>window.location.href='" + url + "';</script>");
        }
    }

    /// <summary>
    /// 弹出遮蔽框
    /// </summary>
    /// <param name="msg"></param>
    public static void ShowCoverLayer(string msg)
    {
        ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
        csm.RegisterStartupScript(typeof(Page), "", "MessageBox('notice','" + msg + "', '提示');", true);
    }

    /// <summary>
    /// 弹出js信息
    /// </summary>
    /// <param name="msg"></param>
    public static void Show(string msg)
    {
        ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
        csm.RegisterClientScriptBlock(typeof(Page), "", "window.onload=function(){ alert('" + msg + "')};", true);
    }

}