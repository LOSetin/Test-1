using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

public partial class HotWords : AdminBase
{
    protected new TMS.SiteSetting bll = new TMS.SiteSetting();
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("热词设置");
        ltTips.Text = "在此，您可以设置热词搜索，设置的热词将会在搜索框下显示，最多显示10个。";
        if(!Page.IsPostBack)
        {
            TMS.SiteSettingInfo setting = bll.Select(1);
            if (setting != null)
                tbName.Text = setting.HotSearchWords;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string name = tbName.Text.Trim();
        if(name.IsNullOrEmpty())
        {
            MessageBox.Show("请输入热词");
            return;
        }

        TMS.SiteSettingInfo setting = bll.Select(1);
        if (setting != null)
            setting.HotSearchWords = name;
        bll.Update(setting);
        MessageBox.Show("设置成功！");
       
    }

 
    
}