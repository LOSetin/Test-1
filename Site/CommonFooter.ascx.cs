using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonFooter : System.Web.UI.UserControl
{
    protected string ShowQA(List<TMS.QAInfo> qaList,string cat)
    {
        //选择前三
        string format= "<p><a href='QA.aspx?cat={0}&ID={1}'>{2}</a></p>";
        //string last = "<p><a href='QA.aspx'>查看更多</a></p>";
        var list = qaList.FindAll(s => s.CatName == cat);
        string result = "";
        int i = 0;
        foreach(var model in list)
        {
            if (i >= 4)
                break;
            result = result + string.Format(format, Server.UrlEncode(cat), model.ID, model.QATitle);
            i = i + 1;
        }

        return result ;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}