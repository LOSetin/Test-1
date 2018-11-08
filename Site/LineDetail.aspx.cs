using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using TMS;

public partial class LineDetail : UIBase
{
    protected LineInfo CurrentLine = null;
    protected List<TravelGroupInfo> Groups = null;
    protected int CurrentGroupID = HttpContext.Current.Request["GroupID"].ToArrowInt();
    protected TravelGroupInfo CurrentGroup = null;

    protected string JsonInfo()
    {
        List<GroupCurtInfo> list = new List<GroupCurtInfo>();
        foreach(TravelGroupInfo group in Groups)
        {
            list.Add(GroupBLL.GroupToCurt(group));
        }
       return Arrow.Framework.JsonHelper.JsonSerializer(list);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentLine = LineBLL.SelectLine(GetUrlInt("ID"));
        if (CurrentLine == null)
            Response.Redirect("Default.aspx");
        else
        {
            Groups = new TravelGroup().SelectList("IsDel=0 And LineID=" + CurrentLine.ID+" Order By GoDate");
            //绑定团期
            ddlGoDate.Items.Clear();
            foreach(TravelGroupInfo group in Groups)
            {
                ddlGoDate.Items.Add(new ListItem(group.GoDate.ToDateOnlyString(), group.ID.ToString()));
            }
            ddlGoDate.SelectedValue = CurrentGroupID.ToString();
            CurrentGroup = Groups.Find(s => s.ID == ddlGoDate.SelectedValue.ToArrowInt());
            if(CurrentGroup==null)
            {
                //Response.Redirect("Default.aspx");
                CurrentGroup = new TravelGroupInfo();
            }
        }
    }
}