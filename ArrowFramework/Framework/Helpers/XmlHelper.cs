using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace Arrow.Framework
{
    /// <summary>
    /// xml操作类
    /// </summary>
    public static  class XmlHelper
    {
        #region DataTable转Xml
        /// <summary>
        /// DataTable转Xml
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static string DataTableToXml(DataTable dt)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);
                //用WriteXml方法写入文件.
                dt.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        #endregion

        #region DataSet转Xml
        /// <summary>
        /// DataSet转Xml
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public static string DataSetToXml(DataSet ds)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);
                //用WriteXml方法写入文件.
                ds.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        #endregion

        #region Xml转DataSet
        /// <summary>
        /// Xml转DataSet
        /// </summary>
        /// <param name="xml">Xml</param>
        /// <returns></returns>
        public static DataSet XmlToDataSet(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                stream = new StringReader(xml);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                return ds;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        #endregion

        #region Xml转DataTable
        /// <summary>
        /// Xml转DataTable
        /// </summary>
        /// <param name="xml">Xml</param>
        /// <returns></returns>
        public static DataTable XmlToDataTable(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                stream = new StringReader(xml);
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                dt = ds.Tables[0];
                return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        #endregion
    }
}
