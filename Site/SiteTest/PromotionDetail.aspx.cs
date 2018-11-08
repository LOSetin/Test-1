using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class PromotionDetail : SiteBase
{
   
    protected List<V_Promotion_GroupInfo> GetPromotionGroups()
    {
        return PromotionBLL.SelectPromotionGroups(GetUrlInt("PromotionID"));
    }

    protected void ShowDetail(int lineID)
    {
        List<LineDetailInfo> details = LineBLL.GetLineDetails(lineID);
        ltDetail.Text = "";
        foreach (LineDetailInfo model in details)
        {
            string title = "<p style='color:#333333;font-weight:bold;font-size:14px'>第" + model.SortOrder + "天：" + model.Title + "</p>";
            string pic = "<p>" + SiteUtility.ShowAllImage(model.DayPics, 150, 150) + "</p>";
            string content = "<p>" + model.DayDesc.NewLineCharToBr() + "</p>";
            string notice = "<p>温馨提示：" + model.WarmTips + "</p>";
            ltDetail.Text += title + pic + content + notice + "<hr/>";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowTitle("促销----促销团");
        if(!Page.IsPostBack)
        {
           
        }
    }
}