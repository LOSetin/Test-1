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

public partial class GroupEdit : AdminBase
{
    protected int GroupID { get { return GetUrlInt("id"); } }
    protected int LineID { get { return GetUrlInt("LineID"); } }
    protected LineInfo MyLine { get { return LineBLL.SelectLine( LineID ); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        ltLineName.Text = MyLine == null ? "" : MyLine.Name;
        if (GroupID == 0)
        {
            SetNav("添加旅游团");
            ltTips.Text = "在此，您可以添加旅游团";
            btnSubmit.Text = "确定添加";
        }
        else
        {
            SetNav("修改旅游团");
            ltTips.Text = "在此，您可以修改旅游团";
            btnSubmit.Text = "确定修改";
            btnClean.Visible = true;
            if(!Page.IsPostBack)
            {
                BindModel();
                
            }
        }
        AddSubNav("线路管理-LineManager.aspx","旅游团管理-GroupManager.aspx?id="+MyLine.ID);

       
    }

    protected void BindModel()
    {
        var model = GroupBLL.SelectGroup(GroupID);
        if (model == null) return;
        tbBegin.Text = model.GoDate.ToDateOnlyString();
        tbEnd.Text = model.BackDate.ToDateOnlyString();
        tbNum.Text = model.TotalNum.ToString();
        tbPrice.Text = model.OuterPrice.ToString();
        tbMemberPrice.Text = model.InnerPrice.ToString();
        tbDeposit.Text = model.Deposit.ToString();
        tbGatheringTime.Text = model.GatheringTime;
        tbGatheringPlace.Text = model.GatheringPlace;
        tbTransfer.Text = model.TransferPlace;
        tbLeader.Text = model.GruopLeader;
        tbRemain.Text = model.RemainNum.ToString();

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DateTime goDate = tbBegin.Text.Trim().ToArrowDateTime();
        DateTime backDate = tbEnd.Text.Trim().ToArrowDateTime();
        int totalNum = tbNum.Text.Trim().ToArrowInt();
        int remainNum = tbRemain.Text.Trim().ToArrowInt();
        decimal outerPrice = tbPrice.Text.Trim().ToArrowDecimal();
        decimal innerPrice = tbMemberPrice.Text.Trim().ToArrowDecimal();
        decimal deposit = tbDeposit.Text.Trim().ToArrowDecimal();
        string gatheringTime = tbGatheringTime.Text.Trim();
        string gatheringPlace = tbGatheringPlace.Text.Trim();
        string transfer = tbTransfer.Text.Trim();
        string leader = tbLeader.Text.Trim();

        if ((goDate == DateTime.MinValue).ValidateSuccess("出团日期不正确！"))
            return;
        if ((backDate == DateTime.MinValue).ValidateSuccess("回程日期不正确！"))
            return;
        if ((goDate <= DateTime.Now).ValidateSuccess("出团日期必须大于今天！"))
            return;
        if ((backDate <= goDate).ValidateSuccess("返程日期必须大于出团日期！"))
            return;
        if ((totalNum <= 0).ValidateSuccess("参团人数不正确！"))
            return;
        if ((remainNum <= 0).ValidateSuccess("剩余位数不正确！"))
            return;
        if ((outerPrice <= 0M).ValidateSuccess("价格不正确！"))
            return;
        if ((innerPrice <= 0M).ValidateSuccess("会员价不正确！"))
            return;
        if ((deposit <= 0M).ValidateSuccess("订金不正确！"))
            return;

        if (GroupID == 0)
        {
            var model = new TravelGroupInfo();
            model.AddTime = DateTime.Now;
            model.AddUserName = CurrentAdmin.UserName;
            model.AddUserRealName = CurrentAdmin.RealName;
            model.BackDate = backDate;
            model.BackTravel = "";
            model.Deposit = deposit;
            model.GatheringPlace = gatheringPlace;
            model.GatheringTime = gatheringTime;
            model.GoDate = goDate;
            model.GoTravel = "";
            model.GroupNum = ""; //团号
            model.GruopLeader = leader;
            model.InnerPrice = innerPrice;
            model.IsDel = 0;
            model.IsPublish = 0;
            model.JoinNum = 0;
            model.RemainNum = remainNum;
            model.LineID = LineID;
            model.Name = "";
            model.OuterPrice = outerPrice;
            model.PromotionNum = 0;
            model.Remarks = "";
            model.TotalNum = totalNum;
            model.TransferPlace = transfer;
            model.TravelGuide = "";
            GroupBLL.AddGroup(model);
            MessageBox.Show("添加成功！", CurrentUrl);
        }
        else
        {
            var model = GroupBLL.SelectGroup(GroupID);
            if (model == null) return;

            model.BackDate = backDate;
            model.Deposit = deposit;
            model.GatheringPlace = gatheringPlace;
            model.GatheringTime = gatheringTime;
            model.GoDate = goDate;
            model.GruopLeader = leader;
            model.InnerPrice = innerPrice;
            model.LineID = LineID;
            model.OuterPrice = outerPrice;
            model.TotalNum = totalNum;
            model.RemainNum = remainNum;
            model.TransferPlace = transfer;
            GroupBLL.UpdateGroup(model);
            MessageBox.Show("更新成功！");
        }


    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("GroupManager.aspx?id=" + MyLine.ID);
    }

  
}