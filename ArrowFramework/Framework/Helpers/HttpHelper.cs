using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Arrow.Framework
{
    /// <summary>
    /// http相关处理
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// url编码
        /// </summary>
        /// <param name="val">需要编码的值</param>
        /// <param name="encode">编码。如gb2312，utf8等</param>
        /// <returns></returns>
        public static string UrlEncode(string val,string encode)
        {
            return HttpUtility.UrlEncode(val,Encoding.GetEncoding(encode));
        }

        #region 获取客户端IP
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string ip = "0.0.0.0";
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return ip;
        }
        #endregion

        #region 获得当前地址
        /// <summary>
        /// 获取网站根目录url
        /// </summary>
        /// <returns></returns>
        public static string GetRootURI()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;
                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在Web站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }

        /// <summary>
        /// 获得当前去除主机名端口以及查询串后的字符串。
        /// 例如，能获得 http://www.abc.com:1234/catalog/shownew.htm?date=today
        /// 中的/catalog/shownew.htm
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrlAbsolutePath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }

        /// <summary>
        /// 获得当前url中查询参数的部分。
        /// 例如，能获得 http://www.abc.com:1234/catalog/shownew.htm?date=today
        /// 中的?date=today。如果url包含查询参数，则返回的结果中包含?
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrlQueryString()
        {
            return HttpContext.Current.Request.Url.Query;
        }

        /// <summary>
        /// 获得当前除去主机名端口后的地址，例如能获得
        /// http://www.abc.com:1234/catalog/shownew.htm?date=today
        /// 中的/catalog/shownew.htm?date=today
        /// </summary>
        public static string GetCurrentUrlPathAndQuery()
        {
            return HttpContext.Current.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// 获得当前完整的url地址，包括以http开头的所有信息。
        /// 例如，能获得http://www.abc.com:1234/catalog/shownew.htm?date=today完整的信息。
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrlFullPath()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }
        #endregion

        #region 获得远程内容
        /// <summary>
        /// 获得远程html
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="encode">编码utf-8,gb2312等</param>
        /// <param name="timeout">超时，单位秒</param>
        /// <returns></returns>
        public static string GetRemoteHtml(string url,string encode,int timeout)
        {
            string strHtml = string.Empty;
            StreamReader strReader = null;
            HttpWebResponse wrpContent = null;
            try
            {
                HttpWebRequest wrqContent = (HttpWebRequest)WebRequest.Create(url);
                wrqContent.Timeout = timeout*1000;
                wrpContent = (HttpWebResponse)wrqContent.GetResponse();
                if (wrpContent.StatusCode != HttpStatusCode.OK)
                {
                    strHtml = "无法获得远程数据！";
                }
                if (wrpContent != null)
                {
                    strReader = new StreamReader(wrpContent.GetResponseStream(), Encoding.GetEncoding(encode));
                    strHtml = strReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                strHtml = e.Message;
            }
            finally
            {
                if (strReader != null)
                    strReader.Close();
                if (wrpContent != null)
                    wrpContent.Close();
            }
            return strHtml;
        }

        /// <summary>
        /// 使用Get方法获得远程数据
        /// </summary>
        /// <param name="url">提交的url地址</param>
        /// <param name="encode">获得内容的编码，gb231，gbk，utf-8等</param>
        /// <returns></returns>
        public static string GetContent(string url, string encode)
        {
            string val = "";
            WebRequest req = HttpWebRequest.Create(url);
            req.Method = "get";
            using (WebResponse rsp = req.GetResponse())
            {
                
                using (StreamReader sr = new StreamReader(rsp.GetResponseStream(), Encoding.GetEncoding(encode)))
                {
                    val = sr.ReadToEnd();
                }
            }
            return val;
        }

        /// <summary>
        /// 使用Post方式获得远程数据
        /// </summary>
        /// <param name="url">提交的url地址</param>
        /// <param name="param">不带?的url参数串，比如id=1&amp;t=abc相关参数需自行编码</param>
        /// <param name="encode">获得内容的编码，gb2312，utf8等</param>
        /// <returns></returns>
        public static string GetContentByPost(string url, string param, string encode)
        {
            string val = "";
            byte[] bs = Encoding.ASCII.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "post";
            req.ContentType = "application/x-www-form-urlencoded";
            req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; zh-CN; rv:1.9.1.4) Gecko/20091016 Firefox/3.5.4 (.NET CLR 2.0.50727)";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (WebResponse rsp = req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(rsp.GetResponseStream(), Encoding.GetEncoding(encode)))
                {
                    val = sr.ReadToEnd();
                }
            }
            return val;
        }
        #endregion

        /// <summary>
        /// 判断当前页面是否通过直接在浏览器输入地址访问 
        /// </summary>
        /// <returns></returns>
        public static bool UrlTypeFromBrowser()
        {
            string httpReferer = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string serverName = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            if ((httpReferer != null) && (httpReferer.IndexOf(serverName) == 7))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 发送smtp邮件
        /// </summary>
        /// <param name="server">smtp服务器,如smtp.qq.com</param>
        /// <param name="sender"></param>
        /// <param name="pwd"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="receiver"></param>
        public static bool SendMail(string server, string sender, string pwd, string subject, string content, string receiver)
        {
            SmtpClient client = new SmtpClient(server);   
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network; 
            client.Credentials = new NetworkCredential(sender, pwd); 

            MailMessage mmsg = new MailMessage(new MailAddress(sender), new MailAddress(receiver)); 
            mmsg.Subject = subject;      
            mmsg.SubjectEncoding = Encoding.UTF8;  
            mmsg.Body = content;       
            mmsg.BodyEncoding = Encoding.UTF8;   
            mmsg.IsBodyHtml = true;   
            mmsg.Priority = MailPriority.High;   
            try
            {
                client.Send(mmsg);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
