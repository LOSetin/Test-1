using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace Arrow.Framework
{
    /// <summary>
    /// 数据处理
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// 绑定树形数据到下拉列表
        /// </summary>
        /// <param name="tbs">树形结构数据源</param>
        /// <param name="ValueField">绑定列的Value</param>
        /// <param name="TextField">绑定列的Text</param>
        /// <param name="ParentField">数据源中表示父结点的字段名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="space">缩进的空格</param>
        /// <returns></returns>
        public static void BindTree(DropDownList ddl, DataTable tbs, string valueField, string textField, string parentField, string prefix = "├", string space = "　")
        { 
            DataTable dt=GetTypeTree(tbs,  valueField, textField,  parentField, prefix, space);
            ddl.DataSource = dt;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
        }

        /// <summary>
        /// 树形结构的下拉列表显示
        /// </summary>
        /// <param name="tbs">树形结构数据源</param>
        /// <param name="valueField">绑定列的Value</param>
        /// <param name="textField">绑定列的Text</param>
        /// <param name="parentField">数据源中表示父结点的字段名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="space">缩进的空格</param>
        /// <returns></returns>
        public static DataTable GetTypeTree(DataTable tbs, string valueField, string textField, string parentField, string prefix = "├", string space = "　")
        {
            DataTable tb = new DataTable();
            tb.Columns.Add(valueField, Type.GetType("System.String"));
            tb.Columns.Add(textField, Type.GetType("System.String"));

            //Level，指定层级，加入新row时根据其上层row加一
            tb.Columns.Add("Level", Type.GetType("System.Int32"));
            DataRow row;

            if (tbs == null)
                return tb;

            //先将顶级菜单放入tb,ParentField为0代表顶级
            DataRow[] TopRows = tbs.Select(parentField.ToString() + @" = '0'"); 
            for (int i = 0; i < TopRows.Length; i++)
            {
                row = tb.NewRow();

                row[0] = TopRows[i][valueField].ToString();//绑定列的Value
                row[1] = TopRows[i][textField].ToString();//绑定列的Text
                row[2] = 0;//0为第一级 

                tb.Rows.Add(row);
            }

            DataRow[] rows;
            for (int i = 0; i < tb.Rows.Count; i++)//tb.Rows.Count这个数字也是不断的在增大的，因为不断的插入新行
            {
                // tb进行循环
                string strSpace = space;
                for (int j = 0; j < Convert.ToInt32(tb.Rows[i][2]); j++)
                {
                    strSpace += space;
                }
                rows = tbs.Select(parentField + "=" + "'" + tb.Rows[i][0].ToString() + "'");//父级编号为此行的id
                for (int j = rows.Length - 1; j >= 0; j--)	//倒循环，结果可以正序
                {//循环嵌套对刚才的
                    row = tb.NewRow();

                    row[0] = rows[j][valueField].ToString();//绑定列的Value
                    row[1] = strSpace + prefix + rows[j][textField].ToString();//绑定列的Text
                    row[2] = Convert.ToInt32(tb.Rows[i][2]) + 1;

                    tb.Rows.InsertAt(row, i + 1);
                }
            }
            return tb;
        }

    }
}
