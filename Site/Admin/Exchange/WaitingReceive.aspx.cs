using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class WaitingReceive : AdminBase
{
    protected List<LineCatInfo> allCats = LineBLL.SelectCatList("1=1");

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("等待收货");
        if(!Page.IsPostBack)
        {
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "ExchangeStatus='" + ExchangeStatus.WaitingReceive + "'";
        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");


       if(dtBegin!=DateTime.MinValue && dtEnd!=DateTime.MinValue && dtBegin<=dtEnd)
        {
            condition = condition + " And AddTime>='" + dtBegin.ToStartString() + "' And AddTime<='" + dtEnd.ToEndString() + "'";
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "ID desc";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_ExchangeHistory";
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        tbBegin.Text = dtBegin == DateTime.MinValue ? "" : dtBegin.ToDateOnlyString();
        tbEnd.Text = dtEnd == DateTime.MinValue ? "" : dtEnd.ToDateOnlyString();
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnConfirm")).Attributes.Add("onclick", "return confirm('确认收货？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "ConfirmData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string memberUserName = (gvData.Rows[drv.RowIndex].FindControl("hfMemberUserName") as HiddenField).Value;

            var model = new CostHistory().Select(id);
            if (model == null)
            {
                MessageBox.Show("该记录不存在！");
            }
            else
            {
                model.FinishTime = DateTime.Now;
                model.ExchangeStatus = ExchangeStatus.Finish;
                new CostHistory().Update(model);
                BindData();
                MessageBox.Show("确认成功！");
            }

        }
       
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("WaitingReceive.aspx?bdate=" + tbBegin.Text.Trim()+"&edate="+tbEnd.Text.Trim());
    }

    
}