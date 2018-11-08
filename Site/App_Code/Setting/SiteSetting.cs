using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework.Extensions;
using Arrow.Framework.WebControls;

/// <summary>
/// 管理设置
/// </summary>
public static class SiteSetting
{
    /// <summary>
    /// 分页大小
    /// </summary>
    public static readonly int PageSize = 10;

    /// <summary>
    /// 分页显示的页数
    /// </summary>
    public static readonly int PageButtonCount = 10;

    /// <summary>
    /// 生成分页对象，并绑定到对应的DataList实例
    /// </summary>
    /// <param name="dtData"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public static WebPager CreateWebPagerForDataList(DataList dtData, int pageIndex)
    {
        WebPager pager = new WebPager();
        pager.ButtonCount = PageButtonCount;
        pager.PageSize = PageSize;
        pager.PageStyle = ControlPageStyle.Full;
        pager.PageCSSName = "gvPage";
        pager.PageIndex = pageIndex;
        dtData.Pager = pager;

        return pager;
    }

    /// <summary>
    /// 生成分页对象，并绑定到对应的GridView实例
    /// </summary>
    /// <param name="dtData"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    public static WebPager CreateWebPagerForGridView(GridView dtData, int pageIndex)
    {
        WebPager pager = new WebPager();
        pager.ButtonCount = PageButtonCount;
        pager.PageSize = PageSize;
        pager.PageStyle = ControlPageStyle.Full;
        pager.PageCSSName = "gvPage";
        pager.PageIndex = pageIndex;
        dtData.Pager = pager;

        return pager;
    }

}