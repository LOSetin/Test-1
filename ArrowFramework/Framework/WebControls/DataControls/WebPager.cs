using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Arrow.Framework.Extensions;
using System.Web.UI;

namespace Arrow.Framework.WebControls
{
    public class WebPager
    {

        #region 私有方法
        /// <summary>
        /// 生成分页HTML，共12343条  第1/10页  首页 上页 1 2 3  4 下页 末页
        /// </summary>
        /// <returns></returns>
        private string CreatePageBar()
        {
            int PageCount = pageCount;
            if (pageIndex > PageCount) pageIndex = PageCount;
            int ButtonCount = buttonCount;

            string Firstpage = string.Format("<a href=?p={0}{1}>{2}</a>", "1", urlQuery, firstPageWord);
            string LastPage = string.Format("<a href=?p={0}{1}>{2}</a>", PageCount, urlQuery, lastPageWord);

            string pageinfo = "<span class='Tips'>共{0}条  第{1}/{2}页</span>" + Firstpage;
            pageinfo = string.Format(pageinfo, recordCount, pageIndex.ToString(), PageCount.ToString());

            //定义格式
            string NextPage = "<a href=?p={0}{1}>" + nextPageWord + "</a>";
            string PreviousPage = "<a  href=?p={0}{1}>" + prevPageWord + "</a>";
            string NumPage = "<a href=?p={0}{1}><span>{0}</span></a>";
            string CurrentPage = "<span class='Current'>{0}</span>";

            //左右显示个数
            int n = ButtonCount / 2;
            int StartPage = pageIndex - n;
            int EndPage = pageIndex + n;

            PreviousPage = pageIndex > 1 ? string.Format(PreviousPage, (pageIndex - 1).ToString(), urlQuery) : string.Format(PreviousPage, "1", urlQuery);
            NextPage = pageIndex + 1 <= PageCount ? string.Format(NextPage, (pageIndex + 1).ToString(), urlQuery) : string.Format(NextPage, PageCount.ToString(), urlQuery);

            if (EndPage > PageCount)
            {
                StartPage = (pageIndex + 1 - n) - (EndPage - PageCount);
                EndPage = PageCount;
            }
            if (StartPage < 1)
            {
                StartPage = 1;
                EndPage = ButtonCount > PageCount ? PageCount : ButtonCount;
            }

            pageinfo += PreviousPage;

            for (int i = StartPage; i <= EndPage; i++)
            {
                if (i != pageIndex)
                    pageinfo += string.Format(NumPage, i, urlQuery);
                else
                    pageinfo += " " + string.Format(CurrentPage, i) + " ";
            }

            pageinfo += NextPage;
            pageinfo += LastPage;

            return pageinfo;
        }

        /// <summary>
        /// 生成分页HTML，样式：首页 上页 1 2 3  4 下页 末页
        /// </summary>
        /// <returns></returns>
        private string CreateSimplePageBar()
        {
            int PageCount = pageCount;
            if (pageIndex > PageCount) pageIndex = PageCount;
            int ButtonCount = buttonCount;

            string Firstpage = string.Format("<a href=?p={0}{1}>{2}</a>", "1", urlQuery, firstPageWord);
            string LastPage = string.Format("<a href=?p={0}{1}>{2}</a>", PageCount, urlQuery, lastPageWord);

            string pageinfo = Firstpage;
            //string pageinfo = "<span class='Tips'>共{0}条  第{1}/{2}页</span>" + Firstpage;
            //pageinfo = string.Format(pageinfo, recordCount, pageIndex.ToString(), PageCount.ToString());

            //定义格式
            string NextPage = "<a href=?p={0}{1}>" + nextPageWord + "</a>";
            string PreviousPage = "<a  href=?p={0}{1}>" + prevPageWord + "</a>";
            string NumPage = "<a href=?p={0}{1}><span>{0}</span></a>";
            string CurrentPage = "<span class='Current'>{0}</span>";

            //左右显示个数
            int n = ButtonCount / 2;
            int StartPage = pageIndex - n;
            int EndPage = pageIndex + n;

            PreviousPage = pageIndex > 1 ? string.Format(PreviousPage, (pageIndex - 1).ToString(), urlQuery) : string.Format(PreviousPage, "1", urlQuery);
            NextPage = pageIndex + 1 <= PageCount ? string.Format(NextPage, (pageIndex + 1).ToString(), urlQuery) : string.Format(NextPage, PageCount.ToString(), urlQuery);

            if (EndPage > PageCount)
            {
                StartPage = (pageIndex + 1 - n) - (EndPage - PageCount);
                EndPage = PageCount;
            }
            if (StartPage < 1)
            {
                StartPage = 1;
                EndPage = ButtonCount > PageCount ? PageCount : ButtonCount;
            }

            pageinfo += PreviousPage;

            for (int i = StartPage; i <= EndPage; i++)
            {
                if (i != pageIndex)
                    pageinfo += string.Format(NumPage, i, urlQuery);
                else
                    pageinfo += " " + string.Format(CurrentPage, i) + " ";
            }

            pageinfo += NextPage;
            pageinfo += LastPage;

            return pageinfo;
        }

        /// <summary>
        /// 生成极简分页HTML，样式：上页  1/20 下页
        /// </summary>
        /// <returns></returns>
        private string CreateVerySimplePageBar()
        {
            if (pageIndex > pageCount) pageIndex = pageCount;

            string pageinfo = "";

            //定义格式
            string NextPage = "<a href=?p={0}{1}>" + nextPageWord + "</a>";
            string PreviousPage = "<a  href=?p={0}{1}>" + prevPageWord + "</a>";
            string CurrentPage = "<span class='Current'>{0}/{1}</span>";

            PreviousPage = pageIndex > 1 ? string.Format(PreviousPage, (pageIndex - 1).ToString(), urlQuery) : string.Format(PreviousPage, "1", urlQuery);
            NextPage = pageIndex + 1 <= pageCount ? string.Format(NextPage, (pageIndex + 1).ToString(), urlQuery) : string.Format(NextPage, pageCount.ToString(), urlQuery);
            CurrentPage = string.Format(CurrentPage, pageIndex, pageCount);

            pageinfo += PreviousPage;
            pageinfo += CurrentPage;
            pageinfo += NextPage;

            return pageinfo;
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 获取?后的url参数，排除参数p
        /// </summary>
        /// <returns></returns>
        public static  string GetUrlQuery()
        {
            string result = "";
            if (HttpContext.Current != null)
            {
                string query = HttpContext.Current.Request.Url.Query;
                if (!query.IsNullOrEmpty())
                {
                    query = query.Trim();
                    if (query.StartsWith("?"))
                    {
                        query = query.Substring(1);
                        string[] paras = query.Split('&');
                        foreach (string s in paras)
                        {
                            if (!s.StartsWith("p="))
                            {
                                result = result + "&" + s;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获得分页总数
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public void CaculatePageCount()
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            int pCount = 1;

            if (recordCount % pageSize == 0)
            {
                pCount = recordCount / pageSize;
            }
            else
            {
                pCount = recordCount / pageSize + 1;
            }

            if (pCount <= 0)
            {
                pCount = 1;
            }

            this.pageCount = pCount;
        }

        /// <summary>
        /// 显示分页
        /// </summary>
        /// <param name="pageStyle"></param>
        /// <param name="writer"></param>
        public  void ShowPager(HtmlTextWriter writer)
        {
            if (recordCount > 0)
            {
                switch (pageStyle)
                {
                    case ControlPageStyle.Simple:
                        writer.WriteLine("<div class=\"" + pageCSSName + "\">" + CreateSimplePageBar() + "</div>");
                        break;
                    case ControlPageStyle.Full:
                        writer.WriteLine("<div class=\"" + pageCSSName + "\">" + CreatePageBar() + "</div>");
                        break;
                    case ControlPageStyle.VerySimple:
                        writer.WriteLine("<div class=\"" + pageCSSName + "\">" + CreateVerySimplePageBar() + "</div>");
                        break;
                    default:
                        writer.WriteLine("<div class=\"" + pageCSSName + "\">" + CreatePageBar() + "</div>");
                        break;
                }
            }
        }

        #endregion

        #region 字段
        private int pageSize = 10; //每页要显示的记录的数目。
        private int pageIndex = 1; //要显示的页的索引,1表示第一页。
        private int recordCount = 0; //数据表中的记录总数。
        private int pageCount = 1;//分页总数
        private string urlQuery = GetUrlQuery(); //url参数
        private string pageCSSName = "gvPage";
        private int buttonCount = 10; //显示的页码数量
        private ControlPageStyle pageStyle = ControlPageStyle.Full;
        private string firstPageWord = "首页";
        private string lastPageWord = "末页";
        private string prevPageWord = "上页";
        private string nextPageWord = "下页";
        #endregion

        #region 属性
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { set { this.pageSize = value; } get { return this.pageSize; } }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { set { this.pageIndex = value; } get { return this.pageIndex; } }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount { set { this.recordCount = value; } get { return this.recordCount; } }

        /// <summary>
        /// 分页总数
        /// </summary>
        public int PageCount { set { this.pageCount = value; } get { return this.pageCount; } }

        /// <summary>
        /// 获取?后的url参数，排除参数p，p作为系统分页参数，不能在其他场合使用p
        /// </summary>
        public string UrlQuery { set { this.urlQuery = value; } get { return this.urlQuery; } }

        /// <summary>
        /// 分页CSS样式
        /// </summary>
        public string PageCSSName { set { this.pageCSSName = value; } get { return this.pageCSSName; } }

        /// <summary>
        /// 显示的按钮页码数
        /// </summary>
        public int ButtonCount { set { this.buttonCount = value; } get { return this.buttonCount; } }

        /// <summary>
        /// 分页的样式
        /// </summary>
        public ControlPageStyle PageStyle { set { this.pageStyle = value; } get { return this.pageStyle; } }

        /// <summary>
        /// 首页标识字符，可以是html
        /// </summary>
        public string FirstPageWord { set { this.firstPageWord = value; } get { return this.firstPageWord; } }

        /// <summary>
        /// 末页标识字符，可以是html
        /// </summary>
        public string LastPageWord { set { this.lastPageWord = value; } get { return this.lastPageWord; } }

        /// <summary>
        /// 上一页标识字符，可以是html
        /// </summary>
        public string PrevPageWord { set { this.prevPageWord = value; } get { return this.prevPageWord; } }

        /// <summary>
        /// 下一页标识字符，可以是html
        /// </summary>
        public string NextPageWord { set { this.nextPageWord = value; } get { return this.nextPageWord; } }
        #endregion

    }
}
