namespace Arrow.Framework.WebControls
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Reflection;
    using System.Data;

    public class TreeListViewRow : GridViewRow
    {
        public TreeListViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
            : base(rowIndex, dataItemIndex, rowType, rowState)
        {

        }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.RowType == DataControlRowType.DataRow)
            {
                if (this.Parent.Parent is TreeListView)
                {
                    TreeListView treeListView = this.Parent.Parent as TreeListView;
                    if (treeListView.DataSource == null) return;
                    DataRow dr = ((DataTable)treeListView.DataSource).Rows[this.DataItemIndex] as DataRow;
                    string str = GetTreeNodeImg(dr, Convert.ToBoolean(dr["TreeListView$Row$IsLeaf"]), Convert.ToBoolean(dr["TreeListView$Row$IsBottom"]), treeListView.ExpendDepth, Convert.ToInt32(dr["TreeListView$Row$Depth"]));
                    this.Cells[treeListView.NodeColumnIndex].Text = str + this.Cells[treeListView.NodeColumnIndex].Text;
                    this.Attributes.Add("id", dr["TreeList$ViewRow$ClientID"].ToString());
                    if (treeListView.ExpendDepth > 0)
                    {
                        this.Style["display"] = treeListView.ExpendDepth >= Convert.ToInt32(dr["TreeListView$Row$Depth"]) ? "" : "none";
                    }
                    else
                    {
                        this.Style["display"] = Convert.ToInt32(dr["TreeListView$Row$Depth"]) >= 1 ? "none" : "";
                    }
                }
            }
            
        }

        #region 获取Tree的图片
        string GetTreeNodeImg(DataRow dr, bool isLeaf, bool isBottom,int expendDepth,int currentDepth)
        {
            return GetTreeNodeOtherImg(dr) + GetTreeNodeLastImg(isLeaf, isBottom,expendDepth,currentDepth);
        }
        string GetTreeNodeOtherImg(DataRow dr)
        {
            if (dr["TreeListView$Row$ParentRow"] != null&&!dr["TreeListView$Row$ParentRow"].Equals(DBNull.Value))
            {
                DataRow drParentRow = dr["TreeListView$Row$ParentRow"] as DataRow;
                bool parentIsBottom = Convert.ToBoolean(drParentRow["TreeListView$Row$IsBottom"]);
                if (parentIsBottom)
                {
                    return GetTreeNodeOtherImg(drParentRow) + string.Format("<img src={0} align=absmiddle>", TreeListView.TreeImageFolder + "white.gif");
                }
                else
                {
                    return GetTreeNodeOtherImg(drParentRow) + string.Format("<img src={0} align=absmiddle>", TreeListView.TreeImageFolder + "i.gif");
                }

            }
            else
            {
                return string.Empty;
            }
        }
        string GetTreeNodeLastImg(bool isLeaf, bool isBottom,int expendDepth,int currentDepth)
        {
            //最后靠近的那个Image
            string lastImg = string.Empty;
            if (isLeaf)
            {
                if (isBottom)
                {
                    lastImg = string.Format("<img src={0} align=\"absmiddle\">", TreeListView.TreeImageFolder + "l.gif");
                }
                else
                {
                    lastImg = string.Format("<img src={0} align=\"absmiddle\">",TreeListView.TreeImageFolder + "t.gif");
                }
            }
            else
            {
                if (currentDepth >= expendDepth)
                {
                    if (isBottom)
                    {
                        lastImg = string.Format("<img src={0} align=\"absmiddle\" onclick='ClickNode(this,true,\"{1}\");' style=\"cursor: hand\">", TreeListView.TreeImageFolder + "lplus.gif", this.Parent.Parent.ClientID);
                    }
                    else
                    {
                        lastImg = string.Format("<img src={0} align=\"absmiddle\" onclick='ClickNode(this,false,\"{1}\");' style=\"cursor: hand\">", TreeListView.TreeImageFolder + "tplus.gif", this.Parent.Parent.ClientID);
                    }
                }
                else
                {
                    if (isBottom)
                    {
                        lastImg = string.Format("<img src={0} align=\"absmiddle\" onclick='ClickNode(this,true,\"{1}\");' style=\"cursor: hand\">", TreeListView.TreeImageFolder + "lminus.gif", this.Parent.Parent.ClientID);
                    }
                    else
                    {
                        lastImg = string.Format("<img src={0} align=\"absmiddle\" onclick='ClickNode(this,false,\"{1}\");' style=\"cursor: hand\">", TreeListView.TreeImageFolder + "tminus.gif", this.Parent.Parent.ClientID);
                    }
                }
            }
            return lastImg;

        }
        #endregion


    }
}