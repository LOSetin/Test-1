namespace Arrow.Framework.WebControls
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:TreeListView runat=server></{0}:TreeListView>"), DefaultProperty("Text")]
    public class TreeListView : System.Web.UI.WebControls.GridView, IPostBackDataHandler
    {
        public TreeListView()
        {
            base.AllowPaging = false;
            base.AllowSorting = false;
            base.AutoGenerateColumns = false;

        }

        #region Tree的属性设置
        private int _nodeColumnIndex;
        /// <summary>
        /// 显示树型的列 Index
        /// </summary>
        public int NodeColumnIndex
        {
            get { return this._nodeColumnIndex; }
            set
            {
                _nodeColumnIndex = value;
            }
        }

        private string _columnKey;
        /// <summary>
        /// Key字段
        /// </summary>
        public string ColumnKey
        {
            get { return _columnKey; }
            set
            {
                _columnKey = value;
            }
        }

        private string _parentKey;
        /// <summary>
        /// 指向父节点的字段
        /// </summary>
        public string ParentKey
        {
            set
            {
                _parentKey = value;
            }
        }

        private string _sortKey;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortKey
        {
            set { _sortKey = value; }
        }

        private object _rootNodeFlag;
        /// <summary>
        /// 根节点的标记 这里采用 ParentKey为什么字符
        /// </summary>
        public object RootNodeFlag
        {
            set
            {
                _rootNodeFlag = value;
            }
        }

        private static string _treeImageFolder = "/Images/Tree/";
        public static string TreeImageFolder
        {
            get
            {
                return _treeImageFolder;
            }
            set
            {
                _treeImageFolder = value;
            }
        }

        private int _expendDepth = 0;
        /// <summary>
        /// 展开的深度
        /// </summary>
        public int ExpendDepth
        {
            get
            {
                return _expendDepth;
            }
            set { _expendDepth = value; }
        }
        #endregion

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                DataTable dtSource = new DataTable();
                if (value is DataSet && ((DataSet)value).Tables.Count > 0)
                {
                    DataSet ds = value as DataSet;
                    dtSource = OrderData(ds.Tables[0]);
                }
                else
                {
                    throw new Exception("DataSource is not DataSet!");
                }
                base.DataSource = dtSource;
            }
        }

        DataTable OrderData(DataTable dtSource)
        {
            DataTable dtResult = dtSource.Clone();
            dtResult.Columns.Add("TreeListView$Row$Depth", typeof(int));
            dtResult.Columns.Add("TreeListView$Row$IsLeaf", typeof(bool));
            dtResult.Columns.Add("TreeListView$Row$IsBottom", typeof(bool));
            dtResult.Columns.Add("TreeListView$Row$ParentRow", typeof(DataRow));
            dtResult.Columns.Add("TreeList$ViewRow$ClientID", typeof(string));
            RecursionOrderData(dtSource, dtResult, _rootNodeFlag, -1, null);
            return dtResult;
        }
      
        string FormatToRowFilter(object val)
        {
            Type type = val.GetType();
            if (type == typeof(string))
            {
                return string.Format("'{0}'", val.ToString().Replace("'", "''"));
            }
            else if (type == typeof(Guid))
            {
                return string.Format("'{0}'", val);
            }
            else if (type.IsValueType)
            {
                return val.ToString();
            }
            else
            {
                return string.Format("'{0}'", val.ToString().Replace("'", "''"));
            }
        }
  
        bool RecursionOrderData(DataTable dtSource, DataTable dtResult, object parentID, int depth, DataRow parentDatarow)
        {
            DataView dv = new DataView(dtSource);
            dv.RowFilter = string.Format("{0}={1}", _parentKey, FormatToRowFilter(parentID));
            dv.Sort = _sortKey;
            DataRow dr = null;
            depth++;
            for (int i = 0; i < dv.Count; i++)
            {
                dr = dtResult.NewRow();

                for (int j = 0; j < dv[i].Row.ItemArray.Length; j++)
                {
                    dr[j] = dv[i][j];
                }

                if (i == dv.Count - 1) //isBottom
                {
                    dr["TreeListView$Row$IsBottom"] = true;
                }
                else
                {
                    dr["TreeListView$Row$IsBottom"] = false;
                }
                dr["TreeListView$Row$Depth"] = depth;
                dr["TreeListView$Row$ParentRow"] = parentDatarow;
                if (depth == 0)
                {
                    dr["TreeList$ViewRow$ClientID"] = Guid.NewGuid().ToString();
                }
                else
                {
                    dr["TreeList$ViewRow$ClientID"] = parentDatarow["TreeList$ViewRow$ClientID"].ToString() + "/" + Guid.NewGuid().ToString();
                }

                dtResult.Rows.Add(dr);
                dr["TreeListView$Row$IsLeaf"] = !RecursionOrderData(dtSource, dtResult, dv[i][_columnKey], depth, dr);
            }

            return dv.Count > 0;
        }

        public override bool AllowPaging
        {
            get
            {
                return base.AllowPaging;
            }
            set
            {
                base.AllowPaging = false;
            }
        }

        public override bool AutoGenerateColumns
        {
            get
            {
                return base.AutoGenerateColumns;
            }
            set
            {
                base.AutoGenerateColumns = false;
            }
        }

        #region 重载:CreateRow
        protected override GridViewRow CreateRow(int rowIndex, int dataSourceIndex, DataControlRowType rowType, DataControlRowState rowState)
        {
            return new TreeListViewRow(rowIndex, dataSourceIndex, rowType, rowState);
        }
        #endregion

        #region 重写:Rows
        private TreeListViewRowCollection _rowsCollection;
        [Browsable(false)]
        public new TreeListViewRowCollection Rows
        {
            get
            {
                ArrayList _rowsArray = new ArrayList();
                for (int i = 0; i < base.Rows.Count; i++)
                {
                    _rowsArray.Add((TreeListViewRow)base.Rows[i]);
                }
                this._rowsCollection = new TreeListViewRowCollection(_rowsArray);
                return this._rowsCollection;
            }
        }
        #endregion

        #region 重载:OnInit
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresPostBack(this);

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("JS_TreeListView"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "JS_TreeListView", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Arrow.Framework.WebControls.TreeListView.Resource.JS_TreeListView.js"));
            }
        }
        #endregion

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return false;
        }

        public void RaisePostDataChangedEvent()
        {

        }

        #endregion

        #region 方法:RenderCheckBoxExField
        /// <summary>
        /// 处理CheckBoxExField类型的列
        /// </summary>
        private void RenderCheckBoxExField()
        {
            if (!this.ShowHeader && !this.ShowFooter)
            {
                return;
            } 

            foreach (DataControlField field in Columns)
            {
                if (field is CheckBoxExField)
                {
                    int checkBoxExFieldIndex = Columns.IndexOf(field) + this.GetAutoGenerateButtonCount();
                    foreach (GridViewRow row in Rows)
                    {
                        if (row.RowType == DataControlRowType.Header)
                        {
                            row.Cells[checkBoxExFieldIndex].Controls.Clear();
                        }
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            row.Cells[checkBoxExFieldIndex].Controls[0].ID = "cbChoose";
                            ((CheckBox)row.Cells[checkBoxExFieldIndex].Controls[0]).Attributes.Add("onclick", "ChooseTree(this);");
                        }
                    }

                    #region 注册脚本
                    string script = @"
    var modifyId = """" ;
    var choosedId = """";
    var choosedIndex; 
    function ChooseTree(obj)
	{
	    var cTrId = obj.parentElement.parentElement.parentElement.id;
	    var treeTable = document.getElementById("""+this.ClientID+@""");
	    for( var i = 0; i < treeTable.rows.length; i++ )
	    {
	        if( treeTable.rows[i].id.indexOf(cTrId) != -1 && treeTable.rows[i].id != cTrId )
	        {
	            document.getElementById(treeTable.rows[i].id+""_cbChoose"").checked = obj.checked;
	        }
	    }
	    choosedId = """";
	    choosedIndex = new Array();
	    for( var i = 1; i < treeTable.rows.length; i++ )
	    {
	        if( document.getElementById(treeTable.rows[i].id+""_cbChoose"").checked )
	        {
	            choosedId += treeTable.rows[i].id.substring(treeTable.rows[i].id.lastIndexOf(""/"")+1) + "","";
	            choosedIndex.push(i);
	        }
	    }
	    
	    choosedId = choosedId.substring(0,choosedId.length-1); 
	}";
                    if (!Page.ClientScript.IsStartupScriptRegistered("TreeListView_CheckBoxExField"))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "TreeListView_CheckBoxExField", script, true);
                    }
                    #endregion
                }
            }
        }
        #region 方法:GetAutoGenerateButtonCount
        private int GetAutoGenerateButtonCount()
        {
            int num = 0;
            if (this.AutoGenerateDeleteButton || this.AutoGenerateEditButton || this.AutoGenerateSelectButton)
            {
                num = 1;
            }
            return num;
        }
        #endregion
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            RenderCheckBoxExField();
            base.Render(writer);
        }
    }
}
     

