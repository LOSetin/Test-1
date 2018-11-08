using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 树形菜单所有状态（包括展开状态，节点数据等）保存与还原，将数据保存于Session中，如此不必每次都绑定树形菜单。
    /// <remarks>保存：state.SaveTreeView(tvwNodes, "TreeViewState", HttpContext.Current); 
    /// 还原：if (state.IsSaved){state.RestoreTreeView(tvwNodes, "TreeViewState", HttpContext.Current);}</remarks>
    /// </summary>
    public class TreeViewState
    {
        /// <summary>
        /// The isSaved field use to check TreeView state was saved.
        /// </summary>
        private bool isSaved;
        public bool IsSaved
        {
            get
            {
                return isSaved;
            }
            set
            {
                isSaved = value;
            }
        }

        /// <summary>
        /// The class constructor method.
        /// </summary>
        /// <param name="key"></param>
        public TreeViewState(string key)
        {
            if (null == System.Web.HttpContext.Current.Session[key])
            {
                isSaved = false;
            }
            else
            {
                isSaved = true;
            }
        }

        /// <summary>
        /// Store TreeView's state in a session.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="key"></param>
        /// <param name="context"></param>
        public void SaveTreeView(TreeView treeView, string key,HttpContext context)
        {
            context.Session[key] = treeView.Nodes;
        }

        /// <summary>
        /// Restore TreeView's state from session variable, and invoke SaveTreeView method.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="key"></param>
        /// <param name="context"></param>
        public void RestoreTreeView(TreeView treeView, string key, HttpContext context)
        {
            if (new TreeViewState(key).IsSaved)
            {
                treeView.Nodes.Clear();

                TreeNodeCollection nodes = (TreeNodeCollection)context.Session[key];
                for (int index = nodes.Count - 1; index >= 0; index--)
                {
                    treeView.Nodes.AddAt(0, nodes[index]);
                }
                this.SaveTreeView(treeView, key, context);
            }
            
        }
    }
}