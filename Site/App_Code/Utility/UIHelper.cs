using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework.Extensions;
using TMS;

/// <summary>
/// UIHelper 的摘要说明
/// </summary>
public static class UIHelper
{
    /// <summary>
    /// 显示版权
    /// </summary>
    /// <returns></returns>
    public static string ShowCopyright()
    {
        return new TMS.SiteSetting().Select(1).Copyright;
    }

    /// <summary>
    /// 根据订单状态，显示订单状态条
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static string ShowOrderStatusBar(string status,string statusHistory)
    {
        string result = "<div class='title-bar1 yahei1'><ul><li>提交订单</li><li {0}>编辑参团人</li><li {1}>等待商家确认</li><li {2}>等待支付</li><li {3}>交易完成</li></ul></div>";
        string cls = "class='tj'";
        string a="", b="", c="", d = "";
        if (status == OrderStatus.Canceled)
        {
            string[] step = statusHistory.Split('|');
            result = "<div class='title-bar1 yahei1'><ul>{0}</ul></div>";
            string allSteps = "";
            for (int i=0;i<step.Length;i++)
            {
                if(!step[i].IsNullOrEmpty())
                {
                    allSteps = allSteps + "<li>" + step[i] + "</li>";
                }
            }
            result = string.Format(result, allSteps);
        }
        else
        {
            if (status == OrderStatus.Submited)
                a = cls;
            else if (status == OrderStatus.WaitingConfirm)
                b = cls;
            else if (status == OrderStatus.WaitingPay)
                c = cls;
            else if (status == OrderStatus.Finished)
                d = cls;
            result = string.Format(result, a, b, c, d);
        }

        return result;
    }

     /// <summary>
     /// 根据订单状态显示订单操作按钮
     /// </summary>
     /// <param name="orderNum">订单号</param>
     /// <param name="orderStatus">订单状态</param>
     /// <param name="returnUrl">返回的url</param>
     /// <returns></returns>
    public static string ShowOrderButton(string orderNum,string orderStatus,string returnUrl)
    {
        string buttonFormat = "<a {3} href = '{0}?OrderNum=" + orderNum + "&ReturnUrl=" + returnUrl + "{1}' class='button'>{2}</a>";
        string cancelButton = string.Format(buttonFormat, "MemberAction.aspx", "&target=Order&action=Cancel", "取消订单", "onclick = \"return confirm('确定取消订单吗？')\"");
        string result = "";
        if(orderStatus==OrderStatus.Submited)
        {
            result = string.Format(buttonFormat, "MemberOrderPeopleEdit.aspx", "", "编辑参团人","");
            result += "<br/>";
            result += cancelButton;
        }
        else if(orderStatus == OrderStatus.WaitingConfirm)
        {
            result += cancelButton;
        }
        else if(orderStatus==OrderStatus.WaitingPay)
        {
            result = string.Format(buttonFormat, "Pay.aspx", "", "我要支付", "onclick = \"return confirm('确定支付吗？')\"");
            result += "<br/>";
            result += cancelButton;
        }

        return result;
    }

    /// <summary>
    /// 显示商品分类
    /// </summary>
    /// <returns></returns>
    public static string ShowGoodCats()
    {
        int cid = HttpContext.Current.Request["cid"].ToArrowInt();
        string result = "";
        List<GoodsCatInfo> goodCats = new GoodsCat().SelectList("IsDel=0");
        string format = "<li {2}><a href='{0}'>{1}</a></li>";

        foreach (var model in goodCats)
        {
            string curStyle = model.ID == cid ? "class=\"cur\"" : "";
            result += string.Format(format, "Mall.aspx?cid=" + model.ID, model.Name, curStyle);
        }

        return result;
    }

    /// <summary>
    /// 显示热词
    /// </summary>
    /// <returns></returns>
    public static string ShowHotWords()
    {
        int count = 0;
        string format= "<a href='UrlRouter.aspx?type=Search&keywords={0}'>{1}</a>";
        string result = "";
        SiteSettingInfo setting = new TMS.SiteSetting().Select(1);
        if(setting!=null)
        {
            string[] arr = setting.HotSearchWords.Split('|');
            for(int i=0;i<arr.Length;i++)
            {
                if(!arr[i].IsNullOrEmpty())
                {
                    result += string.Format(format, HttpContext.Current.Server.UrlEncode(arr[i]), arr[i]);
                    count = count + 1;
                }
                if (count >= 10)
                    break;
            }
        }

        return result;
    }

}