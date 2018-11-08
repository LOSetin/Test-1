using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LineSearchInfo
{
    /// <summary>
    /// 一级分类，0表示不限
    /// </summary>
    public int FirstID { set; get; }

    /// <summary>
    /// 二级分类，0表示不限
    /// </summary>
    public int SecondID { set; get; }

    /// <summary>
    /// 旅游天数，当为10时，表示>10
    /// </summary>
    public int Days { set; get; }

    /// <summary>
    /// 固定价格区间，0表示不限，1表示0-500，2表示500-1000，3表示1000-2000，4表示2000-4000，5表示4000-6000，6表示6000以上，当自定义价格的值存在时，此处值无效，为0
    /// </summary>
    public int FixPrice { set; get; }

    /// <summary>
    /// 自定义价格区间的最小值，如果自定义价格的值不为0，则覆盖固定价格区间
    /// </summary>
    public decimal MinCustomPrice { set; get; }

    /// <summary>
    /// 自定义价格区间的最大值，如果自定义价格的值不为0，则覆盖固定价格区间
    /// </summary>
    public decimal MaxCustomPrice { set; get; }

    /// <summary>
    /// 是否接送，1表示接送，0表示不限，-1表示不接送
    /// </summary>
    public int IsPickup { set; get; }

    /// <summary>
    /// 线路名称或编号
    /// </summary>
    public string KeyWord { set; get; }

    /// <summary>
    /// 出团日期的最小值
    /// </summary>
    public DateTime MinGoDate { set; get; }

    /// <summary>
    /// 出团日期的最大值
    /// </summary>
    public DateTime MaxGoDate { set; get; }

    /// <summary>
    /// 0表示不排序，1表示最新的在前，-1表示最新的在后
    /// </summary>
    public int NewSort { set; get; }

    /// <summary>
    /// 0表示不排序，1表示推荐的在前，-1表示推荐的在后
    /// </summary>
    public int SellSort { set; get; }

    /// <summary>
    /// 0表示不排序，1表示价格高的在前，-1表示价格高的在后
    /// </summary>
    public int PriceSort { set; get; }

}