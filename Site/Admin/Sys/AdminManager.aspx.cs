using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using System.Data;

public partial class Station_DealRecord : AdminBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("管理员管理");
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        string realName = GetUrlString("RealName");

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Ascending = false;
        if (!realName.IsNullOrEmpty())
            query.Condition = "RealName like '%" + realName + "%'";
        query.Fields = SiteUserBLL.AllFields;
        query.OrderBy = "RoleIDs";
        query.PrimaryKey = "Name";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "SiteUser";
        
        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        tbName.Text = realName;
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if(e.Row.RowType==DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnReset")).Attributes.Add("onclick", "return confirm('确定重置密码吗？');");
            ((LinkButton)e.Row.FindControl("lbtnEnable")).Attributes.Add("onclick", "return confirm('确定启用该用户吗？');");
            ((LinkButton)e.Row.FindControl("lbtnDisabled")).Attributes.Add("onclick", "return confirm('确定禁用该用户吗？');");
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该用户吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string userName = e.CommandArgument.ToArrowString();
        string command = e.CommandName;
        var model = SiteUserBLL.Select(userName);
        if(model.ValidateIsNull("该用户不存在！"))
        {
            BindData();
            return;
        }
        if (command== "UpdateRealName")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string realName = (gvData.Rows[drv.RowIndex].FindControl("tbRealName") as TextBox).Text;
            if (!realName.IsNullOrEmpty())
            { 
                model.RealName = realName;
                SiteUserBLL.Update(model);
                MessageBox.Show("修改成功！");
            }
        }
        else if(command== "ReSetPwd")
        {
            model.Pwd = SiteUserBLL.AdminEncrypt("admin");
            SiteUserBLL.Update(model);
            MessageBox.Show("重置成功！");
        }
        else if(command== "Enable")
        {
            model.UserStatus = AdminStatus.Enable;
            SiteUserBLL.Update(model);
            MessageBox.Show("启用成功！");
        }
        else if(command== "Disabled")
        {
            model.UserStatus = AdminStatus.Disabled;
            SiteUserBLL.Update(model);
            MessageBox.Show("禁用成功！");
        }
        else if(command=="Del")
        {
            try
            {
                SiteUserBLL.Del(userName);
                MessageBox.Show("删除成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        BindData();
       

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string url = "AdminManager.aspx?RealName=" + Server.UrlEncode(tbName.Text.Trim()) ;
        Response.Redirect(url);

     
    }
}