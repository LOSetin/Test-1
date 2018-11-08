using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework
{
    /// <summary>
    /// 用户状态维持的泛型接口
    /// </summary>
    public interface IArrowUserStatus<T> where T: class
    {
        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expire">过期时间（以分钟为单位），0表示默认过期时间</param>
        void SetValue(string key, T t, int expire = 0);

        /// <summary>
        /// 获取值，如果值不存在，或过期，则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetValue(string key);

        /// <summary>
        /// 清除指定值
        /// </summary>
        void Remove(string key);
    }
}
