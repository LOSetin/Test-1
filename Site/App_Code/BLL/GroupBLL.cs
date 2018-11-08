using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework.Extensions;
using TMS;

/// <summary>
/// GroupBLL 的摘要说明
/// </summary>
public class GroupBLL
{
    private static readonly TravelGroup dal = new TravelGroup();

    #region 基本方法

    public static int AddGroup(TravelGroupInfo model)
    {
        return dal.Add(model);
    }

    public static int UpdateGroup(TravelGroupInfo model)
    {
        return dal.Update(model);
    }

    public static TravelGroupInfo SelectGroup(int id)
    {
        return dal.Select(id);
    }

    #endregion

    /// <summary>
    /// 提取团的简要信息，包括出发日期，价格，剩余团位
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public static GroupCurtInfo GroupToCurt(TravelGroupInfo group)
    {
        GroupCurtInfo curt = new GroupCurtInfo();
        curt.Date = group.GoDate.ToDateOnlyString();
        curt.Price = group.OuterPrice.ToString();
        curt.Pos = group.RemainNum.ToString();
        return curt;
    }

    public static int SetGroupIsDel(int id, int isDel)
    {
        string sql = "update TravelGroup Set IsDel=" + isDel + " Where ID=" + id;
        return Db.Helper.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 获取某线路最近一次发团
    /// </summary>
    /// <param name="lineID"></param>
    /// <returns></returns>
    public static TravelGroupInfo GetRecentlyGroup(int lineID)
    {
        string date = DateTime.Now.ToString();
        List<TravelGroupInfo> list = dal.SelectList(1, "IsDel=0 And LineID=" + lineID + " And GoDate>'" + date + "'", "GoDate asc");
        if (list.Count == 1)
            return list[0];
        else return null;
    }

    /// <summary>
    /// 获取某线路所有未出发的团
    /// </summary>
    /// <param name="lineID"></param>
    /// <returns></returns>
    public static List<TravelGroupInfo> GetLineGroups(int lineID)
    {
        return dal.SelectList("IsDel=0 And LineID="+lineID+" And GoDate>'"+DateTime.Now.ToString()+"'");
    }

}