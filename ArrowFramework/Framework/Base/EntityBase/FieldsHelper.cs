using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Arrow.Framework
{
    /// <summary>
    /// 字段序列化
    /// </summary>
    public class FieldsHelper
    {
        #region 二进制序列化扩展字段集合
        /// <summary>
        /// 二进制序列化对象
        /// </summary>
        /// <param name="coll">需要序列化的对象</param>
        /// <returns></returns>
        public static byte[] SerializeExtraFields(NameValueCollection coll)
        {
            byte[] b = { };
            // 将extendedAttributes对象（里面保存了所有的用户扩展信息）序列化为内存流 
            if (coll != null && coll.Count > 0)
            {
                // 序列化对象 
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                // 创建一个内存流，序列化后保存在其中 
                MemoryStream ms = new MemoryStream();
                binaryFormatter.Serialize(ms, coll);
                // 设置内存流的起始位置 
                ms.Position = 0;
                // 读入到 byte 数组 
                b = new Byte[ms.Length];
                ms.Read(b, 0, b.Length);
                ms.Close();
            }

            return b;

        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="serializedExtendedAttributes">需要反序列化的对象</param>
        /// <returns></returns>
        public static NameValueCollection DeserializeExtraFields(byte[] serializedExtendedAttributes)
        {
            NameValueCollection coll = new NameValueCollection();
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                // 将 byte 数组到内存流 
                ms.Write(serializedExtendedAttributes, 0, serializedExtendedAttributes.Length);
                // 将内存流的位置到最开始位置 
                ms.Position = 0;
                // 反序列化成NameValueCollection对象，创建出与原对象完全相同的副本 
                coll = (NameValueCollection)binaryFormatter.Deserialize(ms);
                ms.Close();
            }
            catch
            {
            }
            return coll;
        }

        #endregion

        #region xml序列化扩展字段集合
        /// <summary>
        /// 对象序列化成 XML String
        /// </summary>
        public static string XmlSerialize(ExFields exFields)
        {
            if (exFields == null) return " ";
            if (exFields.Count <= 0) return " ";

            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExFields));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, exFields);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }

        /// <summary>
        /// XML String 反序列化成对象
        /// </summary>
        public static ExFields XmlDeserialize(string xmlString)
        {
            ExFields fields = new ExFields();
            xmlString = xmlString.Trim();
            if (string.IsNullOrEmpty(xmlString)) return fields;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExFields));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    fields = (ExFields)obj;
                }
            }
            return fields;
        }
        #endregion
    }
}
