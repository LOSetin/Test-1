using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Arrow.Framework.Extensions;

namespace Arrow.Framework.TxtDB
{
    /// <summary>
    /// 文本数据库操作类，文本使用UTF-8编码，适合不超过10M的文本，不支持并发，适合后台单管理员操作
    /// </summary>
    public abstract class TxtDBHelper
    {
        #region 私有方法
        private static string GetDbPath<T>()
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/"+typeof(T).Name+".txt");
        }

        private static void Save<T>(List<T> t)
        {
            try
            {
                string dbPath = GetDbPath<T>();
                if (!File.Exists(dbPath))
                {
                    File.Create(dbPath).Close();
                }
                string contents = JsonHelper.JsonSerializer<List<T>>(t);
                File.WriteAllText(dbPath, contents, Encoding.UTF8);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Add<T>(T t)
        {
            List<T> datas = SelectAll<T>();
            datas.Add(t);
            Save<T>(datas);
        }

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="match"></param>
        public static void Update<T>(T t, Predicate<T> match)
        {
            List<T> datas = SelectAll<T>();
            int idx = datas.FindIndex(match);
            if (idx >= 0)
            {
                datas.RemoveAt(idx);
                datas.Add(t);
                Save<T>(datas);
            }
        }

        /// <summary>
        /// 更新一个对象，如果该对象不存在，则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="match"></param>
        public static void UpdateAndAdd<T>(T t, Predicate<T> match)
        {
            List<T> datas = SelectAll<T>();
            int idx = datas.FindIndex(match);
            if (idx >= 0)
            {
                datas.RemoveAt(idx);
                datas.Add(t);
                Save<T>(datas);
            }
            else
            {
                Add<T>(t);
            }
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        public static void Delete<T>(Predicate<T> match)
        {
            List<T> datas = SelectAll<T>();
            datas.RemoveAll(match);
            Save<T>(datas);
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> SelectAll<T>()
        {
            List<T> datas = new List<T>();
            string dbPath = GetDbPath<T>();
            if (File.Exists(dbPath))
            {
                try
                {
                    string content = File.ReadAllText(GetDbPath<T>(), Encoding.UTF8);
                    if (!content.IsNullOrEmpty())
                        datas = JsonHelper.JsonDeserialize<List<T>>(content);
                }
                catch
                {
                    throw;
                }
            }
            return datas;
        }

        /// <summary>
        /// 获取指定数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> SelectList<T>(Predicate<T> match)
        {
            List<T> datas = SelectAll<T>();
            return datas.FindAll(match);
        }

        /// <summary>
        /// 返回第一个符合条件的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T SelectFirst<T>(Predicate<T> match)
        {
            List<T> datas = SelectAll<T>();
            return datas.Find(match);
        }

       
    }
}
