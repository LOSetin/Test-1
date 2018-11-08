using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DbProviderFactory factory = DbProviderFactories.GetFactory("Oracle.Managed.DataAccess");
        //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        //factory.CreateConnection();
        SqlConnection conn = new SqlConnection();
    }
}