using System;
using System.Text;
using System.Web;
using Arrow.Framework.Extensions;
using System.IO;

namespace Arrow.Framework
{
    /// <summary>
    /// WebPage基类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 当前分页的url参数
        /// </summary>
        public static readonly string UrlPara_CurrentPageIndex = "p";

        /// <summary>
        /// 返回地址的url参数，值为ReturnUrl
        /// </summary>
        public static readonly string UrlPara_ReturnUrl = "ReturnUrl";
       
        #region 查询字符串转换为字符串
        /// <summary>
        /// 将Request.QueryString[UrlPara]转换为字符串，对象为null或DBNull.Value,则返回""
        /// </summary>
        /// <param name="UrlPara"></param>
        /// <returns></returns>
        protected string GetUrlString(string UrlPara)
        {
            return HttpContext.Current.Request.QueryString[UrlPara];
        }

        #endregion

        #region 查询字符串转换为整型
        /// <summary>
        /// 将Request.QueryString[UrlPara]转换为整型
        /// </summary>
        /// <param name="UrlPara">url查询参数，转换不成功转换为0</param>
        /// <returns></returns>
        protected int GetUrlInt(string UrlPara)
        {
            return GetUrlInt(UrlPara, 0);
        }

        /// <summary>
        /// 将Request.QueryString[UrlPara]转换为整型
        /// </summary>
        /// <param name="UrlPara">url参数</param>
        /// <param name="defaultVal">url参数无法转为整型时的默认值</param>
        /// <returns></returns>
        protected int GetUrlInt(string UrlPara, int DefaultValue)
        {
            return HttpContext.Current.Request.QueryString[UrlPara].AsInt(DefaultValue);
        }
        #endregion

        #region 查询字符串转换为货币
        /// <summary>
        /// 将Request.QueryString[UrlPara]转换为decimal
        /// </summary>
        /// <param name="UrlPara"></param>
        /// <param name="defaultVal">转换失败时的默认值</param>
        /// <returns></returns>
        protected decimal GetUrlDecimal(string UrlPara, decimal defaultVal)
        {
            return HttpContext.Current.Request.QueryString[UrlPara].AsDecimal(defaultVal);
        }

        /// <summary>
        /// 将Request.QueryString[UrlPara]转换为decimal,转换失败返回0
        /// </summary>
        /// <param name="UrlPara"></param>
        /// <returns></returns>
        protected decimal GetUrlDecimal(string UrlPara)
        {
            return GetUrlDecimal(UrlPara, 0);
        }
        #endregion

        #region 查询字符串转换为日期
        /// <summary>
        ///  将Request.QueryString[UrlPara]转换为datetime
        /// </summary>
        /// <param name="UrlPara"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        protected DateTime GetUrlDateTime(string UrlPara, DateTime defaultVal)
        {
            return HttpContext.Current.Request.QueryString[UrlPara].AsDateTime(defaultVal);
        }

        /// <summary>
        ///  将Request.QueryString[UrlPara]转换为datetime,不能转换则返回DateTime.MinValue;
        /// </summary>
        /// <param name="UrlPara"></param>
        /// <returns></returns>
        protected DateTime GetUrlDateTime(string UrlPara)
        {
            return GetUrlDateTime(UrlPara, DateTime.MinValue);
        }
        #endregion

        #region 获得ArrowControl的当前分页，统一使用参数p作为分页参数
        /// <summary>
        /// 获得ArrowControl的当前分页，统一使用参数p作为分页参数
        /// </summary>
        protected int ArrowControlPageIndex
        {
            get
            {
                int page = GetUrlInt(UrlPara_CurrentPageIndex, 1); 
                if (page <= 0) page = 1;
                return page;
            }
        }
        #endregion

        #region 错误处理
        /// <summary>
        /// 错误处理
        /// </summary>
        protected virtual void ShowError()
        {
            HttpContext.Current.Response.Clear();
            StringBuilder sb = new StringBuilder();
            Exception currentError = HttpContext.Current.Server.GetLastError();
            sb.Append("<p style=\"color:#d00; font-size:14px; padding:4px 10px;\">系统发生错误：</p>\n");
            sb.Append("<ul style=\"font-size:12px;\">\n");
            sb.Append(" <li>错误地址：" + HttpContext.Current.Request.Url.ToString() + "</li>\n");
            sb.Append(" <li>错误信息：" + currentError.ToString() + "</li>\n");
            sb.Append(" <li>出错时间：" + DateTime.Now.ToString() + "</li>\n");
            sb.Append("</ul>\n");
            sb.Append("<hr />\n");
            sb.Append("<p style=\"padding:4px 10px; font-size:12px;\"><a href=\"javascript:history.back()\" title=\"返回上一页面\">返回上一页面</a></p>");

            HttpContext.Current.Server.ClearError();
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End(); 
        }

        private string logPath = "~/Log/";
        protected virtual void SaveLog()
        {
            HttpContext.Current.Response.Clear();
            StringBuilder sb = new StringBuilder();
            Exception currentError = HttpContext.Current.Server.GetLastError();
            sb.AppendLine("错误地址：" + HttpContext.Current.Request.Url.ToString());
            sb.AppendLine("错误信息：" + currentError.ToString());
            sb.Append("出错时间：" + DateTime.Now.ToString());
            string logName = DateTime.Now.ToDateOnlyString() + ".txt";
            string fullPath = Server.MapPath(logPath + logName);
            string basePath = Server.MapPath(logPath);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            File.AppendAllText(fullPath, sb.ToString());

        }
        #endregion

        #region 属性

        /// <summary>
        /// 网站根目录的url地址
        /// </summary>
        public string SiteRootUrl
        {
            get { return HttpHelper.GetRootURI(); }
        }

        /// <summary>
        /// 当前页面完整的地址
        /// </summary>
        public string CurrentUrl
        {
            get {
                    return HttpContext.Current.Request.Url.AbsoluteUri;
            }
        }

        /// <summary>
        /// 返回当前地址中的参数ReturnUrl的值
        /// </summary>
        public string CurrentReturnUrl
        {
            get { return GetUrlString("ReturnUrl"); }
        }

        /// <summary>
        /// 设置页面是否过期，如果设置为true，而页面经过post，则点后退按钮会提示页面过期
        /// </summary>
        public virtual bool EnablePageExpires
        {
            get { return false; }
        }

        /// <summary>
        /// 当错误发生时，是否显示自定义错误信息
        /// </summary>
        public virtual bool ShowCustomErrorMsg
        {
            get { return false; }
        }
        #endregion

        #region 地址生成及跳转
        /// <summary>
        /// 将当前地址作为返回地址，生成ReturnUrl=Server.UrlEncode(当前地址)
        /// </summary>
        /// <param name="prefix">?或&amp;</param>
        /// <returns></returns>
        public virtual string CreateReturnUrl(string prefix)
        {
            return prefix + UrlPara_ReturnUrl + "=" + Server.UrlEncode(CurrentUrl);
        }

        /// <summary>
        /// 获取url参数ReturnUrl，并跳转到该地址，如果该地址不存在，在跳转到默认地址
        /// </summary>
        /// <returns></returns>
        public virtual void RedirectToReturnUrl(string defaultUrl)
        {
            string url = GetUrlString(UrlPara_ReturnUrl);
            if (url.IsNullOrEmpty() || !url.StartsWith("http://"))
            {
                HttpContext.Current.Response.Redirect(defaultUrl);
            }
            else
            {
                HttpContext.Current.Response.Redirect(url);
            }
        }
        #endregion

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            SaveLog();
            if (ShowCustomErrorMsg)
                ShowError();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (EnablePageExpires)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore();
            }
        }
        


    }
}
