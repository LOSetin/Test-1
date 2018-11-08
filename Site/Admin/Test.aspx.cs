using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

public partial class Admin_Test : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(SiteUserBLL.AdminEncrypt("admin"));

        //FieldInfo[] ps = typeof(PromotionType).GetFields(BindingFlags.Public | BindingFlags.Static);
        //for(int i=0;i<ps.Length;i++)
        //{
        //    if(ps[i].IsInitOnly && ps[i].IsPublic && ps[i].IsStatic)
        //        Response.Write(ps[i].Name + "," + ps[i].GetValue(null) + "<br/>");
        //}

        List<string> values = typeof(PromotionType).ArrowEnumValues();
        
        foreach(string s in values)
        {
            s.ResponseLine();
        }

     

    }
}