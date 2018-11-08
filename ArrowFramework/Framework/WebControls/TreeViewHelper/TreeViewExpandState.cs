using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 树形菜单的展开状态保存与还原
    /// </summary>
    public class TreeViewExpandState
    {
        private int _restoreViewIndex;

        public TreeViewExpandState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 保存树形菜单展开状态
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="key"></param>
        public void SaveTreeView(TreeView treeView, string key)
        {
            //保存TreeView展开状态
            List<bool?> list = new List<bool?>();
            SaveTreeViewExpandedState(treeView.Nodes, list);
            HttpContext.Current.Session[key + treeView.ID] = list;
        }

        /// <summary>
        /// 还原树形菜单展开状态
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="key"></param>
        public void RestoreTreeView(TreeView treeView, string key)
        {
            List<bool?> list = new List<bool?>();
            if (HttpContext.Current.Session[key + treeView.ID] != null)
            {
                list = HttpContext.Current.Session[key + treeView.ID] as List<bool?>;
            }
            _restoreViewIndex = 0;
            RestoreTreeViewExpandedState(treeView.Nodes, list);
        }

        #region 私有方法

        //保存节点展开状态
        private void SaveTreeViewExpandedState(TreeNodeCollection nodes, List<bool?> list)
        {
            foreach (TreeNode node in nodes)
            {
                list.Add(node.Expanded);
                if (node.ChildNodes.Count > 0)
                {
                    SaveTreeViewExpandedState(node.ChildNodes, list);
                }
            }
        }

        //还原节点展开状态
        private void RestoreTreeViewExpandedState(TreeNodeCollection nodes, List<bool?> list)
        {
            foreach (TreeNode node in nodes)
            {
                if (_restoreViewIndex >= list.Count)
                {
                    return;
                }
                //还原TreeNode的展开状态
                node.Expanded = list[_restoreViewIndex];
                _restoreViewIndex += 1;
                if (node.ChildNodes.Count > 0)
                {
                    RestoreTreeViewExpandedState(node.ChildNodes, list);
                }
            }
        }
        #endregion

    }
}