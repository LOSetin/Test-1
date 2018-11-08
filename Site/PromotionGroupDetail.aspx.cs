using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using TMS;

public partial class PromotionGroupDetail : UIBase
{
    protected LineInfo CurrentLine = null;
    //同一线路，并且做同一促销的团
    protected List<V_Promotion_GroupInfo> PromotionsGroups = null;

    //当前促销团的ID
    protected int CurrentGroupID = HttpContext.Current.Request["GroupID"].ToArrowInt();
    protected V_Promotion_GroupInfo CurrentGroup = null;

    protected string JsonInfo()
    {
        //获取同一线路，并且做促销的团
        List<GroupCurtInfo> list = new List<GroupCurtInfo>();
        foreach(V_Promotion_GroupInfo group in PromotionsGroups)
        {
            GroupCurtInfo model = new GroupCurtInfo();
            model.Date = group.GoDate.ToDateOnlyString();
            model.Pos = group.TotalNum.ToString();
            model.Price = PromotionBLL.CaculatePromotionPrice(group.RawOuterPrice, group.PromotionType, group.Discount, group.TotalPayOneTime, group.TotalPayOneTimeJoinNum).ToString();
            list.Add(model);
        }
       return Arrow.Framework.JsonHelper.JsonSerializer(list);
    }

    /// <summary>
    /// 显示原价，现价
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected string ShowPrice(TMS.V_Promotion_GroupInfo model)
    {
        string format = "<span style='color:red;font-weight:bold;font-size:16px;'>￥{0}</span>&nbsp;&nbsp;&nbsp;&nbsp;原价：<span class='black font-n16'>￥{1}</span>";
        string result = "";
        if (model.PromotionType == PromotionType.Discount)
        {
            decimal oPrice1 = model.RawOuterPrice;
            decimal nPrice1 = decimal.Round(model.RawOuterPrice * model.Discount, 0);
            result = string.Format(format, nPrice1, oPrice1);
        }
        else if (model.PromotionType == PromotionType.Bundle)
        {
            decimal oPrice2 = model.RawOuterPrice * model.TotalPayOneTimeJoinNum;
            decimal nPrice2 = model.TotalPayOneTime;
            result = string.Format(format, nPrice2, oPrice2);
        }
        return result;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentLine = LineBLL.SelectLine(GetUrlInt("LineID"));
        if (CurrentLine == null)
            Response.Redirect("Default.aspx");
        else
        {
            // 获取同一线路，并且做促销的团
             PromotionsGroups = new V_Promotion_Group().SelectList("IsDel=0 And PromotionID="+GetUrlInt("PromotionID") +" And LineID=" + CurrentLine.ID+" Order By GoDate");
            //绑定团期
            ddlGoDate.Items.Clear();
            foreach(V_Promotion_GroupInfo group in PromotionsGroups)
            {
                ddlGoDate.Items.Add(new ListItem(group.GoDate.ToDateOnlyString(), group.GroupID.ToString()));
            }
            ddlGoDate.SelectedValue = CurrentGroupID.ToString();
            CurrentGroup = PromotionsGroups.Find(s => s.GroupID == ddlGoDate.SelectedValue.ToArrowInt());
            if(CurrentGroup==null)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}