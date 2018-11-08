using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS;

/// <summary>
/// PromotionBLL 的摘要说明
/// </summary>
public class PromotionBLL
{
    private static readonly Promotion dal = new Promotion();
    private static readonly PromotionGroup pgDal = new PromotionGroup();
    private static readonly V_Promotion_Group vpgDal = new V_Promotion_Group();

    #region 基本方法
    public static int Add(PromotionInfo model)
    {
        return dal.Add(model);
    }

    public static int Update(PromotionInfo model)
    {
        return dal.Update(model);
    }

    public static PromotionInfo Select(int id)
    {
        return dal.Select(id);
    }

    public static int AddGroupToPromotion(PromotionGroupInfo model)
    {
        return pgDal.Add(model);
    }

    public static int UpdateGroupOfPromotion(PromotionGroupInfo model)
    {
        return pgDal.Update(model);
    }

    public static PromotionGroupInfo SelectGroupOfPromotion(int id)
    {
        return pgDal.Select(id);
    }

    public static int DelGroupOfPromotion(int autoID)
    {
        return pgDal.Delete(autoID);
    }

    /// <summary>
    /// 更新某促销团的促销团位总数
    /// </summary>
    /// <param name="totalNum">新的总数</param>
    /// <param name="id">PromotionGroup中的自增ID</param>
    /// <returns></returns>
    public static int UpdateTotalNumOfPromotionGroup(int totalNum,int id)
    {
        string sql = "Update PromotionGroup Set TotalNum=" + totalNum + " Where ID=" + id;
        return Db.Helper.ExecuteNonQuery(sql);
    }

    #endregion

    /// <summary>
    /// 设置促销删除状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDel"></param>
    /// <returns></returns>
    public static int SetIsDel(int id, int isDel)
    {
        string sql = "Update Promotion Set IsDel=" + isDel + " Where ID=" + id;
        return Db.Helper.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 显示促销详细信息
    /// </summary>
    /// <param name="promotionType"></param>
    /// <param name="discount"></param>
    /// <param name="totalPay"></param>
    /// <param name="joinNum"></param>
    /// <returns></returns>
    public static string ShowPromotionDetail(string promotionType,string discount,string totalPay,string joinNum)
    {
        string result = "";
        if(promotionType==PromotionType.Discount)
        {
            result = "折扣率：" + discount;
        }else if(promotionType==PromotionType.Bundle)
        {
            result = totalPay + "任选" + joinNum + "个团位";
        }

        return result;
    }

    /// <summary>
    /// 获得所有有效的促销，未删除，促销开始时间小于当前时间，促销结束时间大于当前时间
    /// </summary>
    /// <returns></returns>
    public static List<PromotionInfo> GetAllValidPromotion()
    {
        return dal.SelectList("IsDel=0 And StartTime<='" + DateTime.Now.ToString() + "' And EndTime>='" + DateTime.Now.ToString() + "'");
    }

    /// <summary>
    /// 判断某团是否在某促销中
    /// </summary>
    /// <param name="groupID"></param>
    /// <param name="promotionID"></param>
    /// <returns></returns>
    public static bool GroupIsInPromotion(int groupID,int promotionID)
    {
        int count = pgDal.GetCount("GroupID=" + groupID + " And PromotionID=" + promotionID);
        return count > 0 ? true : false;
    }

    public static List<V_Promotion_GroupInfo> SelectPromotionGroups(int promotionID)
    {
        return vpgDal.SelectList("PromotionID=" + promotionID);
    }

    public static decimal CaculatePromotionPrice(decimal rawPrice,string promotionType,decimal discount,decimal totalPayOneTime,int totalJoinOneTime)
    {
        decimal price = rawPrice;
        if (promotionType == PromotionType.Discount)
            price = decimal.Round(rawPrice * discount, 0);
        else if (promotionType == PromotionType.Bundle)
            price = totalPayOneTime;

        return price;
    }

}