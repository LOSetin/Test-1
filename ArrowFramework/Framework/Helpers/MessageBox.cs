using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Arrow.Framework
{
    /// <summary>
    /// 弹出信息窗口
    /// </summary>
    public static class MessageBox
    {
        #region 弹出js提示窗口
        /// <summary>
        /// 弹出js提示窗口
        /// </summary>
        /// <param name="msg">弹出的信息</param>

        public static void Show(string msg)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
       
            csm.RegisterStartupScript(typeof(Page), "", "alert('" + msg + "');", true);
        }

        /// <summary>
        /// 弹出信息并跳转
        /// </summary>
        /// <param name="msg">弹出的信息</param>
        /// <param name="url">跳转的url</param>
        public static void Show(string msg, string url)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
            csm.RegisterStartupScript(typeof(Page), "", "alert('" + msg + "');window.location.href='" + url + "';", true);
        }

        /// <summary>
        /// 弹出信息并跳转
        /// </summary>
        /// <param name="msg">弹出的信息</param>
        /// <param name="url">跳转的url</param>
        ///<param name="target">在那个框架跳转</param>
        public static void Show(string msg, string url, string target)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
            csm.RegisterStartupScript(typeof(Page), "", "alert('" + msg + "');" + target + " .location.href='" + url + "';", true);
        }

        /// <summary>
        /// 弹出信息并后退或关闭
        /// </summary>
        /// <param name="msg">弹出的信息</param>
        /// <param name="back">true则后退，false则关闭</param>
        public static void Show(string msg, bool back)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
            if (back)
            {
                csm.RegisterStartupScript(typeof(Page), "", "alert('" + msg + "');history.go(-1);", true);
            }
            else
            {
                csm.RegisterStartupScript(typeof(Page), "", "alert('" + msg + "');window.close();", true);
            }
        }

        /// <summary>
        /// 弹出信息后，延时刷新本页
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="timeOut">延时时间，单位毫秒</param>

        public static void Show(string msg, int timeOut)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
            csm.RegisterStartupScript(typeof(Page), "", "alert('" + msg + "');setTimeout('window.location.reload()'," + timeOut.ToString() + ");", true);

        }

        /// <summary>
        /// 不弹出窗口直接跳转
        ///<param name="url">跳转的URL，如果url="-1"，表示后退</param>
        /// </summary>
        public static void Jump(string url)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
            if (url == "-1")
            {
                csm.RegisterStartupScript(typeof(Page), "", "history.go(-1);", true);
            }
            else
            {
                csm.RegisterStartupScript(typeof(Page), "", "window.location.href='" + url + "';", true);
            }
        }

        /// <summary>
        /// 不弹出窗口直接跳转
        /// </summary>
        /// <param name="url">跳转的地址</param>
        /// <param name="target">跳转的框架</param>
        public static void Jump(string url, string target)
        {
            ClientScriptManager csm = ((Page)(HttpContext.Current.Handler)).ClientScript;
            csm.RegisterStartupScript(typeof(Page), "", target + ".location.href='" + url + "';", true);
        }
        #endregion

        //以下需要.net3.5以上才支持
        #region 使用ScriptManager时的弹出窗口，主要针对Ajax

        /// <summary>
        /// 弹出信息
        /// </summary>
        /// <param name="msg">要弹出的信息</param>
        //弹出信息框
        public static void AjaxMsg(string msg)
        {
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "ajaxmsg", "alert('" + msg + "');", true);
        }


        /// <summary>
        /// 弹出信息并跳转
        /// </summary>
        /// <param name="msg">弹出的信息</param>
        /// <param name="url">跳转的url</param>
        public static void AjaxMsg(string msg, string url)
        {
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page),"ajaxmsg", "alert('" + msg + "');window.location.href='" + url + "';", true);
        }


        /// <summary>
        /// 弹出信息并跳转
        /// </summary>
        /// <param name="msg">弹出的信息</param>
        /// <param name="url">跳转的url</param>
        ///<param name="target">在那个框架跳转</param>
        public static void AjaxMsg(string msg, string url, string target)
        {
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "ajaxmsg", "alert('" + msg + "');" + target + " .location.href='" + url + "';", true);
        }


        /// <summary>
        /// 弹出信息并关闭页面或返回上一页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="back">true则弹出信息后后退，false则弹出信息后关闭</param>
        public static void AjaxMsg(string msg, bool back)
        {
            if (back)
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "backmsg", "alert('" + msg + "');history.go(-1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "closemsg", "alert('" + msg + "');window.close();", true);
            }

        }

        /// <summary>
        /// 弹出信息后，延时刷新本页
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="timeOut">延时时间，单位毫秒</param>

        public static void AjaxMsg(string msg, int timeOut)
        {
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "closemsg", "alert('" + msg + "');setTimeout('window.location.reload()'," + timeOut.ToString() + ");", true);
        }

        /// <summary>
        /// 不弹出窗口直接跳转
        ///<param name="url">跳转的URL，如果url="-1"，表示后退</param>
        /// </summary>
        public static void AjaxJump(string url)
        {
            if (url == "-1")
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler,typeof(Page), "ajaxback", "history.go(-1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(Page), "ajaxjump", "window.location.href='" + url + "';", true);
            }
        }

        /// <summary>
        /// 不弹出窗口直接跳转
        /// </summary>
        /// <param name="url">跳转的地址</param>
        /// <param name="target">跳转的框架</param>
        public static void AjaxJump(string url, string target)
        {
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(Page), "ajaxjump", target + ".location.href='" + url + "';", true);
        }

        #endregion
 
    }
}

