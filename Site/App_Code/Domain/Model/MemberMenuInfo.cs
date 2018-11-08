using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 会员菜单实体类
/// </summary>
public class MemberMenuInfo
{
    public MemberMenuInfo(string title,string url)
    {
        this.Title = title;
        this.Url = url;
    }

    public string Title { set; get; }

    public string Url { set; get; }

}