using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;

public partial class Master_Logout : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (CurrentUser != null)
        //{
        //    RPMS.BLL.LoginHelper.SetLoginUserInfo(null);
        //}
        MessageBox.Show("感谢使用，您已经安全退出！", "Login.aspx");
    }
}