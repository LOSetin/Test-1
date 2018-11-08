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
    /// ����ֶ���
    /// </summary>
    public class NumberField:DataControlField
    {
        #region ����:IsContinuousArrange
        private bool _IsContinuousArrange = true;
        /// <summary>
        /// ����Ƿ�����������(��ҳ״̬)
        /// </summary>
        [Category("Behavior")]
        [Description("����Ƿ�����������(��ҳ״̬)")]
        public bool IsContinuousArrange
        {
            get { return _IsContinuousArrange;}
            set { _IsContinuousArrange = value; }
        }
        #endregion

        #region ����:InitializeCell
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType == DataControlCellType.DataCell)
            {

                cell.Text = Convert.ToString(rowIndex+1);
            }
        }
        #endregion

        #region ����:DataControlField
        protected override DataControlField CreateField()
        {
            return new NumberField();            
        }
        #endregion
    }
}
