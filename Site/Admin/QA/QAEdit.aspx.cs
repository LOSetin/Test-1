using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;

public partial class PromotionEdit : AdminBase
{
    protected int QAID = HttpContext.Current.Request.QueryString["ID"].ToArrowInt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            QACat.BindToDrowdownList(ddlCat);
        }
        if (QAID == 0)
        {
            SetNav("添加问答");
            ltTips.Text = "在此，您可以添加问答";
            btnSubmit.Text = "确定添加";
        }
        else
        {
            SetNav("修改问答");
            ltTips.Text = "在此，您可以修改问答";
            btnSubmit.Text = "确定修改";
            if(!Page.IsPostBack)
            {
                BindModel();
            }
        }

       
    }

    protected void BindModel()
    {
        QAInfo model = new QA().Select(QAID);
        if (model != null)
        {
            tbName.Text = model.QATitle;
            tbContent.Text = model.QAContent;
            ddlCat.SelectedValue = model.CatName;
        }

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string title = tbName.Text.Trim();
        string catName = ddlCat.SelectedValue;
        string content = tbContent.Text;
        var bll = new TMS.QA();

        if(title.IsNullOrEmpty())
        {
            MessageBox.Show("标题必须填写！");
            return;
        }

        if(content.IsNullOrEmpty())
        {
            MessageBox.Show("内容必须填写！");
            return;
        }

        if(QAID==0)
        {
            //添加
            QAInfo model = new QAInfo();
            model.QATitle = title;
            model.CatName = catName;
            model.QAContent = content;
            bll.Add(model);
            MessageBox.Show("添加成功！", CurrentUrl);

        }
        else
        {
            QAInfo model = bll.Select(QAID);
            if(model==null)
            {
                MessageBox.Show("该问答不存在！", true);
                return;
            }
            model.QATitle = title;
            model.CatName = catName;
            model.QAContent = content;
            bll.Update(model);
            MessageBox.Show("修改成功！");

        }
      
       

    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("QAManager.aspx");
    }


}