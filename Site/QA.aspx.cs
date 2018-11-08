using Arrow.Framework;
using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

public partial class QA : UIBase
{
    protected string ShowQA(List<TMS.QAInfo> allItems,string cat)
    {
        var items = allItems.FindAll(s => s.CatName == cat);
        string css = "no";
        string urlCat = Request.QueryString["cat"];
        if (cat == urlCat)
            css = "no";
        else
            css = "none";
        string format1 = @"<div class='list-item {0}'>{1}</div>";
        string format2 = @"<p id='J_nav_{1}'><a href='QA.aspx?cat={0}&ID={1}'>{2}</a></p>";
        string result2 = "";
        foreach(var model in items)
        {
            result2 = result2 + string.Format(format2, Server.UrlEncode(cat), model.ID, model.QATitle);
        }
        string result1 = string.Format(format1, css, result2);


        return result1;
    }

    protected TMS.QAInfo CurrentQA = new TMS.QA().Select(HttpContext.Current.Request.QueryString["ID"].ToArrowInt());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(CurrentQA==null)
        {
            List<TMS.QAInfo> qaList = new TMS.QA().SelectList(1, "1=1", "ID asc", null);
            if (qaList.Count == 1)
            {
                CurrentQA = qaList[0];
                Response.Redirect("QA.aspx?cat=" + Server.UrlEncode(CurrentQA.CatName) + "&ID=" + CurrentQA.ID);
            }
            else
            {
                //没有任何内容，跳转到首页
                Response.Redirect("Default.aspx");
            }
        }
        

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }
}