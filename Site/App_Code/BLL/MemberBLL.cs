using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS;
using Arrow.Framework;
using System.Data.Common;

/// <summary>
/// MemberBLL 的摘要说明
/// </summary>
public static class MemberBLL
{
    private static readonly SiteMember dal = new SiteMember();
    private static readonly MemberFav favDal = new MemberFav();
    private static readonly CostHistory costDal = new CostHistory();
    private static readonly IArrowUserStatus<MemberInfo> currentMember = new ArrowWebCookieStatus<MemberInfo>();
    private static readonly string memberKey = "KEY_TMS_MEMBER";

    #region 基本方法
    public static int AddMember(SiteMemberInfo model)
    {
        return dal.Add(model);
    }

    public static int UpdateMember(SiteMemberInfo model)
    {
        return dal.Update(model);
    }

    public static SiteMemberInfo Select(string userName)
    {
        return dal.Select(userName);
    }

    public static SiteMemberInfo Select(string userName,DbTransaction tran)
    {
        return dal.Select(userName, tran);
    }

    public static int Update(SiteMemberInfo model)
    {
        return dal.Update(model);
    }

    public static int Update(SiteMemberInfo model,DbTransaction tran)
    {
        return dal.Update(model, tran);
    }

    public static int AddCostHistory(CostHistoryInfo model,DbTransaction tran)
    {
        return costDal.Add(model, tran);
    }

    #endregion

    #region 设置登录信息
    public static void SetLoginInfo(MemberInfo mi , bool remember=false)
    {
        //如果记住登陆，则记住一个月，否则，关闭浏览器就自动退出
        if (remember)
            currentMember.SetValue(memberKey, mi, 60 * 24 * 30);
        else
        currentMember.SetValue(memberKey, mi);
    }

    public static MemberInfo GetLoginInfo()
    {
        return currentMember.GetValue(memberKey);
    }


    public static void LogOut()
    {
        currentMember.Remove(memberKey);
    }

    public static int AddFav(MemberFavInfo model)
    {
        return favDal.Add(model);
    }

    public static int DelFav(string userName, string favType, string objID)
    {
        return favDal.DeleteList("UserName='" + userName + "' And FavType='" + favType + "' And FavObjID='" + objID + "'");
    }

   

    #endregion

    /// <summary>
    /// 加密密码
    /// </summary>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public static string Encrypt(string pwd)
    {
        return EncryptHelper.MD5Encrypt(pwd);
    }

    /// <summary>
    /// 获得某用户的某个线路的收藏
    /// </summary>
    /// <param name="lineID"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static List<MemberFavInfo> SelectLineFavs(int lineID,string userName)
    {
        return favDal.SelectList("FavType='" + FavType.Line + "' And UserName='" + userName + "' And FavObjID='" + lineID + "'");
    }

    /// <summary>
    /// 获得某用户的线路收藏
    /// </summary>
    /// <param name="lineID"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static List<MemberFavInfo> SelectLineFavs(string userName)
    {
        return favDal.SelectList("FavType='" + FavType.Line + "' And UserName='" + userName + "'");
    }

    /// <summary>
    /// 更新登陆信息
    /// </summary>
    /// <param name="userName"></param>
    public static void UpdateLoginInfo(string userName)
    {
        var model = MemberBLL.Select(userName);
        if (model == null) return;
        MemberInfo mi = new MemberInfo();
        mi.UserName = model.UserName;
        mi.RealName = model.RealName;
        mi.InviteNum = model.InviteNum;
        mi.Phone = model.MobileNum;
        SetLoginInfo(mi);
    }

    /// <summary>
    /// 兑换商品
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="goodsID"></param>
    /// <param name="goodsNum"></param>
    /// <returns></returns>
    public static bool ExchangeGoods(string userName,int goodsID,int goodsNum,string address,string realName,string phone, out string msg)
    {
        msg = "";
        bool success = false;
      
        //判断点数是否足够
        //判断商品数量是否足够
        //商品数量减去购买数量
        //用户已使用点数增加
        //写入消费记录
        using (DbConnection conn = Db.Helper.CreateConnection())
        {
            conn.ConnectionString = Db.Helper.ConnectionString;
            conn.Open();
            DbTransaction tran = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                var user = Select(userName, tran);
                if (user == null)
                {
                    msg = "用户不存在！";
                    return false;
                }
                var goods = GoodsBLL.SelectGoods(goodsID, tran);
                if (goods == null)
                {
                    msg = "商品不存在！";
                    return false;
                }
                if (goods.Num <= 0)
                {
                    msg = "商品已被兑换完！";
                    return false;
                }
                if (goods.Num < goodsNum)
                {
                    msg = "商品库存不足！";
                    return false;
                }
                int remainPoints = user.TotalPoints - user.UsedPoints;
                int pointsCost = goods.Points * goodsNum;
                if (remainPoints < pointsCost)
                {
                    msg = "点数不足！";
                    return false;
                }

                goods.Num = goods.Num - goodsNum;
                user.UsedPoints = user.UsedPoints + pointsCost;

                CostHistoryInfo model = new CostHistoryInfo();
                model.AddTime = DateTime.Now;
                model.CostType = CostType.Exchange;
                model.GoodsID = goodsID;
                model.GoodsName = goods.Name;
                model.GoodsNum = goodsNum;
                model.MoneyAfter = user.TotalCost;
                model.MoneyBefore = user.TotalCost;
                model.OrderNum = "";
                model.MoneyCost = 0M;
                model.PointsAfter = user.TotalPoints - user.UsedPoints;
                model.PointsBefore = remainPoints;
                model.PointsCost = pointsCost;
                model.ExchangeStatus = ExchangeStatus.WaitingSend;
                model.SendTime = GlobalSetting.MinTime;
                model.FinishTime = GlobalSetting.MinTime;
                model.ExpressName = "";
                model.ExpressNum = "";

                model.LinkAddress = address;
                model.LinkMan = realName;
                model.LinkPhone = phone;

                model.Remarks = "";
                model.UserName = userName;

                GoodsBLL.UpdateGoods(goods, tran);
                MemberBLL.Update(user, tran);
                MemberBLL.AddCostHistory(model, tran);

                tran.Commit();

                msg = "操作成功！";
                success = true;
            }
            catch(Exception ex)
            {
                tran.Rollback();
                msg = ex.Message;
                success = false;
            }


        }



        return success;
    }

    /// <summary>
    /// 获得用户可用积分
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static int GetMemberPoints(string memberName)
    {
        int points = 0;
        var member = Select(memberName);
        if (member != null)
            points = member.TotalPoints - member.UsedPoints;
        return points;
    }

    /// <summary>
    /// 获取会员消费总额
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static decimal GetMemberTotalCost(string memberName)
    {
        decimal result = 0M;

        var member = Select(memberName);
        if (member != null)
            result = member.TotalCost;

        return result;
    }

}