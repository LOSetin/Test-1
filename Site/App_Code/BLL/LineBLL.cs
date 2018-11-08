using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework;
using TMS;
using Arrow.Framework.Extensions;
using System.Data;

/// <summary>
/// LineBLL 的摘要说明
/// </summary>
public class LineBLL
{
    private static readonly LineCat catDAL = new LineCat();
    private static readonly Line lineDAL = new Line();
    private static readonly LineDetail detailDal = new LineDetail();

    #region 基本方法
    public static List<LineCatInfo> SelectCatList(int topNum,string strWhere,string orderBy)
    {
        return catDAL.SelectList(topNum, strWhere, orderBy);
    }

    public static LineCatInfo SelectTop1Cat()
    {
        List<LineCatInfo> catList = catDAL.SelectList(1, "IsDel=0", "SortOrder");
        if (catList.Count == 1)
            return catList[0];
        else
            return null;
    }

    public static DataSet SelectLineCat(string strWhere)
    {
        return catDAL.SelectDataSet(strWhere);
    }

    public static List<LineCatInfo> SelectCatList(string strWhere)
    {
        return catDAL.SelectList(strWhere, null);
    }

    public static int AddLineCat(LineCatInfo model)
    {
        return catDAL.Add(model);
    }

    public static LineCatInfo SelectLineCat(int id)
    {
        return catDAL.Select(id);
    }

    public static int UpdateLineCat(LineCatInfo model)
    {
        return catDAL.Update(model);
    }

    public static int AddLine(LineInfo model)
    {
        return lineDAL.Add(model);
    }

    public static LineInfo SelectLine(int id)
    {
        return lineDAL.Select(id);
    }

    public static int UpdateLine(LineInfo model)
    {
        return lineDAL.Update(model);
    }

    public static int AddDetail(LineDetailInfo model)
    {
        return detailDal.Add(model);
    }

    public static int UpdateDetail(LineDetailInfo model)
    {
        return detailDal.Update(model);
    }

    public static LineDetailInfo SelectDetail(int id)
    {
        return detailDal.Select(id);
    }

    public static int DelDetail(int id)
    {
        return detailDal.Delete(id);
    }

    public static List<LineInfo> SelectLineList(string strWhere)
    {
        return lineDAL.SelectList(strWhere);
    }

    public static List<LineInfo> SelectLineList(int top,string strWhere,string orderBy)
    {
        return lineDAL.SelectList(top, strWhere, orderBy);
    }

    public static List<LineInfo> SelectLineList(string strWhere, string orderBy, int pageIndex, int pageSize)
    {
        return lineDAL.SelectList(strWhere, orderBy, pageIndex, pageSize);
    }

    #endregion

    /// <summary>
    /// 获得最近发团日期
    /// </summary>
    /// <param name="lineID"></param>
    /// <returns></returns>
    public static TravelGroupInfo GetLastGroup(int lineID)
    {
        var list = new TravelGroup().SelectList(1, "LineID=" + lineID, "GoDate desc");
        if (list.Count == 1)
            return list[0];
        else
            return null;

    }

    /// <summary>
    /// 设置线路的删除状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDel"></param>
    /// <returns></returns>
    public static int SetLineIsDel(int id,int isDel)
    {
        string sql = "Update Line Set IsDel=" + isDel + " Where ID=" + id;
        return Db.Helper.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 显示分类名
    /// </summary>
    /// <param name="allCat"></param>
    /// <param name="firstID"></param>
    /// <param name="secondID"></param>
    /// <returns></returns>
    public static string ShowCatName(List<LineCatInfo> allCat, int firstID,int secondID)
    {
        string result = "";
        LineCatInfo firstCat = allCat.Find(s => s.ID == firstID);
        LineCatInfo secondCat = allCat.Find(s => s.ID == secondID);
        if(firstCat!=null)
        {
            result = result + firstCat.Name;
            if (secondCat != null)
                result = result + "—" + secondCat.Name;
        }

        return result;
    }

    /// <summary>
    /// 选择某一线路的详细行程
    /// </summary>
    /// <param name="lineID"></param>
    /// <returns></returns>
    public static List<LineDetailInfo> GetLineDetails(int lineID)
    {
        return detailDal.SelectList("LineID=" + lineID+" Order By SortOrder");
    }


}