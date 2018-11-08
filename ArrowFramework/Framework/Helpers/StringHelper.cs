using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Arrow.Framework
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public static class StringHelper
    {
        #region 返回时间格式yyyyMMddHHmmssfff的字符串
        /// <summary>
        /// 返回时间格式yyyyMMddHHmmssfff的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeSeq()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
        #endregion

        #region 过滤重复字符串
        /// <summary>
        /// 过滤重复字符串
        /// </summary>
        /// <param name="strSource">需要过滤的字符串</param>
        /// <param name="splitChar">分割符</param>
        /// <returns></returns>
        public static string RepeatFilter(string strSource, char splitChar)
        {
            string _str = string.Empty;
            string[] strArray = strSource.Split(splitChar);
            for (int i = 0; i < strArray.Length; i++)
            {
                for (int j = i + 1; j < strArray.Length; j++)
                {
                    if (strArray[i] != string.Empty && strArray[j] != string.Empty)
                    {
                        if (strArray[j] == strArray[i])
                        {
                            strArray[j] = string.Empty;
                        }
                    }
                }
                if (strArray[i] != string.Empty)
                    _str += strArray[i] + splitChar.ToString();
            }
            if (_str.LastIndexOf(splitChar.ToString()) > -1)
                _str = _str.Substring(0, _str.Length - 1);
            return _str;
        }
        #endregion

        #region 将字符串进行HTML安全编码
        /// <summary>
        /// 将字符串进行HTML安全编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEncode(string str)
        {

            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("'", "&acute;");
            str = str.Replace("\"", "&quot;");
            return str;
        }
        #endregion

        #region 将字符串进行HTML解码
        /// <summary>
        ///将字符串进行HTML解码，主要针对TextBox等
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlDecode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&amp;", "&");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&acute;", "'");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        #endregion

        #region 过滤威胁标记
        /// <summary>
        /// 去除有威胁的标记，获取干净的输入值
        /// </summary>
        /// <param name="text">用户输入</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>输出安全的字符串</returns>
        public static string RemoveDirtyString(string text , int maxLen=20)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return "";

            if (text.Length > 20)
                text = text.Substring(0, 20);

            //去掉所有标记
            text = Regex.Replace(text, "<(.|\\n)*?>", "");
            //去掉单引号
            text = text.Replace("'", "");
            //去掉空格
            text = Regex.Replace(text, @"\s", ""); 

            return text;
        }
        #endregion

        #region 获得body之间的所有内容
        /// <summary>
        /// 获得body之间的所有内容
        /// </summary>
        /// <param name="strWholeHtml"></param>
        /// <returns></returns>
        public static string GetBodyHtml(string strWholeHtml)
        {
            string strRegex = @"(?m)<body[^>]*>(\w|\W)*?</body[^>]*>";
            string strResult = string.Empty;
            Match matchText = Regex.Match(strWholeHtml, strRegex, RegexOptions.IgnoreCase);
            return  matchText.Groups[0].Value;
        }
        #endregion

        #region 去除所有HTML标记，脚本
        ///   <summary>   
        ///   去除HTML标记   
        ///   </summary>   
        ///   <param name="strHtml">包括HTML的源码</param>   
        ///   <returns>已经去除后的文字</returns>   
        public static string RemoveHtml(string strHtml)
        {
            string[] aryReg ={   
                  @"<script[^>]*?>.*?</script>|<script>[\s\S]*?</script>|<script[^>]*>([\s\S](?!<script))*?</script>",  
                  @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",   
                  @"([\r\n])[\s]+",   
                  @"&(quot|#34);",   
                  @"&(amp|#38);",   
                  @"&(lt|#60);",   
                  @"&(gt|#62);",     
                  @"&(nbsp|#160);",     
                  @"&(iexcl|#161);",   
                  @"&(cent|#162);",   
                  @"&(pound|#163);",   
                  @"&(copy|#169);",   
                  @"&#(\d+);",   
                  @"-->",   
                  @"<!--.*\n"   
                };

            string[] aryRep =   {   
                    "",   
                    "",   
                    "",   
                    "\"",   
                    "&",   
                    "<",   
                    ">",   
                    "   ",   
                    "\xa1",//chr(161),   
                    "\xa2",//chr(162),   
                    "\xa3",//chr(163),   
                    "\xa9",//chr(169),   
                    "",   
                    "",   
                    ""   
                  };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            return strOutput;
        }
        #endregion

        #region 过滤脚本和iframe
        /// <summary>
        /// 过滤脚本和iframe
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string RemoveJsIframe(string strHtml)
        {
            string[] aryReg ={   
                  @"<*script[^>]*?>.*?</*script*>", 
                  @"<\s*script",
                  @"<*iframe[^>]*?>.*?</*iframe*>",
                  @"<\s*iframe",                
                };

            string[] aryRep =   {   
                    "", 
                    "",
                    "",
                    ""
                  };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            return strOutput;
        }
        #endregion

        #region 过滤脚本和图片和iframe
        /// <summary>
        /// 过滤脚本和图片和iframe,只保留p,br
        /// </summary>
        /// <param name="strHtml">需要处理的html字符串</param>
        /// <returns></returns>
        public static string RemoveJsImgIframe(string strHtml)
        {
            string[] aryReg ={   
                  @"<*script[^>]*?>.*?</*script*>", 
                  @"<\s*script",
                  @"<*iframe[^>]*?>.*?</*iframe*>",
                  @"<\s*iframe",
                  @"\<img[^\>]+\>", 
                };

            string[] aryRep =   {   
                    "", 
                    "",
                    "",
                    "",
                    ""
                  };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
         
            return strOutput;
        }
        #endregion

        #region 去除所有html标签,只保留p,br
        /// <summary>
        /// 去除所有html标签,只保留p,br
        /// </summary>
        /// <param name="strHtml">需要处理的html字符串</param>
        /// <returns></returns>
        public static string ToSimpleHtml(string strHtml)
        {
            //只留p和br
            string strOutput = "";
            Regex reg = new Regex(@"<((/?p|/?br)|[^\s>]+)[^>]*>");
            strOutput = reg.Replace(strHtml, delegate(Match m) { if (m.Groups[2].Success) return "<" + m.Groups[1].Value + ">"; return ""; });
            return strOutput;
        }
        #endregion

        #region 去除所有html标签,只保留p,br,a
        /// <summary>
        /// 去除所有html标签,只保留p,br,a
        /// </summary>
        /// <param name="strHtml">需要处理的html字符串</param>
        /// <returns></returns>
        public static string ToSimpleHtmlWithLink(string strHtml)
        {
            string strOutput = "";
            //只留p,br,a
            strOutput = Regex.Replace(strHtml, @"<((/?p|/?br)|(/?a)|[^\s>]+)[^>]*>", delegate(Match m)
           {
               if (m.Groups[3].Success)
                   return "<" + m.Groups[1].Value + " " + Regex.Match(m.Value, @"(?i)href=(['""\s]?)[^'""\s]+\1") + ">";
               if (m.Groups[2].Success)
                   return "<" + m.Groups[1].Value + ">";
               return "";
           });
            return strOutput;
        }
        #endregion

        #region 长文章分页

        private static string GetUrl()
        {
            string url = HttpContext.Current.Request.RawUrl;
            //此处是防止wap手机不支持session而将session写在url上构造
            //if (HttpContext.Current.Session["Member"] != null)
            //{
            //    HttpBrowserCapabilities sbText = HttpContext.Current.Request.Browser;
            //    string s = sbText.Cookies.ToString();
            //    if (s.ToLower() == "false")
            //    {
            //        url = "/(S(" + HttpContext.Current.Session.SessionID + "))/" + url;
            //    }
            //}
            return url.Split('?')[0];
        }

        /// <summary>
        /// 长文章分页 
        /// </summary>
        /// <param name="p_strContent">去除额外标记后的内容</param>
        /// <param name="m_intPageSize">每页显示多少个字符</param>
        /// <param name="urlQuery">查询参数串,比如detail.aspx?id=5&p=</param>
        /// <param name="lt">显示分页的literal控件</param>
        /// <returns></returns>

        private static string OutputBySize(string p_strContent, int m_intPageSize, string urlQuery, System.Web.UI.WebControls.Literal lt)
        {
            string m_strRet = "";
            //设置第一页为初始页
            int m_intCurrentPage = 1;
            int m_intTotalPage = 0;
            //文章长度
            int m_intArticlelength = p_strContent.Length;
            if (m_intPageSize < m_intArticlelength)
            {//如果每页大小大于文章长度时就不用分页了
                if (m_intArticlelength % m_intPageSize == 0)
                {//set total pages count
                    m_intTotalPage = m_intArticlelength / m_intPageSize;
                }
                else
                {//if the totalsize
                    m_intTotalPage = m_intArticlelength / m_intPageSize + 1;
                }
                if (HttpContext.Current.Request.QueryString["p"] != null)
                {//set Current page number
                    try
                    {//处理不正常的地址栏的值
                        m_intCurrentPage = Convert.ToInt32(HttpContext.Current.Request.QueryString["p"]);
                        if (m_intCurrentPage > m_intTotalPage)
                            m_intCurrentPage = m_intTotalPage;
                    }
                    catch
                    {
                        m_intCurrentPage = 1;
                    }
                }
                //set the page content 设置获取当前页的大小
                if (m_intCurrentPage < m_intTotalPage)
                {
                    m_intPageSize = m_intCurrentPage < m_intTotalPage ? m_intPageSize : (m_intArticlelength - m_intPageSize * (m_intCurrentPage - 1));
                    m_strRet += p_strContent.Substring(m_intPageSize * (m_intCurrentPage - 1), m_intPageSize);
                }
                else if (m_intCurrentPage == m_intTotalPage)
                {
                    int mm_intPageSize = m_intArticlelength - m_intPageSize * (m_intCurrentPage - 1);
                    m_strRet += p_strContent.Substring(m_intArticlelength - mm_intPageSize);
                }

                string m_strPageInfo = " <div class='Spacer'></div><div class='Spacer'></div><p style='text-align:center'>";
                for (int i = 1; i <= m_intTotalPage; i++)
                {
                    /***************只显示5页****************/
                    //显示的上限
                    int maxPageNum = m_intCurrentPage + 3 + (m_intCurrentPage == 1 ? 1 : 0);
                    int minPageNum = m_intTotalPage > maxPageNum ? (maxPageNum - 4) : (m_intTotalPage - 4);
                    if (i > maxPageNum || i < minPageNum)
                    {
                    }
                    else if (i == m_intCurrentPage)
                    {
                        m_strPageInfo += "<span style='color:Red;'>第" + i + "页</span>&nbsp;";
                    }
                    else
                        m_strPageInfo += "<a href='" + GetUrl() + urlQuery + i.ToString() + "' >第" + i + "页</a>&nbsp;";
                }
                m_strPageInfo += "</p>";
                //输出显示各个页码
                lt.Text = m_strPageInfo;
                //HttpContext.Current.Response.Write(m_strPageInfo);
            }
            else
            {
                m_strRet += p_strContent;
            }
            return m_strRet;
        }
        #endregion

        #region 判断一个字符串是否属于另一个字符串，如判断abc是否属于aa,bb,cc，按","分割
        /// <summary>
        /// 判断一个字符串是否属于另一个字符串，如判断abc是否属于aa,bb,cc，按","分割
        /// </summary>
        /// <param name="strValidate">验证的字符串，如abc</param>
        /// <param name="strTarget">目标字符串，如aa,bb,cc</param>
        /// <param name="charSplit">分割的字符</param>
        /// <returns></returns>
        public static bool InSplitString(string strValidate, string strTarget, char charSplit)
        {
            bool blReturn = false;
            if (!string.IsNullOrEmpty(strTarget))
            {
                string[] strArr = strTarget.Split(charSplit);
                for (int i = 0; i < strArr.Length; i++)
                {
                    if (strArr[i] == strValidate)
                    {
                        blReturn = true;
                        break;
                    }
                }
            }
            return blReturn;
        }
        #endregion

        #region 从字符串中删除另一个字符串，如从aa,bb,cc删除bb，此字符串是以","分割
        /// <summary>
        /// 从字符串中删除另一个字符串，如从aa,bb,cc删除bb，此字符串是以","分割
        /// </summary>
        /// <param name="strRemove">需要删除的字符串，如bb</param>
        /// <param name="strTarget">目标字符串，如aa,bb,cc</param>
        /// <param name="charSplit">分割的字符</param>
        /// <returns></returns>
        public static string RemoveFromSplitString(string strRemove, string strTarget, char charSplit)
        {
            string[] strArr = strTarget.Split(charSplit);
            string strReturn = "";
            for (int i = 0; i < strArr.Length; i++)
            {
                if (strArr[i] != strRemove)
                {
                    strReturn += strArr[i] + charSplit.ToString();
                }
            }
            if (strReturn.EndsWith(charSplit.ToString()))
            {
                strReturn = strReturn.Substring(0, strReturn.Length - 1);
            }
            return strReturn;
        }
        #endregion

        #region 获得数字字母组合的随机字符串
        /// <summary>
        /// 获得随机字符串
        /// </summary>
        /// <param name="length">需要获得的字符串长度</param>
        /// <returns>随机字符串</returns>
        public static string GetRndString(int length)
        {
            Random random = new Random();
            string[] strArray = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(strArray[random.Next(strArray.Length)]);
            }
            return builder.ToString();
        }
        #endregion

        #region 字符串按字节截取
        /// <summary>
        /// 字符串长度(按字节算),英文一个字节,中文两个字节
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StrLength(string str)
        {
            int len = 0;
            byte[] b;

            for (int i = 0; i < str.Length; i++)
            {
                b = Encoding.Default.GetBytes(str.Substring(i, 1));
                if (b.Length > 1)
                    len += 2;
                else
                    len++;
            }

            return len;
        }

        /// <summary>
        /// 截取指定长度字符串(按字节算),英文一个字节,中文两个字节
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string StrCut(string str, int length)
        {
            int len = 0;
            byte[] b;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                b = Encoding.Default.GetBytes(str.Substring(i, 1));
                if (b.Length > 1)
                    len += 2;
                else
                    len++;

                if (len >= length)
                    break;

                sb.Append(str[i]);
            }

            return sb.ToString();
        }
        #endregion

        #region 汉字字符串转成首字母拼音,如您好转成NH
        /// <summary>
        /// 汉字字符串转成首字母拼音,如您好转成NH
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string ToChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// 单个汉字转拼音
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        private static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }
        #endregion

    }
}
