using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 增强型CheckBox字段列
    /// </summary>
    public class CheckBoxExField : CheckBoxField
    {
        #region 属性:CheckedValue
        private string _CheckedValue = "1";
        [Category("Appearance")]
        [Description("指定CheckBox选中状态的值")]
        public string CheckedValue
        {
            get { return _CheckedValue; }
            set { _CheckedValue = value; }
        }
        #endregion

        #region 重载:InitializeCell
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType != DataControlCellType.DataCell)
            {
                cell.Controls.Clear();
                CheckBox cb = new CheckBox();
                if (cellType == DataControlCellType.Header)
                {
                    cb.Text = this.HeaderText;
                }
                cell.Controls.Add(cb);
            }
            if (((CheckBoxExField)cell.ContainingField).DataField == null || ((CheckBoxExField)cell.ContainingField).DataField=="")
            {
                cell.Controls.Clear();
                CheckBox cb = new CheckBox();
                if (cellType == DataControlCellType.DataCell)
                {
                    cb.Enabled = !this.ReadOnly;
                }                
                cell.Controls.Add(cb);
            }
        }
        #endregion 

        #region 重载:DataControlField
        protected override DataControlField CreateField()
        {
            return new CheckBoxExField();            
        }
        #endregion

        #region 重载:OnDataBindField
        protected override void OnDataBindField(object sender, EventArgs e)
        {
            Control control1 = (Control)sender;
            Control control2 = control1.NamingContainer;
            object obj1 = this.GetValue(control2);
            if (!(control1 is CheckBox))
            {
                throw new HttpException("CheckBoxField_WrongControlType");
            }

            if (obj1 == null || Convert.IsDBNull(obj1))
            {
                ((CheckBox)control1).Checked = false;
                ((CheckBox)control1).Enabled = true;
            }
            else if (obj1 is bool)
            {
                ((CheckBox)control1).Checked = (bool)obj1;
                ((CheckBox)control1).Enabled = true;
            }
            else
            {
                if (obj1.ToString() != "")
                {
                    if (String.Compare(obj1.ToString(), this.CheckedValue, true) == 0)
                    {
                        ((CheckBox)control1).Checked = true;                    
                    }
                    else
                    {
                        ((CheckBox)control1).Checked = false;
                    }
                    ((CheckBox)control1).Enabled = true;
                }
            }
            ((CheckBox)control1).Text = this.Text;
            

        }
        #endregion
    }
}
