using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// LoginInfo 的摘要说明
/// </summary>
public class LoginInfo
{
    public string UserName { set; get; }

    public string RealName { set; get; }

    public string InviteNum { set; get; }

    public bool IsSuper { set; get; }

    public string LastLoginIP { set; get; }

    public string ThisLoginIP { set; get; }

    public DateTime LastLoginTime { set; get; }

    public DateTime ThisLoginTime { set; get; }

}