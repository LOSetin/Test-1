using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 序号字段列
    /// </summary>
    public class NumberField:DataControlField
    {
        #region 属性:IsContinuousArrange
        private bool _IsContinuousArrange = true;
        /// <summary>
        /// 序号是否连续的排列(分页状态)
        /// </summary>
        [Category("Behavior")]
        [Description("序号是否连续的排列(分页状态)")]
        public bool IsContinuousArrange
        {
            get { return _IsContinuousArrange;}
            set { _IsContinuousArrange = value; }
        }
        #endregion

        #region 重载:InitializeCell
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType == DataControlCellType.DataCell)
            {

                cell.Text = Convert.ToString(rowIndex+1);
            }
        }
        #endregion

        #region 重载:DataControlField
        protected override DataControlField CreateField()
        {
            return new NumberField();            
        }
        #endregion
    }
}
