using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS;
using System.Data.Common;

/// <summary>
/// OrderBLL 的摘要说明
/// </summary>
public class OrderBLL
{
    private static readonly TravelOrder dal = new TravelOrder();
    private static readonly OrderDetail detailDal = new OrderDetail();
    private static readonly V_Order viewDal = new V_Order();

    public static int AddOrder(TravelOrderInfo model)
    {
        return dal.Add(model);
    }

    public static TravelOrderInfo SelectOrder(string orderNum)
    {
        return dal.Select(orderNum);
    }

    public static int UpdateOrder(TravelOrderInfo model)
    {
        return dal.Update(model);
    }

    public static int UpdateDetail(OrderDetailInfo model)
    {
        return detailDal.Update(model);
    }

    public static V_OrderInfo GetOrderView(string orderNum)
    {
        List<V_OrderInfo> orders = viewDal.SelectList("OrderNum='" + orderNum + "'");
        if (orders.Count == 1)
            return orders[0];
        return null;
    }

    /// <summary>
    /// 更新参团者，并提交订单
    /// </summary>
    /// <param name="orderNum"></param>
    /// <param name="memberUserName"></param>
    /// <param name="details"></param>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public static bool UpdateTeammateAndSubmit(string orderNum,string memberUserName, List<OrderDetailInfo> details,out string errorMsg)
    {
        errorMsg = "";
        bool isSuccess = false;
        using (DbConnection conn = Db.Helper.CreateConnection())
        {
            conn.ConnectionString = Db.Helper.ConnectionString;
            conn.Open();
            using (DbTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    foreach (OrderDetailInfo model in details)
                    {
                        detailDal.Update(model, tran);
                    }
                    string sql = "update TravelOrder Set OrderStatus='" + OrderStatus.WaitingConfirm + "',OrderHistory=OrderHistory+'" + OrderStatus.WaitingConfirm + "|' Where OrderNum='" + orderNum + "' And AddMemberName='" + memberUserName + "'";
                    Db.Helper.ExecuteNonQuery(tran, sql);
                    tran.Commit();
                    isSuccess = true;
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    errorMsg = ex.Message;
                }
            }
        }
        return isSuccess;
    }

    /// <summary>
    /// 设置订单状态，不适用于确认订单
    /// </summary>
    /// <param name="orderNum"></param>
    /// <param name="memberUserName"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public static int UpdateOrderStatus(string orderNum, string memberUserName, string status)
    {
        if (OrderStatus.WaitingConfirm == status)
            return 0;
        string sql = "update TravelOrder Set OrderStatus='" + status + "',OrderHistory=OrderHistory+'"+status+"|' Where OrderNum='" + orderNum + "' And AddMemberName='" + memberUserName + "'";
        return Db.Helper.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 确认订单
    /// </summary>
    /// <param name="orderNum"></param>
    /// <param name="memberUserName"></param>
    /// <param name="currentAdmin"></param>
    /// <returns></returns>
    public static int ConfirmOrder(string orderNum,string memberUserName,LoginInfo currentAdmin)
    {
        string sql = "update TravelOrder Set OrderStatus='" + OrderStatus.WaitingPay + "',OrderHistory=OrderHistory+'"+OrderStatus.WaitingPay+"|',OperatorUserName='"+currentAdmin.UserName+ "',OperatorRealName='"+currentAdmin.RealName+"'  Where OrderNum='" + orderNum + "' And AddMemberName='" + memberUserName + "'";
        return Db.Helper.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 设置支付按钮可见状态
    /// </summary>
    /// <param name="orderStatus"></param>
    /// <returns></returns>
    public static bool SetPayButtonVisible(string orderStatus)
    {
        bool result = false;
        if (OrderStatus.WaitingPay == orderStatus)
            result = true;
        return result;
    }

    /// <summary>
    /// 设置编辑参团者的按钮的可见状态
    /// </summary>
    /// <param name="orderStatus"></param>
    /// <returns></returns>
    public static bool SetEditButtonVisible(string orderStatus)
    {
        bool result = false;
        if (orderStatus == OrderStatus.Submited)
            result = true;
        return result;
    }

    /// <summary>
    /// 设置提交按钮可见状态
    /// </summary>
    /// <param name="orderStatus"></param>
    /// <returns></returns>
    public static bool SetSubmitButtonVisible(string orderStatus)
    {
        bool result = false;
        if (orderStatus == OrderStatus.Submited)
            result = true;
        return result;
    }

    /// <summary>
    /// 设置取消按钮可见状态
    /// </summary>
    /// <param name="orderStatus"></param>
    /// <returns></returns>
    public static bool SetCancelButtonVisible(string orderStatus)
    {
        bool result = false;
        if (orderStatus != OrderStatus.Finished && orderStatus!=OrderStatus.Canceled)
            result = true;
        return result;
    }

    /// <summary>
    /// 设置确认订单按钮可见状态，后台管理员按钮
    /// </summary>
    /// <param name="orderStatus"></param>
    /// <returns></returns>
    public static bool SetConfirmButtonVisible(string orderStatus)
    {
        bool result = false;
        if (orderStatus == OrderStatus.WaitingConfirm)
            result = true;
        return result;
    }

    /// <summary>
    /// 获得某个订单的所有详细信息
    /// </summary>
    /// <param name="orderNum"></param>
    /// <param name="memberUserName"></param>
    /// <returns></returns>
    public static List<OrderDetailInfo> GetAllDetails(string orderNum,string memberUserName)
    {
        return detailDal.SelectList("OrderNum='" + orderNum + "' And AddMemberName='" + memberUserName + "'");
    }



    /// <summary>
    /// 快速添加订单，以及根据购买的数量构造虚拟单
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tran"></param>
    public static void FastAddOrder(TravelOrderInfo model,DbTransaction tran)
    {
        dal.Add(model, tran);
        for(int i=0;i<model.BuyNum;i++)
        {
            var detail = CreateVirtualOrderDetail(model.AddMemberName, model.OrderNum, model.GroupID, model.LineID);
            detailDal.Add(detail, tran);
        }
    }

    /// <summary>
    /// 生成虚拟的订单详细信息
    /// </summary>
    /// <param name="addMemberName"></param>
    /// <param name="orderNum"></param>
    /// <param name="groupID"></param>
    /// <param name="lineID"></param>
    /// <returns></returns>
    public static OrderDetailInfo CreateVirtualOrderDetail(string addMemberName, string orderNum,int groupID,int lineID)
    {
        OrderDetailInfo model = new OrderDetailInfo();
        model.AddTime = DateTime.Now;
        model.GroupID = groupID;
        model.IDNum = "";
        model.LineID = lineID;
        model.MobileNum = "";
        model.OrderNum = orderNum;
        model.RealName = "";
        model.Remarks = "";
        model.Sex = "";
        model.AddMemberName = addMemberName;
        return model;
    }

    /// <summary>
    /// 支付成功后的处理，如果不是返回success，则表示处理失败，将记录在日志中
    /// </summary>
    /// <param name="orderNum"></param>
    /// <param name="memberUserName"></param>
    /// <param name="moneyPayed"></param>
    /// <returns></returns>
    public static bool PaySuccessHandler(string orderNum,string memberUserName,decimal moneyPayed,out string msg)
    {
        //支付成功后的处理
        //更改订单的已支付额
        //更改订单的状态为完成
        //增加用户的点数
        //记录用户的消费记录

        msg = "";
        bool success = false;
        int points = Convert.ToInt32(moneyPayed);
        string orderSql = "Update TravelOrder Set OrderStatus='" + OrderStatus.Finished + "',MoneyPayed=" + moneyPayed + " Where OrderNum='" + orderNum + "' And AddMemberName='" + memberUserName + "'";

        using (DbConnection conn = Db.Helper.CreateConnection())
        {
            conn.ConnectionString = Db.Helper.ConnectionString;
            conn.Open();
            using (DbTransaction tran = conn.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    SiteMemberInfo member = MemberBLL.Select(memberUserName, tran);
                    if (member == null)
                    {
                        msg = "用户不存在！";
                        return false;
                    }

                    decimal oldTotalCost = member.TotalCost;
                    decimal newTotalCost = oldTotalCost + moneyPayed;
                    int oldTotalPoints = member.TotalPoints;
                    int newTotalPoints = oldTotalPoints + points;

                    member.TotalCost = newTotalCost;
                    member.TotalPoints = newTotalPoints;

                    CostHistoryInfo model = new CostHistoryInfo();
                    model.AddTime = DateTime.Now;
                    model.CostType = CostType.JoinGroup;
                    model.GoodsID = 0;
                    model.GoodsName = "";
                    model.GoodsNum = 0;
                    model.MoneyCost = moneyPayed;
                    model.OrderNum = orderNum;
                    model.PointsAfter = newTotalPoints;
                    model.PointsBefore = oldTotalPoints;
                    model.MoneyBefore = oldTotalCost;
                    model.MoneyAfter = newTotalCost;
                    model.PointsCost = points;
                    model.ExchangeStatus = "";
                    model.SendTime = GlobalSetting.MinTime;
                    model.FinishTime = GlobalSetting.MinTime;
                    model.ExpressName = "";
                    model.ExpressNum = "";
                    model.LinkAddress = "";
                    model.LinkMan = "";
                    model.LinkPhone = "";
                    model.Remarks = "";
                    model.UserName = memberUserName;

                    Db.Helper.ExecuteNonQuery(tran, orderSql);
                    MemberBLL.Update(member, tran);
                    MemberBLL.AddCostHistory(model, tran);

                    tran.Commit();
                    success = true;
                }
                catch(Exception ex)
                {
                    msg = ex.Message;
                    tran.Rollback();
                }
            }

        }
      
        


        return success;
    }

    /// <summary>
    /// 获取会员未处理订单
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static int GetNotFinishOrderCount(string memberName)
    {
        string sql = "Select count(*) from TravelOrder Where AddMemberName='"+memberName+"' And OrderStatus in ('" + OrderStatus.Submited + "','" + OrderStatus.WaitingConfirm + "','" + OrderStatus.WaitingPay + "')";
        return Convert.ToInt32(Db.Helper.ExecuteScalar(sql));
    }

}