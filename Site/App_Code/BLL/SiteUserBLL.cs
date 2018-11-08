using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS;
using Arrow.Framework;
using System.Data.Common;

/// <summary>
/// SiteUserBLL 的摘要说明
/// </summary>
public class SiteUserBLL
{
    private static readonly SiteUser dal = new SiteUser();
    private static readonly IArrowUserStatus<LoginInfo> currentAdmin = new ArrowWebCookieStatus<LoginInfo>();
    private static readonly string adminKey = "KEY_TMS_ADMIN";

    /// <summary>
    /// 所有字段
    /// </summary>
    public static readonly string AllFields = SiteUserInfo.AllFields;

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string AdminEncrypt(string str)
    {
        return EncryptHelper.MD5Encrypt(str);
    }

    #region 基本方法
    /// <summary>
    /// 获得管理用户信息
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static SiteUserInfo Select(string userName)
    {
        return dal.Select(userName);
    }

    public static int Add(SiteUserInfo model)
    {
        return dal.Add(model);
    }

    public static int Update(SiteUserInfo model)
    {
        return dal.Update(model);
    }

    public static int Del(string userName)
    {
        return dal.Delete(userName);
    }
    #endregion

    /// <summary>
    /// 判断验证码是否存在
    /// </summary>
    /// <param name="inviteNum"></param>
    /// <returns></returns>
    public static bool InviteNumExist(string inviteNum)
    {
        string strWhere = "InviteNum=@inviteNum";
        DbParameter[] paras = { Db.Helper.MakeInParameter("@inviteNum", inviteNum) };
        return dal.GetCount(strWhere, paras) >= 1 ? true : false;
    }

    /// <summary>
    /// 根据邀请码选择用户
    /// </summary>
    /// <param name="inviteNum"></param>
    /// <returns></returns>
    public static SiteUserInfo SelectUserByInviteNum(string inviteNum)
    {
        string strWhere = "InviteNum=@inviteNum";
        DbParameter[] paras = { Db.Helper.MakeInParameter("@inviteNum", inviteNum) };
        List<SiteUserInfo> users = dal.SelectList(strWhere, paras);
        if (users.Count == 1)
            return users[0];
        else
            return null;

    }

    #region 设置登录信息，退出登录
    public static void SetLoginInfo(LoginInfo li)
    {
        currentAdmin.SetValue(adminKey, li);
    }

    public static LoginInfo GetLoginInfo()
    {
        return currentAdmin.GetValue(adminKey);
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    public static void Logout()
    {
        currentAdmin.Remove(adminKey);
    }
    #endregion

    /// <summary>
    /// 登陆管理用户，返回登陆实体，并更新登陆信息
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public static LoginInfo DoLogin(string userName,string pwd)
    {
        LoginInfo li = null;
        SiteUserInfo model = Select(userName);
        if(model!=null && model.Pwd==AdminEncrypt(pwd) && model.UserStatus==AdminStatus.Enable)
        {
            //登陆成功
            //更新登陆信息
            model.LastLoginIP = model.ThisLoginIP;
            model.LastLoginTime = model.ThisLoginTime;
            model.ThisLoginIP = HttpHelper.GetClientIP();
            model.ThisLoginTime = DateTime.Now;
            dal.Update(model);

            li = new LoginInfo();
            li.InviteNum = model.InviteNum;
            li.IsSuper = model.RoleIDs == "0" ? true : false;
            li.RealName = model.RealName;
            li.UserName = model.Name;
            li.LastLoginIP = model.LastLoginIP;
            li.LastLoginTime = model.LastLoginTime;
            li.ThisLoginIP = model.ThisLoginIP;
            li.ThisLoginTime = model.ThisLoginTime;
            SetLoginInfo(li);
        }

        return li;
    }

    /// <summary>
    /// 更改密码
    /// </summary>
    /// <param name="model"></param>
    /// <param name="newPwd"></param>
    /// <returns></returns>
    public static bool ChangePwd(SiteUserInfo model,string newPwd)
    {
        string pwd = AdminEncrypt(newPwd);
        model.Pwd = pwd;
        return dal.Update(model) == 1 ? true : false;
    }

}