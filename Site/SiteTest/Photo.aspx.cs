using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class SiteTest_Photo : SiteBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowTitle("照片墙");
        if(!Page.IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        int pageIndex = GetUrlInt("p");
        if (pageIndex <= 0) pageIndex = 1;
        SiteSetting.CreateWebPagerForDataList(dtPhoto,pageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "ID desc";
        query.PrimaryKey = PicWallInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = PicWallInfo.TableOrViewName;
        query.Condition = "1=1";

        dtPhoto.Db = TMS.Db.Helper;
        dtPhoto.Query = query;
        dtPhoto.DataBind();

        dtPhoto.RepeatDirection = RepeatDirection.Horizontal;
        dtPhoto.RepeatColumns = 5;
    }
}