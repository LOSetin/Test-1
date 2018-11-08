using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class SiteTest_Line : SiteBase
{
    protected int fid
    {
        get
        {
            int pid = GetUrlInt("fid");
            if (pid == 0)
            {
                var cat = LineBLL.SelectTop1Cat();
                if (cat != null)
                    pid = cat.ID;
            }
            return pid;
        }
    }

    protected int sid { get { return GetUrlInt("sid"); } }

    protected void ShowLevel1Cat()
    {
        ltLevel1.Text = "";
        var catList = LineBLL.SelectCatList("IsDel=0 And ParentID=0");
        foreach(var model in catList)
        {
            ltLevel1.Text += "<a href='line.aspx?fid=" + model.ID + "'>" + model.Name + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
    }

    protected void ShowLevel2Cat()
    {
        ltLevel2.Text = "";
        var catList = LineBLL.SelectCatList("IsDel=0 And ParentID="+fid+" Order by SortOrder asc");
        foreach (var model in catList)
        {
            ltLevel2.Text += "<a href='line.aspx?fid=" + fid + "&sid=" + model.ID + "'>" + model.Name + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
    }

    protected List<LineInfo> GetLines()
    {
        string condition = "IsDel=0";
        if (fid > 0)
            condition = condition + " And FirstCatID=" + fid;
        if (sid > 0)
            condition = condition + " And SecondCatID=" + sid;
        return LineBLL.SelectLineList(condition);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowTitle("旅游线路");
        if(!Page.IsPostBack)
        {
            ShowLevel1Cat();
            ShowLevel2Cat();
        }
    }
}