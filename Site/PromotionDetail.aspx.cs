using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;

public partial class PromotionDetail : UIBase
{
    protected List<TMS.V_Promotion_GroupInfo> GetGroups(string condition, string orderBy, int pageIndex, int pageSize)
    {
        return new TMS.V_Promotion_Group().SelectList(condition, orderBy, pageIndex, pageSize);
    }



    /// <summary>
    /// 显示原价，现价
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected string ShowPrice(TMS.V_Promotion_GroupInfo model)
    {
        string format = "<p class='black font-n16'>原价：￥{0}</p><p class='red font-s16'><b>现价：￥{1}</b></p>";
        string result = "";
        if(model.PromotionType==PromotionType.Discount)
        {
            decimal oPrice1 = model.RawOuterPrice;
            decimal nPrice1 = decimal.Round(model.RawOuterPrice * model.Discount, 0);
            result = string.Format(format, oPrice1, nPrice1);
        }
        else if(model.PromotionType==PromotionType.Bundle)
        {
            decimal oPrice2 = model.RawOuterPrice * model.TotalPayOneTimeJoinNum;
            decimal nPrice2 = model.TotalPayOneTime;
            result = string.Format(format, oPrice2, nPrice2);
        }
        return result;
    }

    protected string ShowBuyNum(TMS.V_Promotion_GroupInfo model)
    {
        string num = "1";
        if (model.PromotionType == PromotionType.Bundle)
            num = model.TotalPayOneTimeJoinNum.ToString();

        return num;
    }

    protected TMS.PromotionInfo GetPromotion()
    {
        return new TMS.Promotion().Select(GetUrlInt("PromotionID"));
    }

    protected TMS.PromotionInfo CurrentPromotion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentPromotion = GetPromotion();
        if(CurrentPromotion == null || CurrentPromotion.IsDel==1 || CurrentPromotion.EndTime<DateTime.Now)
        {
            Response.Write("<script>alert('该促销已过期！');history.go(-1);</script>");
            Response.End();
        }
    }
}