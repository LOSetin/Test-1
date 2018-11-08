using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.Generic
{
    /// <summary>
    /// 初始化交由静态构造函数实现，并可以在运行时编译。在这种模式下，无需自己解决线程安全性问题，CLR会给我们解决
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ArrowSingleton<T> where T : new()
    {
        private static readonly T instance = new T();

        private ArrowSingleton()
        {
           
        }

        public static T CreateInstance()
        {
            return instance;
        }
    }
    
}
