using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS;

/// <summary>
/// PicWallBLL 的摘要说明
/// </summary>
public class PicWallBLL
{
    private static readonly PicWall dal = new PicWall();

    #region 基本方法
    public static int AddPhoto(PicWallInfo model)
    {
        return dal.Add(model);
    }

    public static int DelPhoto(int id)
    {
        return dal.Delete(id);
    }

    public static int UpdatePhoto(PicWallInfo model)
    {
        return dal.Update(model);
    }

    public static PicWallInfo Select(int id)
    {
        return dal.Select(id);
    }

    public static List<PicWallInfo> SelectList(string strWhere,string orderBy,int pageIndex,int pageSize)
    {
        return dal.SelectList(strWhere, orderBy, pageIndex, pageSize);
    }

    #endregion

}