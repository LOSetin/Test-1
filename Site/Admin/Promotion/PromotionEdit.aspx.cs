using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;

public partial class PromotionEdit : AdminBase
{
    protected int PromotionID { get { return GetUrlInt("id"); } }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            typeof(PromotionType).BindToDropDownList(ddlType);
            SetControlStatus();
        }
        if (PromotionID == 0)
        {
            SetNav("添加促销");
            ltTips.Text = "在此，您可以添加促销";
            btnSubmit.Text = "确定添加";
            btnClean.Visible = false;
        }
        else
        {
            SetNav("修改促销");
            ltTips.Text = "在此，您可以修改促销";
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
        var model = PromotionBLL.Select(PromotionID);
        if (model == null) return;

        tbName.Text = model.Name;
        tbBegin.Text = model.StartTime.ToDateOnlyString();
        tbEnd.Text = model.EndTime.ToDateOnlyString();
        ddlType.SelectedValue = model.PromotionType;
        SetControlStatus();
        tbDiscount.Text = model.Discount.ToString();
        tbTotalPay.Text = model.TotalPayOneTime.ToString();
        tbJoinNum.Text = model.TotalPayOneTimeJoinNum.ToString();
        tbCoverPath.Text = model.CoverPath;
        tbRemarks.Text = model.PromotionDesc;

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string name = tbName.Text.Trim();
        string cover = tbCoverPath.Text.Trim();
        string promotionType = ddlType.SelectedValue.ToArrowString();
        decimal discount = tbDiscount.Text.Trim().ToArrowDecimal();
        decimal totalOneTime = tbTotalPay.Text.Trim().ToArrowDecimal();
        int joinNum = tbJoinNum.Text.Trim().ToArrowInt();
        string desc = tbRemarks.Text.Trim();
        DateTime dtBegin = tbBegin.Text.Trim().ToArrowDateTime();
        DateTime dtEnd = tbEnd.Text.Trim().ToArrowDateTime();

        #region 验证数据
        if (name.ValidateIsNullOrEmpty("请输入促销名称！"))
            return;

        if ((dtBegin == DateTime.MinValue || dtEnd == DateTime.MinValue).ValidateSuccess("开始时间和结束时间不正确！"))
            return;

        if ((dtBegin >= dtEnd).ValidateSuccess("结束时间必须大于开始时间！"))
            return;

        if (promotionType.IsEqualTo(PromotionType.Discount))
        {
            if(discount>=1 || discount<=0)
            {
                MessageBox.Show("折扣输入错误！");
                return;
            }
        }
        else if(promotionType.IsEqualTo(PromotionType.Bundle))
        {
            if ((totalOneTime <= 0).ValidateSuccess("一次性支付金额不正确！"))
                return;

            if ((joinNum <= 0).ValidateSuccess("购买团位个数不正确！"))
                return;
        }

        #endregion

        PromotionInfo model = null;

        if (PromotionID == 0)
        {
            //添加
            model = new PromotionInfo();
        }
        else
        {
            model = PromotionBLL.Select(PromotionID);
            if (model == null) return;
        }
        model.CoverPath = cover;
        model.Discount = discount;
        model.EndTime = dtEnd;
        model.FullCutMinus = 0M;
        model.FullCutTotal = 0M;
        model.IsDel = 0;
        model.Name = name;
        model.PromotionDesc = desc;
        model.PromotionType = promotionType;
        model.Remarks = "";
        model.SecondKillPrice = 0M;
        model.StartTime = dtBegin;
        model.Tag = "";
        model.TotalPayOneTime = totalOneTime;
        model.TotalPayOneTimeJoinNum = joinNum;
        if(PromotionID==0)
        {
            model.AddTime = DateTime.Now;
            model.AddUserName = CurrentAdmin.UserName;
            model.AddUserRealName = CurrentAdmin.RealName;
            PromotionBLL.Add(model);
            MessageBox.Show("添加成功！", CurrentUrl);
        }
        else
        {
            PromotionBLL.Update(model);
            MessageBox.Show("修改成功！");
        }
       
       

    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("PromotionManager.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbCoverPath.Text = SiteUtility.UploadPic(upCover);
    }

    private void SetControlStatus()
    {
        if (ddlType.SelectedValue.ToArrowString().IsEqualTo(PromotionType.Discount))
        {
            tbTotalPay.Enabled = false;
            tbJoinNum.Enabled = false;
            tbDiscount.Enabled = true;
        }
        else if (ddlType.SelectedValue.ToArrowString().IsEqualTo(PromotionType.Bundle))
        {
            tbDiscount.Enabled = false;
            tbTotalPay.Enabled = true;
            tbJoinNum.Enabled = true;
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetControlStatus();
    }
}