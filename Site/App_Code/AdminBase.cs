using System;
using Arrow.Framework;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

/// <summary>
/// AdminBase 的摘要说明
/// </summary>
public class AdminBase : PageBase
{
    #region 属性 
    /// <summary>
    /// 显示错误
    /// </summary>
    public bool ShowCustomErrorMsg
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// 当前登录用户信息，关闭浏览器则消失
    /// </summary>
    public LoginInfo CurrentAdmin
    {
        set { SiteUserBLL.SetLoginInfo(value); }
        get { return SiteUserBLL.GetLoginInfo(); }
    }
    #endregion

    #region 方法
   

    /// <summary>
    /// 获取url参数ReturnUrl，并跳转到该地址，如果该地址不存在，在跳转到后台首页
    /// </summary>
    /// <returns></returns>
    public void RedirectToReturnUrl()
    {
        RedirectToReturnUrl("../Master/Right.aspx");
    }


    /// <summary>
    /// 设置当前导航
    /// </summary>
    /// <param name="title">标题</param>
    protected void SetNav(string title)
    {
        if (CurrentAdmin != null)
        {
            ((Literal)Master.FindControl("ltTitle")).Text = title+"&nbsp;&nbsp;&nbsp;";
        }
    }

    private void AddSubNav(string title, string url = "#")
    {
        if (CurrentAdmin != null)
        {
            string format = "<span class='subNav'>{0} / </span>";
            if (!url.IsNullOrEmpty() && !url.IsEqualTo("#"))
                format = "<span class='subNav'><a href='{1}'>{0}</a> / </span>";
            ((Literal)Master.FindControl("ltTitle")).Text += string.Format(format, title, url);
        }
    }

    /// <summary>
    /// 字符串是以-拆分，第一个是标题，第二个是url
    /// </summary>
    /// <param name="items"></param>
    protected void AddSubNav(params string[] items)
    {
        if(CurrentAdmin!=null)
        {
            if (items.Length > 0)
                AddSubNav("后台首页");
            for(int i=0;i<items.Length;i++)
            {
                string[] arr = items[i].Split('-');
                if (arr.Length >= 2)
                    AddSubNav(arr[0], arr[1]);
                else
                    AddSubNav(arr[0]);
            }
        }
    }

    #endregion

    protected override void OnInit(EventArgs e)
    {
        if(!CurrentUrl.EndsWith("/Login.aspx"))
        {
            if (CurrentAdmin == null)
            {
                Response.Write("<script>alert('连接超时!');parent.window.location.href='../Master/Login.aspx';</script>");
                Response.End();
            }
            else
            {
                //Sys只有超管可以访问
                if(CurrentUrl.Contains("/Sys/") && !CurrentAdmin.IsSuper)
                {
                    Response.Write("<script>alert('没有访问的权限!');history.go(-1);</script>");
                    Response.End();
                }
            }
        }

        base.OnInit(e);


    }

}
