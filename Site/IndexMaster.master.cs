using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;

public partial class IndexMaster : System.Web.UI.MasterPage
{
    public bool IsIndexPage
    {
        set; get;
    }

    public string ShowBodyCss()
    {
        if (IsIndexPage)
            return "class=\"idx_bg\"";
        return "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected string ShowCat()
    {
        List<LineCatInfo> allCats = new LineCat().SelectList("IsDel=0 Order By SortOrder");
        IEnumerable<LineCatInfo> parentCats = allCats.FindAll(s => s.ParentID == 0).Take(6);
        string result = "";
        foreach(LineCatInfo cat in parentCats)
        {
            //外面菜单的格式
            string outerFormat = @"<dt>
                            	<span><a href='{0}'>{1}</a></span>
                                <p>{2}</p>
                                </dt> ";
            string menuFormat = "<a href = '{0}' > {1} </a>";

            List<LineCatInfo> sonCats = allCats.FindAll(s => s.ParentID == cat.ID);
            string son = "";
            IEnumerable<LineCatInfo> top5Cats = sonCats.Take(5);
            foreach(LineCatInfo cat2 in top5Cats)
            {
                son += string.Format(menuFormat, "Line.aspx?fid=" + cat.ID + "&sid=" + cat2.ID, cat2.Name);
            }
            string outer = string.Format(outerFormat, "Line.aspx?fid=" + cat.ID, cat.Name, son);

            string hiderFormat = "<dd><ul>{0}</ul></dd>";
            string lineFormat = "<li><p>{0}</p></li>";

            int i = 1;
            string menuItem = "";
            string menuItemList = "";
            foreach(LineCatInfo cat3 in sonCats)
            {
                menuItem = menuItem + string.Format(menuFormat, "Line.aspx?sid=" + cat3.ID, cat3.Name);
                if (i % 6 == 0)
                {
                    menuItemList += string.Format(lineFormat, menuItem);
                    menuItem = "";
                }
                i = i + 1;
            }
            //如果不是刚好每行6个，则剩下的凑成最后一行
            if(!menuItem.IsNullOrEmpty())
            {
                menuItemList += string.Format(lineFormat, menuItem);
            }

            string hiderMenu = string.Format(hiderFormat, menuItemList);
            result += outer + hiderMenu;
        }

        return result;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}
