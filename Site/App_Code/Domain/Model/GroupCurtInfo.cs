using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 团的简要信息，包括出发日期，价格，剩余团位
/// </summary>
public class GroupCurtInfo
{
    /// <summary>
    /// 出发日期
    /// </summary>
    public string Date { set; get; }
    
    /// <summary>
    /// 价格
    /// </summary>
    public string Price { set; get; }

    /// <summary>
    /// 剩余团位
    /// </summary>
    public string Pos { set; get; }
}