using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;
using System.Data;

public partial class LineEdit : AdminBase
{
    protected int LineID { get { return GetUrlInt("id"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            DataTable dt = LineBLL.SelectLineCat("IsDel=0").Tables[0];
            DataHelper.BindTree(ddlCat, dt, "ID", "Name", "ParentID");
        }
       
        if (LineID == 0)
        {
            SetNav("添加线路");
            ltTips.Text = "在此，您可以添加线路";
            btnSubmit.Text = "确定添加";
        }
        else
        {
            SetNav("修改线路");
            ltTips.Text = "在此，您可以修改线路";
            btnSubmit.Text = "确定修改";
            btnClean.Visible = true;
            if(!Page.IsPostBack)
            {
                BindModel();
            }
        }

       
    }

    protected void BindModel()
    {
        var model = LineBLL.SelectLine(LineID);
        if (model == null) return;

        tbName.Text = model.Name;
        int catID = model.SecondCatID == 0 ? model.FirstCatID : model.SecondCatID;
        ddlCat.SelectedValue = catID.ToString();
        tbProductNum.Text = model.ProductNum;
        tbCoverPath.Text = model.CoverPath;
        tbDays.Text = model.TravelDays;
        tbStartAddress.Text = model.StartAddress;
        tbTargetAddress.Text = model.TargetAddress;
        tbBackTravel.Text = model.BackTravel;
        tbGoTravel.Text = model.GoTravel;
        tbRemarks.Text = model.LineDesc;
        tbNotice.Text = model.SignUpNotice;
        tbMinPrice.Text = model.MinPrice.ToString();
        chkPickup.Checked = model.IsPickup == 1 ? true : false;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string name = tbName.Text.Trim();
        int catID = ddlCat.SelectedValue.ToArrowInt();
        string productNum = tbProductNum.Text.Trim();
        string coverPath = tbCoverPath.Text.Trim();
        int days = tbDays.Text.Trim().ToArrowInt();
        string goTravel = tbGoTravel.Text.Trim();
        string backTravel = tbBackTravel.Text.Trim();
        string desc = tbRemarks.Text.Trim();
        string notice = tbNotice.Text.Trim();
        string startAddress = tbStartAddress.Text.Trim();
        string targetAddress = tbTargetAddress.Text.Trim();
        decimal minPrice = tbMinPrice.Text.Trim().ToArrowDecimal();

        #region 验证数据

        if (name.ValidateIsNullOrEmpty("请输入线路名称！"))
            return;

        if (productNum.ValidateIsNullOrEmpty("请输入产品编号！"))
            return;

        if ((minPrice == 0).ValidateSuccess("最低价不正确！"))
            return;

        if ((catID == 0).ValidateSuccess("请选择分类！"))
            return;

        if ((days <= 0).ValidateSuccess("旅游天数不正确！"))
            return;

        LineCatInfo cat = LineBLL.SelectLineCat(catID);
        if (cat.ValidateIsNull("该分类不存在！"))
            return;

        int firstCatID = 0;
        int secondCatID = 0;
        //如果是一级分类，则二级分类id为0
        if (cat.ParentID == 0)
        {
            firstCatID = catID;
        }
        else
        {
            firstCatID = cat.ParentID;
            secondCatID = catID;
        }
        #endregion



        if (LineID==0)
        {
            //添加
            var model = new LineInfo();
            model.AddTime = DateTime.Now;
            model.AddUserName = CurrentAdmin.UserName;
            model.AddUserRealName = CurrentAdmin.RealName;
            model.BackTravel = backTravel;
            model.BigPicPath = "";
            model.CoverPath = coverPath;
            model.FirstCatID = firstCatID;
            model.GoTravel = goTravel;
            model.IsDel = 0;
            model.LineDesc = desc;
            model.Name = name;
            model.OtherNotice = "";
            model.ProductNum = productNum;
            model.Remarks = "";
            model.SecondCatID = secondCatID;
            model.SignUpNotice = notice;
            model.StartAddress = startAddress;
            model.Tag = "";
            model.TargetAddress = targetAddress;
            model.TravelDays = days.ToString();
            model.WarmTips = "";
            model.MinPrice = minPrice;
            model.IsHot = 0;
            model.IsTop = 0;
            model.HitTimes = 0;
            model.IsPickup = chkPickup.Checked ? 1 : 0;
            LineBLL.AddLine(model);
            MessageBox.Show("添加成功！", CurrentUrl);
        }
        else
        {
            var model = LineBLL.SelectLine(LineID);
            if (model.ValidateIsNull("该线路不存在！"))
                return;

            model.BackTravel = backTravel;
            model.CoverPath = coverPath;
            model.FirstCatID = firstCatID;
            model.GoTravel = goTravel;
            model.LineDesc = desc;
            model.Name = name;
            model.ProductNum = productNum;
            model.SecondCatID = secondCatID;
            model.SignUpNotice = notice;
            model.StartAddress = startAddress;
            model.TargetAddress = targetAddress;
            model.TravelDays = days.ToString();
            model.MinPrice = minPrice;
            model.IsPickup = chkPickup.Checked ? 1 : 0;
            LineBLL.UpdateLine(model);
            MessageBox.Show("修改成功！");

        }



    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("LineManager.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbCoverPath.Text = SiteUtility.UploadPic(upCover);
    }

   
}