using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// ¸´Ñ¡¿ò¼¯ºÏ
    /// </summary>
    public class CheckBoxCollection : CollectionBase
    {
        public void Add(CheckBox checkbox)
        {
            this.List.Add(checkbox);
        }

        public void Remove(CheckBox checkbox)
        {
            this.List.Remove(checkbox);
        }

        public CheckBox this[int Index]
        {
            get 
            {
                if (Index >= 0 && Index < Count)
                {
                    return this.List[Index] as CheckBox;
                }
                return null;
            }
            set
            { 
                this.List[Index]=value;
            }
        }
        
  
    }
}
