using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework.Extensions;

/// <summary>
/// LineSearch 的摘要说明
/// </summary>
public static class LineSearch
{
    private static object getUrlValue(string name)
    {
        return HttpContext.Current.Request.QueryString[name];
    }

    /// <summary>
    /// 从url获得查找条件实体
    /// </summary>
    /// <returns></returns>
    public static LineSearchInfo GetSearchInfoFromUrl()
    {
        LineSearchInfo model = new LineSearchInfo();
        model.FirstID = getUrlValue("fid").ToArrowInt();
        model.SecondID = getUrlValue("sid").ToArrowInt();
        model.Days = getUrlValue("days").ToArrowInt();
        model.IsPickup = getUrlValue("pickup").ToArrowInt();
        model.KeyWord = getUrlValue("keyword").ToArrowString();
        model.FixPrice = getUrlValue("fprice").ToArrowInt();
        model.MinCustomPrice = getUrlValue("cprice1").ToArrowDecimal();
        model.MaxCustomPrice = getUrlValue("cprice2").ToArrowDecimal();
        model.MinGoDate = getUrlValue("cdate1").ToArrowDateTime(GlobalSetting.MinTime);
        model.MaxGoDate = getUrlValue("cdate2").ToArrowDateTime(GlobalSetting.MinTime);
        model.NewSort = getUrlValue("newsort").ToArrowInt();
        model.PriceSort = getUrlValue("pricesort").ToArrowInt();
        model.SellSort = getUrlValue("sellsort").ToArrowInt();

        return model;
    }

    /// <summary>
    /// 将查询实体转化成url查询参数，此时分页参数为1
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string CreateQueryParas(LineSearchInfo model)
    {
        string para = "?p=1";
        para += "&fid=" + model.FirstID;
        para += "&sid=" + model.SecondID;
        para += "&days=" + +model.Days;
        para += "&pickup=" + model.IsPickup;
        para += "&keyword=" + HttpContext.Current.Server.UrlEncode(model.KeyWord);
        para += "&fprice=" + model.FixPrice;
        para += "&cprice1=" + model.MinCustomPrice;
        para += "&cprice2=" + model.MaxCustomPrice;
        para += "&cdate1=" + (model.MinGoDate <= GlobalSetting.MinTime ? "" : model.MinGoDate.ToDateOnlyString());
        para += "&cdate2=" + (model.MaxGoDate <= GlobalSetting.MinTime ? "" : model.MaxGoDate.ToDateOnlyString());
        para += "&newsort=" + model.NewSort;
        para += "&pricesort=" + model.PriceSort;
        para += "&sellsort=" + model.SellSort;
        return para;
    }

    /// <summary>
    /// 将查询实体转化成url查询参数，此时分页参数为1，并提交查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static void GoSearch(LineSearchInfo model)
    {
        string para = CreateQueryParas(model);
        HttpContext.Current.Response.Redirect("Line.aspx" + para);
    }

    /// <summary>
    /// 生成where字句
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string CreateFilter(LineSearchInfo model)
    {
        string filter = "IsDel=0";
        if (model.FirstID > 0)
            filter += " And FirstCatID = " + model.FirstID;
        if (model.SecondID > 0)
            filter += " And SecondCatID=" + model.SecondID;

        if (model.Days > 0 && model.Days < 10)
            filter += " And TravelDays=" + model.Days;
        else if (model.Days >= 10)
            filter += " And TravelDays>10";

        if (model.IsPickup == 1)
            filter += " And IsPickup=1";
        else if (model.IsPickup == -1)
            filter += " And IsPickup=0";

        if (!string.IsNullOrEmpty(model.KeyWord.Trim()))
            filter += " And Name like '%" + model.KeyWord + "%'";

        if (model.MinGoDate > GlobalSetting.MinTime && model.MaxGoDate > GlobalSetting.MinTime)
        {
            filter += " And ID In(Select LineID From TravelGroup Where IsDel=0 And GoDate>='" + model.MinGoDate.ToString() + "' And GoDate<'" + model.MaxGoDate.AddDays(1).ToString() + "')";
        }

        if(model.MinCustomPrice>0 || model.MaxCustomPrice>0)
        {
            if (model.MinCustomPrice > 0)
                filter += " And MinPrice>=" + model.MinCustomPrice;
            if (model.MaxCustomPrice > 0)
                filter += " And MinPrice<" + model.MaxCustomPrice;
        }
        else
        {
            //如果自定义价格无值，则按固定价格区间
            if (model.FixPrice == 1)
                filter += " And MinPrice<=500";
            else if (model.FixPrice == 2)
                filter += " And MinPrice>500 And MinPrice<=1000";
            else if (model.FixPrice == 3)
                filter += " And MinPrice>1000 And MinPrice<=2000";
            else if (model.FixPrice == 4)
                filter += " And MinPrice>2000 And MinPrice<=4000";
            else if (model.FixPrice == 5)
                filter += " And MinPrice>4000 And MinPrice<=6000";
            else if (model.FixPrice == 6)
                filter += " And MinPrice>6000";
        }

        return filter;
    }

    /// <summary>
    /// 获得orderby子句
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string CreateSort(LineSearchInfo model)
    {
        string orderBy = "";
        if (model.NewSort == 1)
            orderBy = "ID desc";
        else if(model.NewSort==-1)
            orderBy = "ID asc";

        if (model.PriceSort == 1)
            orderBy = "MinPrice desc";
        else if(model.PriceSort==-1)
            orderBy = "MinPrice asc";

        if (model.SellSort == 1)
            orderBy = "IsHot desc";
        else if(model.SellSort==-1)
            orderBy = "IsHot asc";

        if (orderBy.IsNullOrEmpty())
            orderBy = "ID desc";

        return orderBy;
    }

}