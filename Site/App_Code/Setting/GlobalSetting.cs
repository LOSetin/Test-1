using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GlobalSetting 的摘要说明
/// </summary>
public class GlobalSetting
{
    /// <summary>
    /// 系统日期最小值，1900-1-1
    /// </summary>
    public static readonly DateTime MinTime = new DateTime(1900, 1, 1);

    /// <summary>
    /// 默认图片上传路径
    /// </summary>
   public static readonly string ImageUploadPath = "~/Upload/Pic/";

    /// <summary>
    /// 可允许上传的图片扩展名
    /// </summary>
    public static readonly string[] ImageExtensions = { ".png", ".jpeg", ".jpg" };

}