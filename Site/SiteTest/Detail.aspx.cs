using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;

public partial class SiteTest_Detail : SiteBase
{
    protected LineInfo MyLine = null;

    protected List<TravelGroupInfo> MyGroups = new List<TravelGroupInfo>();

    protected TravelGroupInfo CurrentGroup = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowTitle("线路详细信息");
        MyLine = LineBLL.SelectLine(GetUrlInt("LineID"));
        MyGroups = GroupBLL.GetLineGroups(GetUrlInt("LineID"));
        if (!Page.IsPostBack)
        {
            BindDropDownList();
            BindData();
        }
    }

    protected void BindDropDownList()
    {
        ddlGroup.Items.Clear();
        foreach(var model in MyGroups)
        {
            ddlGroup.Items.Add(new ListItem(model.GoDate.ToDateOnlyString(), model.ID.ToString()));
        }
    }

    protected void BindData()
    {
        if (MyLine == null) return;
        ltNotice.Text = MyLine.SignUpNotice;
        ltDesc.Text = MyLine.LineDesc;
        ltImage.Text = SiteUtility.ShowImage(MyLine.CoverPath, 200, 200, true);
        ltLineName.Text = MyLine.Name;
        ltGoTravel.Text = MyLine.GoTravel;
        ltBackTravel.Text = MyLine.BackTravel;

        TravelGroupInfo group = MyGroups.Find(s => s.ID == ddlGroup.SelectedValue.ToArrowInt());
        if (group == null) return;
        ltBackDate.Text = group.BackDate.ToDateOnlyString();
        ltPrice.Text = CurrentMember == null ? group.OuterPrice.ToString() : group.InnerPrice.ToString();
        ltNum.Text = group.TotalNum.ToString();
        ltRemain.Text = group.RemainNum.ToString();
        ltTime.Text = group.GatheringTime;
        ltPlace.Text = group.GatheringPlace;
      

        List<LineDetailInfo> details = LineBLL.GetLineDetails(MyLine.ID);
        ltDetail.Text = "";
        foreach(LineDetailInfo model in details)
        {
            string title = "<p style='color:red;font-weight:bold;font-size:14px'>第"+ model.SortOrder+"天："+ model.Title+"</p>";
            string pic = "<p>" + SiteUtility.ShowAllImage(model.DayPics, 150, 150) + "</p>";
            string content = "<p>" +  model.DayDesc.NewLineCharToBr()+"</p>";
            string notice ="<p>温馨提示："+ model.WarmTips+"</p>";
            ltDetail.Text += title + pic + content + notice + "<hr/>";
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("Order.aspx?PromotionID=0&LineID=" + GetUrlString("LineID") + "&GroupID=" + ddlGroup.SelectedValue.ToArrowString());
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {

    }

    protected void btnShare_Click(object sender, EventArgs e)
    {

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        
    }
}