namespace Arrow.Framework.WebControls
{ 
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Web.UI.WebControls;

    public class TreeListViewRowCollection : GridViewRowCollection
    {
        public TreeListViewRowCollection(ArrayList rows)
            : base(rows)
        {        
        }

        public void CopyTo(TreeListViewRow[] array, int index)
        {
            ((ICollection)this).CopyTo(array, index);
            
        }


        public new TreeListViewRow this[int index]
        {
            get
            {

                return (TreeListViewRow)base[index];
            }
        }

    }
}

