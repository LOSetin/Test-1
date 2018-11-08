using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 积分兑换状态
/// </summary>
public class ExchangeStatus
{
    /// <summary>
    /// 用户点击了兑换，兑换成功，等待发货
    /// </summary>
    public static readonly string WaitingSend = "等待发货";

    /// <summary>
    /// 后台已发货
    /// </summary>
    public static readonly string WaitingReceive = "等待收货";

    /// <summary>
    /// 整个兑换成功
    /// </summary>
    public static readonly string Finish = "完成";
}