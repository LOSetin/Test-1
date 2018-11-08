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

public partial class GoodsCat : AdminBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("商品分类管理");
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Ascending = false;
        query.Fields = GoodsBLL.GoodsCatFields;
        query.OrderBy = "SortOrder";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "GoodsCat";
        query.Condition = "IsDel=0";
        
        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if(e.Row.RowType==DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除分类吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = e.CommandArgument.ToArrowInt();
        string command = e.CommandName;
        if(command=="UpdateData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string catName = (gvData.Rows[drv.RowIndex].FindControl("tbName") as TextBox).Text;
            string order = (gvData.Rows[drv.RowIndex].FindControl("tbOrder") as TextBox).Text;
            if(!catName.IsNullOrEmpty())
            {
                var model = GoodsBLL.SelectGoodsCat(id);
                if(model!=null)
                {
                    model.Name = catName;
                    model.SortOrder = order.ToArrowInt();
                    GoodsBLL.UpdateGoodsCat(model);
                }
            }

        }
        else if(command=="DelData")
        {
            var model = GoodsBLL.SelectGoodsCat(id);
            if (model != null)
            {
                model.IsDel = 1;
                GoodsBLL.UpdateGoodsCat(model);
            }
        }

        BindData();
        MessageBox.Show("操作成功！");

    }



    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string catName = tbName.Text.Trim();
        int sortOrder = tbSortOrder.Text.Trim().ToArrowInt();
        if (catName.ValidateIsNullOrEmpty("请输入分类名称！"))
            return;

        var model = new TMS.GoodsCatInfo();
        model.IsDel = 0;
        model.Name = catName;
        model.Remarks = "";
        model.SortOrder = sortOrder;

        GoodsBLL.AddGoodsCat(model);

        tbName.Text = "";
        tbSortOrder.Text = "";
        MessageBox.Show("添加成功！");
        BindData();

    }
}