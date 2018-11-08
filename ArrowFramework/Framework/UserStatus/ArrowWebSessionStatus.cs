using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Arrow.Framework
{
    /// <summary>
    /// 使用Session进行状态管理
    /// </summary>
    public class ArrowWebSessionStatus<T> : IArrowUserStatus<T> where T : class
    {
        #region 实现接口
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expire">过期时间（以分钟为单位）</param>
        public void SetValue(string key, T t , int expire=0) 
        {
            if (HttpContext.Current!= null)
            {
                HttpContext.Current.Session[key] = t;
                if (expire > 0)
                    HttpContext.Current.Session.Timeout = expire;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public T GetValue(string key) 
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session[key] != null)
                {
                    return HttpContext.Current.Session[key] as T;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public void Remove(string key)
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[key] = null;
            }
        }
        #endregion
    }
}
