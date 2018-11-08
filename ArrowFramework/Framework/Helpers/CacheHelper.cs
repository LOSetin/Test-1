using System;
using System.Web;
using System.Web.Caching;
using System.Text;

namespace Arrow.Framework
{
    /// <summary>
    /// 缓存处理
    /// </summary>
    public static class CacheHelper
    {
        #region 创建缓存
        /// <summary>
        /// 简单创建/修改Cache
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        /// <param name="oCacheValue">Cache值</param>
        /// <param name="iExpires">有效期，秒数（使用的是当前时间+秒数得到一个绝对到期值）</param>
        /// <param name="priority">保留优先级，1最不会被清除，6最容易被内存管理清除（1:NotRemovable；2:High；3:AboveNormal；4:Normal；5:BelowNormal；6:Low）</param>
        public static void Add(string strCacheName, object oCacheValue, int iExpires, int priority)
        {
            if (oCacheValue == null) return;
            TimeSpan ts = new TimeSpan(0, 0, iExpires);
            CacheItemPriority cachePriority;
            switch (priority)
            {
                case 6:
                    cachePriority = CacheItemPriority.Low;
                    break;
                case 5:
                    cachePriority = CacheItemPriority.BelowNormal;
                    break;
                case 4:
                    cachePriority = CacheItemPriority.Normal;
                    break;
                case 3:
                    cachePriority = CacheItemPriority.AboveNormal;
                    break;
                case 2:
                    cachePriority = CacheItemPriority.High;
                    break;
                case 1:
                    cachePriority = CacheItemPriority.NotRemovable;
                    break;
                default:
                    cachePriority = CacheItemPriority.Default;
                    break;
            }

            HttpContext.Current.Cache.Insert(strCacheName, oCacheValue, null, DateTime.UtcNow.Add(ts), System.Web.Caching.Cache.NoSlidingExpiration, cachePriority, null);
        }

        /// <summary>
        /// 创建依赖缓存
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        /// <param name="oCacheValue">Cache值</param>
        /// <param name="dep">缓存依赖,如new CacheDependency(Server.MapPath("db.config")) </param>
        /// <param name="iExpires">过期时间，单位秒</param>
        public static void Add(string strCacheName, object oCacheValue, CacheDependency dep, int iExpires)
        {
            if (oCacheValue == null) return;
            TimeSpan ts = new TimeSpan(0, 0, iExpires);
            HttpContext.Current.Cache.Insert(strCacheName, oCacheValue, dep, DateTime.UtcNow.Add(ts), Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 创建依赖缓存
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        /// <param name="oCacheValue">Cache值</param>
        /// <param name="dep">缓存依赖,如new CacheDependency(Server.MapPath("db.config"));</param>
        public static void Add(string strCacheName, object oCacheValue, CacheDependency dep)
        {
            if (oCacheValue == null) return;
            HttpContext.Current.Cache.Insert(strCacheName, oCacheValue, dep);
        }

        #endregion

        #region 选取缓存值
        /// <summary>
        /// 读取Cache对象的值，如果Cache不存在，则返回Null。
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        /// <returns>如果Cache存在，则返回Cache对象，否则返回Null</returns>
        public static object Select(string strCacheName)
        {
                return HttpContext.Current.Cache[strCacheName];
        }

        /// <summary>
        /// 读取Cache对象的值
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="strCacheName">Cache名称</param>
        /// <returns>返回T类型的值，T必须为class类型，转换失败则返回null</returns>
        public static T Select<T>(string strCacheName) where T: class
        {
            T t = default(T);
            if (HttpContext.Current.Cache[strCacheName] != null)
            {
                t = HttpContext.Current.Cache[strCacheName] as T;
            }
            return t;
        }
        #endregion

        #region 删除cache对象
        /// <summary>
        /// 删除Cache对象
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        public static void Delete(string strCacheName)
        {
            if (HttpContext.Current.Cache[strCacheName] != null)
            {
                HttpContext.Current.Cache.Remove(strCacheName);
            }
        }
        #endregion

    }
}
