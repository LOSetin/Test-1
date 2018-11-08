using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// QACat 的摘要说明
/// </summary>
public static class QACat
{
    /// <summary>
    /// 网上支付问题
    /// </summary>
    public static readonly string QANetPay = "网上支付问题";

    /// <summary>
    /// 旅游常见概念解释
    /// </summary>
    public static readonly string QATravelConcept = "旅游常见概念解释";

    /// <summary>
    /// 签证相关问题
    /// </summary>
    public static readonly string QASign = "签证相关问题";

    /// <summary>
    /// 问答分类列表
    /// </summary>
    public static readonly List<string> QACats = new List<string>() { QANetPay,QATravelConcept,QASign };

    /// <summary>
    /// 绑定到下拉
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="showAll">是否在第一个位置添加 所有 项</param>
    public static void BindToDrowdownList(DropDownList ddl,bool showAll=false)
    {
        ddl.Items.Clear();
        foreach(string s in QACats)
        {
            ddl.Items.Add(new ListItem(s, s));
        }
        if (showAll)
            ddl.Items.Insert(0, new ListItem("所有", "所有"));

    }

}