using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Xml;

namespace Arrow.Framework
{
    /// <summary>
    /// NameValueCollection类型扩展，使之支持XML序列化
    /// </summary>
    public class ExFields : NameValueCollection, IXmlSerializable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExFields()
            : base(StringComparer.CurrentCultureIgnoreCase)
        { }

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// XML反序列化为对象
        /// </summary>
        /// <param name="reader"></param>
        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer oKeySerializer = new XmlSerializer(GetType());
            XmlSerializer oValueSerializer = new XmlSerializer(GetType());
            string sName = "";
            string sValue = "";

            if (reader.IsEmptyElement) return;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.LocalName == "Field")
                    {
                        sName = reader.GetAttribute("Name");
                        sValue = reader.GetAttribute("Value");
                        this.Add(sName, sValue);
                    }
                }
            }

        }

        /// <summary>
        /// 序列化为XML
        /// </summary>
        /// <param name="writer"></param>
        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            for (int i = 0; i < this.Count; i++)
            {
                writer.WriteStartElement("Field");
                writer.WriteAttributeString("Name", this.Keys[i]);
                writer.WriteAttributeString("Value", this[i]);
                writer.WriteEndElement();
            }

        }

    }
}
