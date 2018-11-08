using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

public partial class UrlRouter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["Type"].ToArrowString();
        
        switch (type.ToLower())
        {
            case "line":
                int id = Request.QueryString["id"].ToArrowInt();
                string date = Request.QueryString["date"];
                DateTime dt = date.ToArrowDateTime();
                List<TMS.TravelGroupInfo> groups = new TMS.TravelGroup().SelectList("IsDel=0 And LineID=" + id + " Order By GoDate");
                var model = groups.Find(s => s.GoDate == dt);
                if (model != null)
                    Response.Redirect("LineDetail.aspx?ID=" + id + "&GroupID=" + model.ID);
                else
                    Response.Redirect("Default.aspx");
                break;

            case "promotion":
                int id1 = Request.QueryString["id"].ToArrowInt();
                DateTime date1 = Request.QueryString["date"].ToArrowDateTime();
                int promotionID = Request.QueryString["PromotionID"].ToArrowInt();
                int lineID = Request.QueryString["LineID"].ToArrowInt();
                List<TMS.V_Promotion_GroupInfo> promotionGroups = new TMS.V_Promotion_Group().SelectList("IsDel=0 And PromotionID=" + promotionID + " And LineID=" + lineID);
                var model1 = promotionGroups.Find(s => s.GoDate == date1);
                if (model1 != null)
                    Response.Redirect("PromotionGroupDetail.aspx?ID=" + id1 + "&PromotionID=" + promotionID + "&GroupID=" + model1.GroupID + "&LineID=" + lineID);
                else
                    Response.Redirect("Default.aspx");
                break;

            case "search":
                string keywords = Request["keywords"].ToArrowString();
                if (!string.IsNullOrEmpty(keywords))
                {
                    LineSearchInfo searchModel = new LineSearchInfo();
                    searchModel.KeyWord = keywords;
                    LineSearch.GoSearch(searchModel);
                }
                Response.Redirect("Line.aspx");
                break;
            default:
               Response.Redirect("Default.aspx");
                break;
        }
      
        

    }
}