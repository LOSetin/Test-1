using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

public partial class GoodsEdit : AdminBase
{
    protected int GetGoodsID()
    {
        return GetUrlInt("ID");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetGoodsID() == 0)
        {
            SetNav("添加商品");
            ltTips.Text = "在这里，您可以添加商品";
            btnSubmit.Text = "确定添加";
        }
        else
        {
            SetNav("修改商品");
            ltTips.Text = "在这里，您可以修改商品";
            btnSubmit.Text = "确定修改";
            if (!Page.IsPostBack)
                BindModel();
        }

        if(!Page.IsPostBack)
        {
            GoodsBLL.BindGoodsCat(ddlCat, false);
        }

    }

    protected void BindModel()
    {
        var model = GoodsBLL.SelectGoods(GetGoodsID());
        if(model!=null)
        {
            tbName.Text = model.Name;
            ddlCat.SelectedValue = model.CatID.ToString();
            tbNum.Text = model.Num.ToString();
            tbPoints.Text = model.Points.ToString();
            tbRemarks.Text = model.Remarks.ToString();
            tbCoverPath.Text = model.CoverPath;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int id = GetGoodsID();
        string name = tbName.Text.Trim();
        int cid = ddlCat.SelectedValue.ToArrowInt();
        int point = tbPoints.Text.Trim().ToArrowInt();
        int num = tbNum.Text.Trim().ToArrowInt();
        string cover = tbCoverPath.Text.Trim();
        string remarks = tbRemarks.Text.Trim();

        if (name.ValidateIsNullOrEmpty("请输入商品名称！"))
            return;

        if ((cid == 0).ValidateSuccess("请先选择分类！"))
            return;

        if ((point == 0).ValidateSuccess("兑换点数必须大于0！"))
            return;

        if ((num == 0).ValidateSuccess("数量必须大于0！"))
            return;


        if(id==0)
        {
            var model = new TMS.GoodsInfo();
            model.AddTime = DateTime.Now;
            model.AddUserName = CurrentAdmin.UserName;
            model.AddUserRealName = CurrentAdmin.RealName;
            model.BigPicPath = "";
            model.CatID = cid;
            model.CoverPath = cover;
            model.IsOut = 0;
            model.Name = name;
            model.Num = num;
            model.Points = point;
            model.Remarks = remarks;

            GoodsBLL.AddGoods(model);

            tbName.Text = "";
            tbPoints.Text = "";
            tbNum.Text = "";
            tbCoverPath.Text = "";
            tbRemarks.Text = "";
            MessageBox.Show("添加成功！");
        }
        else
        {
            var model = GoodsBLL.SelectGoods(id);
            if (model!= null)
            {
                model.Name = name;
                model.CatID = cid;
                model.CoverPath = cover;
                model.Num = num;
                model.Points = point;
                model.Remarks = remarks;
                GoodsBLL.UpdateGoods(model);
                MessageBox.Show("修改成功！");
            }

        }
        

       


    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("GoodsManager.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbCoverPath.Text = SiteUtility.UploadPic(upCover);
    }
}