using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class PhotoManager : AdminBase
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("照片管理");
        if(!Page.IsPostBack)
        {
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "1=1";
        string keyword = GetUrlString("keyword");
        if(!keyword.IsNullOrEmpty())
        {
            condition = condition + " And Name like '%" + keyword + "%'";
        }


        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Ascending = false;
        query.Fields = "*";
        query.OrderBy = "ID desc,IsTop desc";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "PicWall";
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        tbKeyWord.Text = keyword;

    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该照片吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string picName = (gvData.Rows[drv.RowIndex].FindControl("tbName") as TextBox).Text;
            var model = PicWallBLL.Select(id);
            if(model!=null)
            {
                model.Name = picName;
                PicWallBLL.UpdatePhoto(model);
            }
        }
        else if (e.CommandName == "DelData")
        {
            //删除
            PicWallBLL.DelPhoto(id);
        }
       
        BindData();
        MessageBox.Show("操作成功！");
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("PhotoManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()));
    }

   
}