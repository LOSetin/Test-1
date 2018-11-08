<%@ WebHandler Language="C#" Class="FavHandler" %>

using System;
using System.Web;
using Arrow.Framework.Extensions;

public class FavHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string act = context.Request.QueryString["act"];
        string memberName = context.Request.QueryString["memberName"];
        int LineID = context.Request.QueryString["ID"].ToArrowInt();

        var member = MemberBLL.GetLoginInfo();
        if (member == null || memberName.IsNullOrEmpty())
        {
            context.Response.Write("0");
            context.Response.End();
        }

        if (member.UserName != memberName)
        {
            context.Response.Write("您没有收藏的权限！");
            context.Response.End();
        }

        if (LineID == 0)
        {
            context.Response.Write("路线不存在！");
            context.Response.End();
        }


        if (act == "add")
        {
            var model = new TMS.MemberFavInfo();
            model.AddTime = DateTime.Now;
            model.FavObjID = LineID.ToString();
            model.FavType = FavType.Line;
            model.UserName = memberName;
            try
            {
                MemberBLL.AddFav(model);
                context.Response.Write("1");
            }
            catch
            {
                context.Response.Write("操作失败，请稍候再试！");
            }
        }
        else if (act == "remove")
        {
            try
            {
                MemberBLL.DelFav(memberName, FavType.Line, LineID.ToString());
                context.Response.Write("1");
            }
            catch
            {
                context.Response.Write("操作失败，请稍候再试！");
            }
        }
        else
        {
            context.Response.Write("无效的操作！");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}