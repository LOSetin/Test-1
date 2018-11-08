using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;
using System.Data;

public partial class ScheduleEdit : AdminBase
{
    protected int ScheduleID { get { return GetUrlInt("id"); } }
    protected int LineID { get { return GetUrlInt("LineID"); } }
    protected LineInfo MyLine { get { return LineBLL.SelectLine( LineID ); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        ltLineName.Text = MyLine == null ? "" : MyLine.Name;
        if (ScheduleID == 0)
        {
            SetNav("添加行程");
            ltTips.Text = "在此，您可以添加行程";
            btnSubmit.Text = "确定添加";
        }
        else
        {
            SetNav("修改行程");
            ltTips.Text = "在此，您可以修改行程";
            btnSubmit.Text = "确定修改";
            btnClean.Visible = true;
            if(!Page.IsPostBack)
            {
                BindModel();
                
            }
        }
        AddSubNav("线路管理-LineManager.aspx","行程管理-ScheduleManager.aspx?id="+MyLine.ID);

       
    }

    protected void BindModel()
    {
        var model = LineBLL.SelectDetail(ScheduleID);
        if (model == null) return;
        tbSort.Text = model.SortOrder.ToString();
        tbName.Text = model.Title;
        string[] covers = model.DayPics.Split('|');
        TextBox[] controls = { tbCoverPath, tbCoverPath2, tbCoverPath3 };
        int count = covers.Length > 3 ? 3 : covers.Length;
        for(int i=0;i<count;i++)
        {
            controls[i].Text = covers[i];
        }

        tbRemarks.Text = model.DayDesc;
        tbTips.Text = model.WarmTips;
        tbHotel.Text = model.Remarks;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int sortOrder = tbSort.Text.ToArrowInt();
        string name = tbName.Text.Trim();
        string cover1 = tbCoverPath.Text.Trim();
        string cover2 = tbCoverPath2.Text.Trim();
        string cover3 = tbCoverPath3.Text.Trim();
        string cover = cover1 + "|" + cover2 + "|" + cover3;

        
        string desc = tbRemarks.Text.Trim();
        string tips = tbTips.Text.Trim();

        if ((sortOrder <= 0).ValidateSuccess("行程次序不正确！"))
            return;

        if (name.ValidateIsNullOrEmpty("请输入行程标题！"))
            return;

        if(ScheduleID==0)
        {
            //添加
            var model = new LineDetailInfo();
            model.AddTime = DateTime.Now;
            model.AddUserName = CurrentAdmin.UserName;
            model.AddUserRealName = CurrentAdmin.RealName;
            model.DayDesc = desc;
            model.DayPics = cover;
            model.LineID = LineID;
            model.SortOrder = sortOrder;
            model.Title = name;
            model.WarmTips = tips;
            model.Remarks = tbHotel.Text.Trim();
            LineBLL.AddDetail(model);
            MessageBox.Show("添加成功！", CurrentUrl);
        }
        else
        {
            var model = LineBLL.SelectDetail(ScheduleID);
            if (model == null) return;
            model.DayDesc = desc;
            model.DayPics = cover;
            model.LineID = LineID;
            model.SortOrder = sortOrder;
            model.Title = name;
            model.WarmTips = tips;
            model.Remarks = tbHotel.Text.Trim();
            LineBLL.UpdateDetail(model);
            MessageBox.Show("修改成功！");

        }


    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("ScheduleManager.aspx?id=" + MyLine.ID);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbCoverPath.Text = SiteUtility.UploadPic(upCover);
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        tbCoverPath2.Text = SiteUtility.UploadPic(upCover2);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        tbCoverPath3.Text = SiteUtility.UploadPic(upCover3);
    }

    protected void btnPlane_Click(object sender, EventArgs e)
    {
        tbName.Text += "[飞机]";
    }

    protected void btnBus_Click(object sender, EventArgs e)
    {
        tbName.Text += "[汽车]";
    }

    protected void btnShip_Click(object sender, EventArgs e)
    {
        tbName.Text += "[轮船]";
    }
}