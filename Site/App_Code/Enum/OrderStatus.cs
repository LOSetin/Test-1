using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 订单状态
/// </summary>
public class OrderStatus
{
 
    /// <summary>
    /// 已提交等待编辑参团人
    /// </summary>
    public static readonly string Submited = "等待编辑参团人";

    /// <summary>
    /// 等待商家确认
    /// </summary>
    public static readonly string WaitingConfirm = "等待商家确认";

    /// <summary>
    /// 商家已确认，等待支付
    /// </summary>
    public static readonly string WaitingPay = "等待支付";

    /// <summary>
    /// 已完成
    /// </summary>
    public static readonly string Finished = "已完成";

    /// <summary>
    /// 已取消
    /// </summary>
    public static readonly string Canceled = "已取消";

}