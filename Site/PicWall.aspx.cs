using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class PicWall : UIBase
{
    protected string ShowPics(int pageIndex)
    {
        List<PicWallInfo> pics = PicWallBLL.SelectList("1=1", "ID Desc", pageIndex, 8);
        string format= "<li><a title=\"{0}\" class=\"a1\" href =\"{1}\" target =\"_blank\" rel =\"example_group\" ><img width=\"230\" height =\"250\" alt =\"{0}\" src =\"{1}\" ></a><span class=\"s1\" ></span><span class=\"s2\" ><a>{0}</a></span></li>";
        string result = "";
        foreach(PicWallInfo model in pics)
        {
            result += string.Format(format, model.Name, getRawCover(model.CoverPath));
        }

        return result;
    }

    protected int GetRecordCount()
    {
        return new TMS.PicWall().GetCount("1=1");
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {

    }
}