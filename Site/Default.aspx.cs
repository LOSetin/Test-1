using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class _Default : UIBase
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    /// <summary>
    /// 显示热卖标题
    /// </summary>
    /// <returns></returns>
    protected string showHotSell()
    {
        string result = "";
        string format = "<li {0}>{1}</li>";
        List<LineCatInfo> hotCats = getHotSellCats();
        int i = 0;
        foreach(LineCatInfo cat in hotCats)
        {
            if (i == 0)
                result += string.Format(format, "class=\"on\"", cat.Name);
            else
                result += string.Format(format, "", cat.Name);
            i = i + 1;
        }
        return result;
    }

    /// <summary>
    /// 显示热卖图片和文字
    /// </summary>
    /// <returns></returns>
    protected string showHotSellItem()
    {
        string start= "<div class=\"dqrm_hot\">";
        string end = "</div>";
        string picStart = "<div class=\"bd\"><ul>";
        string picEnd = "</ul></div>";
        string picFormat = "<li><a href=\"{0}\"><img src=\"{1}\" alt=\"\"></a></li>";
        string picContent = "";
        string textStart = "<div class=\"hd\"><ul>";
        string textEnd = "</ul></div>";
        string textFormat = "<li {0}><i></i><a href =\"{1}\" >{2}</a><span>￥{3} </span></li>";
        string textClass = "class=\"on\"";
        string textContent = "";
        string result = "";

        List<LineCatInfo> hotCats = getHotSellCats();

        foreach(LineCatInfo cat in hotCats)
        {
            List<LineInfo> catLines = LineBLL.SelectLineList(4, "IsDel=0 And SecondCatID=" + cat.ID, "ID desc");
            int i = 0;
            picContent = "";
            textContent = "";
            foreach (LineInfo line in catLines)
            {
                if (i == 0) textClass = "class=\"on\""; else textClass = "";
                picContent += string.Format(picFormat, "LineDetail.aspx?id=" + line.ID,getRawCover(line.CoverPath));
                textContent += string.Format(textFormat,textClass, "LineDetail.aspx?id=" + line.ID, line.Name, line.MinPrice);
                i = i + 1;
            }
            result += start + picStart + picContent + picEnd + textStart + textContent + textEnd + end;
        }

        return result;
    }

    /// <summary>
    /// 显示幻灯
    /// </summary>
    /// <returns></returns>
    protected string showSlider()
    {
        List<SliderInfo> sliders = new Slider().SelectList("1=1 order by sortorder");
        string headStart = "<div class=\"head\"><ul>";
        string headEnd = "</ul></div>";
        string headFormat = "<li>{0}</li>";
        string picStart = "<div class=\"body\"><ul>";
        string picEnd = "</ul></div>";
        string picFormat = "<li><a href=\"{0}\"><img src=\"{1}\" alt=\"\"></a></li>";
        string headString = "";
        string picString = "";
        foreach (SliderInfo slider in sliders)
        {
            headString += string.Format(headFormat, slider.Name);
            picString += string.Format(picFormat, slider.Url, getRawCover(slider.PicPath));
        }

        return headStart + headString + headEnd + picStart + picString + picEnd;
    }

    /// <summary>
    /// 显示热卖排行
    /// </summary>
    /// <returns></returns>
    protected string showHotSellSort()
    {
        string headStart = "<div class=\"hd\"><ul>";
        string headEnd = "</ul></div>";
        string headFormat = "<li {0}>{1}</li>";
        string headOn = "class=\"on\"";
        string itemStart = "<div class=\"bd\">";
        string itemEnd = "</div>";
        string itemFormat = "<li><a href=\"{0}\">{1}</a><span>￥{2}</span></li>";

        string headString = "";
        string itemString = "";
        List<LineCatInfo> hotSortCats = LineBLL.SelectCatList("IsDel=0 And IsHotSort=1 Order by SortOrder");
        int i = 0;
        foreach(LineCatInfo cat in hotSortCats)
        {
            if (i > 0) headOn = "";
            headString += string.Format(headFormat, headOn, cat.Name);
            i = i + 1;
            List<LineInfo> lines = LineBLL.SelectLineList(10, "IsDel=0 And FirstCatID=" + cat.ID, "IsHot desc,ID desc");
            itemString += "<ul>";
            foreach(LineInfo line in lines)
            {
                itemString += string.Format(itemFormat, "LineDetail.aspx?ID=" + line.ID, line.Name, line.MinPrice);
            }
            itemString += "</ul>";
        }

        return headStart+headString+headEnd+itemStart+itemString+itemEnd;
    }

    protected List<LineCatInfo> lineCatsShowIndex()
    {
        return LineBLL.SelectCatList(2, "ParentID=0 And IsDel=0 And IsShowIndex=1", "SortOrder");
    }

    /// <summary>
    /// 显示分类模块
    /// </summary>
    /// <param name="cat">一级分类实体</param>
    /// <returns></returns>
    protected string showModule(LineCatInfo cat)
    {
        if (cat == null) return "";
        string headFormat="<div class=\"head\"><span>{0}</span><ul class=\"clearfix\">{1}</ul></div>";
        string itemFormat = " <li {0}>{1}</li>";
        string onClass = "class=\"on\"";
        string itemString = "";
        string headString = "";

        string bodyFormat = "<div class=\"body\">{0}</div>";
        string bodyString = "";
        string bodyItemFormat = "<li><div class=\"pic\"><a href = \"{0}\" ><img src=\"{1}\" alt=\"\"></a></div><p><a href = \"{0}\" >{2}</a></p><span><i>￥</i>{3}</span></li>";

        List<LineCatInfo> sonCats = LineBLL.SelectCatList("IsDel=0 And ParentID=" + cat.ID+" Order by SortOrder");
        int i = 0;
        foreach(LineCatInfo scat in sonCats)
        {
            if (i > 0) onClass = "";
            itemString += string.Format(itemFormat, onClass, scat.Name);

            List<LineInfo> lines = LineBLL.SelectLineList(10, "IsDel=0 And SecondCatID=" + scat.ID, "ID desc");

            bodyString += "<ul class=\"clearfix idx_lst\">";
            foreach (LineInfo line in lines)
            {
                bodyString += string.Format(bodyItemFormat, "Line.aspx?ID=" + line.ID, getRawCover(line.CoverPath), line.Name, line.MinPrice);
            }
            bodyString += "</ul>";

            i = i + 1;
        }
        //头部html
        headString = string.Format(headFormat, cat.Name, itemString);
        //bodyhtml
        string body = string.Format(bodyFormat, bodyString);



        return headString + body;
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}