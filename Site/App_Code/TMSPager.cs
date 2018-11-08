using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TMSPager 的摘要说明
/// </summary>
public static class TMSPager
{
    /// <summary>
    /// 获取记录总数
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public static int GetRecordCount(string tableName,string strWhere)
    {
        string sql = "select count(*) from " + tableName + " Where " + strWhere;
        return Convert.ToInt32(TMS.Db.Helper.ExecuteScalar(sql));
    }

    /// <summary>
    /// 显示分页
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageCount"></param>
    /// <param name="buttonCount"></param>
    /// <param name="recordCount"></param>
    /// <param name="urlQuery"></param>
    /// <returns></returns>
    public static string Show(int pageIndex, int pageSize, int pageCount, int buttonCount,int recordCount, string urlQuery)
    {

        int PageCount = pageCount;
        if (pageIndex > PageCount) pageIndex = PageCount;
        int ButtonCount = buttonCount;

        string pageinfo = "<span class=\"page-num\"> {0}条/页  共{1}条</span>";
        pageinfo = string.Format(pageinfo, pageSize,recordCount);

        pageinfo+= "<ul class=\"pagination-list\">";

        //定义格式
        string Firstpage = string.Format("<li class= \"active\"><a title='首页' class='myfirst'  href=\"?p=1{0}\" >&nbsp;</a></li>", urlQuery);
        string LastPage = string.Format("<li class= \"active\"><a title='末页' class='mylast'  href=\"?p={0}{1}\" >&nbsp;</a></li>", PageCount, urlQuery);

        string NextPage = "<li class= \"active\"><a title='下一页' class=\"next {2}\" href=\"?p={0}{1}\" >&nbsp;</a></li>";
        string PreviousPage = "<li class= \"active\"><a title='上一页' class=\"prev {2}\" href=\"?p={0}{1}\" >&nbsp;</a></li>";
        string NumPage= "<li><a class=\"page-link\" href=\"?p={0}{1}\">{0}</a></li>";
        string CurrentPage = "<li><a class=\"page-link current\" href=\"?p={0}{1}\">{0}</a></li>";

        //左右显示个数
        int n = ButtonCount / 2;
        int StartPage = pageIndex - n;
        int EndPage = pageIndex + n;

        PreviousPage = pageIndex > 1 ? string.Format(PreviousPage, (pageIndex - 1).ToString(), urlQuery, "") : string.Format(PreviousPage, "1", urlQuery, "disabled-prev");
        NextPage = pageIndex + 1 <= PageCount ? string.Format(NextPage, (pageIndex + 1).ToString(), urlQuery,"") : string.Format(NextPage, PageCount.ToString(), urlQuery, "disabled-next");

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

        pageinfo += Firstpage + PreviousPage;

        for (int i = StartPage; i <= EndPage; i++)
        {
            if (i != pageIndex)
                pageinfo += string.Format(NumPage, i, urlQuery);
            else
                pageinfo += string.Format(CurrentPage, i, urlQuery);
        }

        pageinfo += NextPage + LastPage;

        pageinfo += "</ul>";


        return pageinfo;
    }

    /// <summary>
    /// 获得分页总数
    /// </summary>
    /// <param name="recordCount"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static int CaculatePageCount(int pageSize, int recordCount)
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

        return pCount;
    }

}