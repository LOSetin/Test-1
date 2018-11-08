using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using System.Text;

public partial class UILine : UIBase
{
    /// <summary>
    /// 获得当前搜索实体
    /// </summary>
    protected LineSearchInfo CurrentSearchInfo
    {
        get { return LineSearch.GetSearchInfoFromUrl(); }
    }

    protected List<TMS.LineInfo> GetLines(string condition,string orderBy,int pageIndex,int pageSize)
    {
        return LineBLL.SelectLineList(condition, orderBy, pageIndex, pageSize);
    }

    /// <summary>
    /// 获得最近发团日期
    /// </summary>
    /// <param name="lineID"></param>
    /// <returns></returns>
    protected TMS.TravelGroupInfo GetLastGroup(int lineID)
    {
        return LineBLL.GetLastGroup(lineID);

    }

    protected void BindFirstCat()
    {
        ddlFirstCat.Items.Clear();
        List<TMS.LineCatInfo> cats= LineBLL.SelectCatList("IsDel=0 And ParentID=0 Order By SortOrder");
        ddlFirstCat.DataSource = cats;
        ddlFirstCat.DataTextField = "Name";
        ddlFirstCat.DataValueField = "ID";
        ddlFirstCat.DataBind();
        ddlFirstCat.Items.Insert(0, new ListItem("不限", "0"));
        ddlFirstCat.SelectedValue = GetUrlString("fid");
    }

    protected void BindSecondCat()
    {
        int fid = ddlFirstCat.SelectedValue.ToArrowInt();
        if(fid>0)
        {
            ddlSecondCat.DataSource = LineBLL.SelectCatList("IsDel=0 And ParentID=" + fid + " Order By SortOrder");
            ddlSecondCat.DataTextField = "Name";
            ddlSecondCat.DataValueField = "ID";
            ddlSecondCat.DataBind();
        }
        ddlSecondCat.Items.Insert(0, new ListItem("不限", "0"));
        ddlSecondCat.SelectedValue = GetUrlString("sid");
    }


    /// <summary>
    /// 生成旅游天数的链接
    /// </summary>
    /// <returns></returns>
    protected string CreateDaysSearchLink()
    {
        string link = "<a href='{0}' class='{1}'>{2}</a>";
        LineSearchInfo model = CurrentSearchInfo;

        StringBuilder sb = new StringBuilder();
        for(int i=0;i<=10;i++)
        {
            string title = i + "天";
            if (i == 0) title = "不限";
            if (i == 10)
                title += "以上";

            model.Days = i;
            string href = LineSearch.CreateQueryParas(model);
            string reqVal = Request.QueryString["days"];
            if (reqVal.IsNullOrEmpty()) reqVal = "0";
            string cls = reqVal == i.ToString() ? "selected" : "";
            sb.AppendLine(string.Format(link, href, cls, title));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 生成接送的搜索连接
    /// </summary>
    /// <param name="value"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    protected string CreatePickupSearchLink(string value,string title)
    {
        string link = "<a href='{0}' class='{1}'>{2}</a>";
        LineSearchInfo model = CurrentSearchInfo;
        model.IsPickup = value.ToArrowInt();

        string href = LineSearch.CreateQueryParas(model);
        string reqVal = Request.QueryString["pickup"];
        if (reqVal.IsNullOrEmpty()) reqVal = "0";
        string cls = reqVal == value ? "selected" : "";
        link = string.Format(link, href, cls, title);

        return link;
    }

    /// <summary>
    /// 生成单价区间的搜索链接
    /// </summary>
    /// <param name="value"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    protected string CreateFixPriceSearchLink(string value,string title)
    {
        string link = "<a href='{0}' class='{1}'>{2}</a>";
        LineSearchInfo model = CurrentSearchInfo;
        model.FixPrice = value.ToArrowInt();
        model.MinCustomPrice = 0M;
        model.MaxCustomPrice = 0M;
        string href = LineSearch.CreateQueryParas(model);
        string reqVal = Request.QueryString["fprice"];
        if (reqVal.IsNullOrEmpty()) reqVal = "0";
        string cls = reqVal == value ? "selected" : "";
        link = string.Format(link, href, cls, title);
        return link;
    }

    /// <summary>
    /// 最新发布
    /// </summary>
    /// <returns></returns>
    protected string CreateNewSort()
    {
        string downArrow = "sort-category-2";
        string upArrow = "sort-category-1";
        string doubleArrow = "sort-category-3";
        LineSearchInfo model = CurrentSearchInfo;
        int newSort = Request.QueryString["newsort"].ToArrowInt();
        string css = doubleArrow;
        if (newSort == 0)
        {
            css = doubleArrow;
            model.NewSort = 1;
            model.PriceSort = 0;
            model.SellSort = 0;
        }
        else if (newSort == 1)
        {
            css = downArrow;
            model.NewSort = -1;
            model.PriceSort = 0;
            model.SellSort = 0;
        }
        else if (newSort == -1)
        {
            css = upArrow;
            model.NewSort = 1;
            model.PriceSort = 0;
            model.SellSort = 0;
        }

        string href = LineSearch.CreateQueryParas(model);
        string link = "<a class='sort-item {0}' href='{1}'>最新发布</a>";

        link = string.Format(link, css, href);

        return link;
}

    /// <summary>
    /// 热销
    /// </summary>
    /// <returns></returns>
    protected string CreateSellSort()
    {
        string downArrow = "sort-category-2";
        string upArrow = "sort-category-1";
        string doubleArrow = "sort-category-3";
        LineSearchInfo model = CurrentSearchInfo;
        int sellSort = Request.QueryString["sellsort"].ToArrowInt();
        string css = doubleArrow;
        if (sellSort == 0)
        {
            css = doubleArrow;
            model.SellSort = 1;
            model.PriceSort = 0;
            model.NewSort = 0;
        }
        else if (sellSort == 1)
        {
            css = downArrow;
            model.SellSort = -1;
            model.PriceSort = 0;
            model.NewSort = 0;
        }
        else if (sellSort == -1)
        {
            css = upArrow;
            model.SellSort = 1;
            model.PriceSort = 0;
            model.NewSort = 0;
        }

        string href = LineSearch.CreateQueryParas(model);
        string link = "<a class='sort-item {0}' href='{1}'>热销</a>";

        link = string.Format(link, css, href);

        return link;
    }

    /// <summary>
    /// 价格
    /// </summary>
    /// <returns></returns>
    protected string CreatePriceSort()
    {
        string downArrow = "sort-category-2";
        string upArrow = "sort-category-1";
        string doubleArrow = "sort-category-3";
        LineSearchInfo model = CurrentSearchInfo;
        int priceSort = Request.QueryString["pricesort"].ToArrowInt();
        string css = doubleArrow;
        if (priceSort == 0)
        {
            css = doubleArrow;
            model.PriceSort = 1;
            model.SellSort = 0;
            model.NewSort = 0;
        }
        else if (priceSort == 1)
        {
            css = downArrow;
            model.PriceSort = -1;
            model.SellSort = 0;
            model.NewSort = 0;
        }
        else if (priceSort == -1)
        {
            css = upArrow;
            model.PriceSort = 1;
            model.SellSort = 0;
            model.NewSort = 0;
        }

        string href = LineSearch.CreateQueryParas(model);
        string link = "<a class='sort-item {0}' href='{1}'>价格</a>";

        link = string.Format(link, css, href);

        return link;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        BindFirstCat();
        BindSecondCat();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void ddlFirstCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        LineSearchInfo model = CurrentSearchInfo;
        model.FirstID = ddlFirstCat.SelectedValue.ToArrowInt();
        model.SecondID = 0;
        LineSearch.GoSearch(model);
    }

    protected void ddlSecondCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        LineSearchInfo model = CurrentSearchInfo; 
        model.SecondID = ddlSecondCat.SelectedValue.ToArrowInt();
        LineSearch.GoSearch(model);
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string keyword = Request.Form["keyword"].ToArrowString().Trim();
        DateTime cdate1 = Request.Form["cdate1"].ToArrowDateTime(GlobalSetting.MinTime);
        DateTime cdate2 = Request.Form["cdate2"].ToArrowDateTime(GlobalSetting.MinTime);
        decimal cprice1 = Request.Form["cprice1"].ToArrowDecimal();
        decimal cprice2 = Request.Form["cprice2"].ToArrowDecimal();

        LineSearchInfo model = CurrentSearchInfo;
        if (cprice1 > 0 || cprice2 > 0)
            model.FixPrice = 0;
        model.KeyWord = keyword;
        model.MinGoDate = cdate1;
        model.MaxGoDate = cdate2;
        model.MaxCustomPrice = cprice2;
        model.MinCustomPrice = cprice1;

        LineSearch.GoSearch(model);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        LineSearchInfo model = CurrentSearchInfo;
        model.KeyWord = "";
        model.MinGoDate = GlobalSetting.MinTime;
        model.MaxGoDate = GlobalSetting.MinTime;
        model.MinCustomPrice = 0M;
        model.MaxCustomPrice = 0M;

        LineSearch.GoSearch(model);
    }
}