using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Master_Right : AdminBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("后台首页");
    }

    protected string GetServerIP()
    {
        string ip = Request.ServerVariables["LOCAL_ADDR"];
        if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
        {
            ip += "&nbsp;[共享IP，对外IP：" + Request.ServerVariables["REMOTE_ADDR"] + "]";
        }
        return ip;
    }


}