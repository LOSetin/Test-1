using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

public partial class SliderEdit : AdminBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("添加幻灯");
        ltTips.Text = "在此，您可以添加幻灯";
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string name = tbName.Text.Trim();
        string cover = tbCoverPath.Text.Trim();
        string url = tbUrl.Text.Trim();
  
        if (name.ValidateIsNullOrEmpty("请输入商品名称！"))
            return;

        if (url.ValidateIsNullOrEmpty("请输入url地址！"))
            return;

        if (cover.ValidateIsNullOrEmpty("请上传照片！"))
            return;

        var model = new TMS.SliderInfo();
        model.AddTime = DateTime.Now;
        model.AddUserName = CurrentAdmin.UserName;
        model.IsTop = 0;
        model.Name = name;
        model.Url = url;
        model.IsShow = 0;
        model.SortOrder = 0;
        model.PicPath = cover;
        new TMS.Slider().Add(model);

        tbUrl.Text = "";
        tbName.Text = "";
        tbCoverPath.Text = "";
        MessageBox.Show("添加成功！");

    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("SliderManager.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbCoverPath.Text = SiteUtility.UploadPic(upCover);
    }
}