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
    /// RadioButton字段列
    /// change base class from [DataControlField] to
    /// [BoundField] for allowing runtime data binding
    /// 2007-02-12, by David Gu
    /// </summary>
    public class RadioButtonField : BoundField
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
            if (cellType == DataControlCellType.DataCell)
            {
                cell.Controls.Clear();
                RadioButton rb = new RadioButton();
                rb.Enabled = !this.ReadOnly;
                cell.Controls.Add(rb);
            }
        }
        #endregion 

        #region 重载:CreateField
        protected override DataControlField CreateField()
        {
            return new RadioButtonField();            
        }
        #endregion

        #region 重载:OnDataBindField
        protected override void OnDataBindField(object sender, EventArgs e)
        {
            Control control0 = (Control)sender;
            Control control1 = null;
            Control control2 = control0.NamingContainer;
            object obj1 = this.GetValue(control2);

            if (control0.Controls.Count > 0)
            {
                control1 = control0.Controls[0];
            }

            if (!(control1 is RadioButton))
            {
                return;
            }

            if (obj1 == null || Convert.IsDBNull(obj1))
            {
                ((RadioButton)control1).Checked = false;
                ((RadioButton)control1).Enabled = true;
            }
            else if (obj1 is bool)
            {
                ((RadioButton)control1).Checked = (bool)obj1;
                ((RadioButton)control1).Enabled = true;
            }
            else
            {
                if (obj1.ToString() != "")
                {
                    if (String.Compare(obj1.ToString(), this.CheckedValue, true) == 0)
                    {
                        ((RadioButton)control1).Checked = true;
                    }
                    else
                    {
                        ((RadioButton)control1).Checked = false;
                    }
                    ((RadioButton)control1).Enabled = true;
                }
            }
            
        }
        #endregion
         
    }
}
