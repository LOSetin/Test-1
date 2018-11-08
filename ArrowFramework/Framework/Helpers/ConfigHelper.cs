using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Arrow.Framework
{
    /// <summary>
    /// 配置文件处理
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// 使用xml方式，获取Config指定key的值，同时适用与WinForm的config文件
        /// </summary>
        /// <param name="strExecutablePath">config文件完整路径，如Server.MapPath("~/Config/Sys.config")</param>
        /// <param name="appKey">key的名称</param>
        /// <returns></returns>
        public static string GetConfigValue(string strExecutablePath, string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(strExecutablePath);
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 使用xml方式，设置Config指定key的值，同时适用与WinForm的config文件
        /// </summary>
        /// <param name="strExecutablePath">config文件完整路径，如Server.MapPath("~/Config/Sys.config")</param>
        /// <param name="AppKey">key的名称</param>
        /// <param name="AppValue">key对应的值</param>
        public static void SetConfigValue(string strExecutablePath, string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称
            xDoc.Load(strExecutablePath);

            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");
            // xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2); 
            }
            xDoc.Save(strExecutablePath);
        }
    }
}
