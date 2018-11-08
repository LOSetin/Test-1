using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

public partial class PhotoUpload : AdminBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("上传照片");
        ltTips.Text = "在此，您可以上传照片";
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string name = tbName.Text.Trim();
        string cover = tbCoverPath.Text.Trim();
  
        if (name.ValidateIsNullOrEmpty("请输入商品名称！"))
            return;

        if (cover.ValidateIsNullOrEmpty("请上传照片！"))
            return;

        var model = new TMS.PicWallInfo();
        model.AddTime = DateTime.Now;
        model.AddUserName = CurrentAdmin.UserName;
        model.AddUserRealName = CurrentAdmin.RealName;
        model.BigPicPath = "";
        model.CoverPath = cover;
        model.IsTop = 0;
        model.Name = name;
        model.Remarks = "";
        model.SortOrder = 0;
        PicWallBLL.AddPhoto(model);

        tbName.Text = "";
        tbCoverPath.Text = "";
        MessageBox.Show("添加成功！");

    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("PhotoManager.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbCoverPath.Text = SiteUtility.UploadPic(upCover);
    }
}