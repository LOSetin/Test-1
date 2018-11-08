using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework;
using System.Web.UI;
using TMS;
using Arrow.Framework.Extensions;

/// <summary>
/// UIBase 的摘要说明
/// </summary>
public class UIBase : PageBase
{
    /// <summary>
    /// 当前登录用户信息，关闭浏览器则消失
    /// </summary>
    public MemberInfo CurrentMember
    {
        get { return MemberBLL.GetLoginInfo(); }
    }

    /// <summary>
    /// 获取?后的url参数，排除参数p
    /// </summary>
    /// <returns></returns>
    public string GetUrlQuery()
    {
        string result = "";

        string query = Request.Url.Query;
        if (!query.IsNullOrEmpty())
        {
            query = query.Trim();
            if (query.StartsWith("?"))
            {
                query = query.Substring(1);
                string[] paras = query.Split('&');
                foreach (string s in paras)
                {
                    if (!s.StartsWith("p="))
                    {
                        result = result + "&" + s;
                    }
                }
            }
        }


        return result;
    }

    /// <summary>
    /// 生成带搜索参数的url
    /// </summary>
    /// <param name="myPara"></param>
    /// <param name="value"></param>
    /// <param name="excludeParas">排除的参数</param>
    /// <returns></returns>
    public string CreateQueryUrl(string myPara, string value, string[] excludeParas = null)
    {
        string result = "";
        bool isReplace = false;
        string query = Request.Url.Query;
        if (!query.IsNullOrEmpty())
        {
            query = query.Trim();
            if (query.StartsWith("?"))
            {
                query = query.Substring(1);
                string[] paras = query.Split('&');
                foreach (string s in paras)
                {
                    if (s.StartsWith("p="))
                        break;
                    if (s.StartsWith(myPara + "="))
                    {
                        result = result + "&" + myPara + "=" + Server.UrlEncode(value);
                        isReplace = true;
                    }
                    else
                    {
                        //如果要排除的参数不为空
                        bool isDel = false;
                        if (excludeParas != null)
                        {
                            for (int i = 0; i < excludeParas.Length; i++)
                            {
                                if (s.StartsWith(excludeParas + "="))
                                {
                                    isDel = true;
                                    break;
                                }
                            }
                        }
                        if (!isDel)
                            result = result + "&" + s;
                    }

                }
            }
        }
        if (!isReplace)
            result = result + "&" + myPara + "=" + Server.UrlEncode(value);
        result = "?p=" + GetUrlInt(UrlPara_CurrentPageIndex) + result;
        return result;
    }

  
    /// <summary>
    /// 获取热卖的二级分类
    /// </summary>
    /// <returns></returns>
    public List<LineCatInfo> getHotSellCats()
    {
        List<LineCatInfo> cats = new LineCat().SelectList("IsDel=0 And IsHotSell=1 Order by ID desc");
        return cats;
    }

    /// <summary>
    /// 显示默认图片
    /// </summary>
    /// <param name="coverPath"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public string getCoverFullPath(string coverPath,int width,int height)
    {
        return SiteUtility.ShowImage(coverPath, width, height);
    }

    /// <summary>
    /// 返回没有处理过的原始图片
    /// </summary>
    /// <param name="coverPath"></param>
    /// <returns></returns>
    public string getRawCover(string coverPath)
    {
        return HttpHelper.GetRootURI() + GlobalSetting.ImageUploadPath.Replace("~", "") + coverPath;
    }

}