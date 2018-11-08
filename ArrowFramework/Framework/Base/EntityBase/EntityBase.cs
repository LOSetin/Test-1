using System;
using Arrow.Framework.Extensions;

namespace Arrow.Framework
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    public class EntityBase
    {
        public EntityBase() { }
        /// <summary>
        /// 扩展字段容器
        /// </summary>
        public ExFields ExtraFields = new ExFields();
        /// <summary>
        /// 设置扩展字段
        /// </summary>
        /// <param name="name">扩展字段名称</param>
        /// <param name="value">扩展字段值</param>
        public void SetExtraField(string name, string value)
        {
            ExtraFields[name] = value; 
        }
        /// <summary>
        /// 获取扩展属性的值
        /// </summary>
        /// <param name="name">扩展字段名称</param>
        /// <returns></returns>
        public string GetExtraField(string name)
        {
            string val = ExtraFields[name];
            if (string.IsNullOrEmpty(val)) val = "";
            return val;
        }

        /// <summary>
        /// 实体类转为字符串，输出各个属性值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "\r\n" + this.ToDebugString();
        }

    }
}
