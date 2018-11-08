using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TMS;
using System.Data.Common;

/// <summary>
/// GoodsCatBLL 的摘要说明
/// </summary>
public class GoodsBLL
{
    private static readonly GoodsCat goodsCatDAL = new GoodsCat();
    private static readonly Goods goodsDAL = new Goods();

    /// <summary>
    /// 商品分类所有字段
    /// </summary>
    public static readonly string GoodsCatFields = GoodsCatInfo.AllFields;

    /// <summary>
    /// 商品所有字段
    /// </summary>
    public static readonly string GoodsFields = GoodsInfo.AllFields;

    #region 基本方法

    public static int AddGoodsCat(GoodsCatInfo model)
    {
        return goodsCatDAL.Add(model);
    }

    public static GoodsCatInfo SelectGoodsCat(int catID)
    {
        return goodsCatDAL.Select(catID);
    }

    public static int UpdateGoodsCat(GoodsCatInfo model)
    {
        return goodsCatDAL.Update(model);
    }

    public static int AddGoods(GoodsInfo model)
    {
        return goodsDAL.Add(model);
    }

    public static GoodsInfo SelectGoods(int id)
    {
        return goodsDAL.Select(id);
    }

    public static int UpdateGoods(GoodsInfo model)
    {
        return goodsDAL.Update(model);
    }

    public static GoodsInfo SelectGoods(int id,DbTransaction tran)
    {
        return goodsDAL.Select(id, tran);
    }

    public static int UpdateGoods(GoodsInfo model,DbTransaction tran)
    {
        return goodsDAL.Update(model, tran);
    }

    #endregion

    /// <summary>
    /// 绑定商品分类到下拉列表
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="isShowAllItem"></param>
    public static void BindGoodsCat(DropDownList ddl,bool isShowAllItem=true)
    {
        ddl.DataSource = goodsCatDAL.SelectList("IsDel=0 Order by SortOrder");
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Name";
        ddl.DataBind();
        if(isShowAllItem)
        {
            ddl.Items.Insert(0, new ListItem("所有分类", "0"));
        }

    }

    /// <summary>
    /// 将商品上架或下架
    /// </summary>
    /// <param name="goodsID">商品ID</param>
    /// <param name="isOut">1表示下架，0表示上架</param>
    /// <returns></returns>
    public static int ChangeGoodsStatus(int goodsID,int isOut)
    {
        string sql = "Update Goods Set IsOut=" + isOut + " Where ID=" + goodsID;
        return Db.Helper.ExecuteNonQuery(sql, null);
    }

    /// <summary>
    /// 获取所有未下架的商品
    /// </summary>
    /// <returns></returns>
    public static List<GoodsInfo> GetAllGoods()
    {
        return goodsDAL.SelectList("IsOut=0", null);
    }

}