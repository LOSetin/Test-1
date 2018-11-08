using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 
/// </summary>
public static class IconHelper
{
    /// <summary>
    /// 将标题中的[飞机]转换为图标
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
   public static string Replace(string title)
    {
        title=title.Replace("[飞机]", " <i class='iconfont i_fj' style='font-size:20px; '></i> ");
        title = title.Replace("[汽车]", " <i class='iconfont i_qc' style='font-size:20px; '></i> ");
        title = title.Replace("[轮船]", " <i class='iconfont i_lc' style='font-size:20px; '></i> ");
        return title;
    }
}