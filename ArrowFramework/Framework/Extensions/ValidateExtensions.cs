using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.Extensions
{
    public static class ValidateExtensions
    {
        private static void showMsg(string msg)
        {
            MessageBox.Show(msg);
        }

        /// <summary>
        /// 判断字符串是否为空，如果为空，则弹出信息，并且返回true，否则返回false
        /// </summary>
        /// <param name="str"></param>
        /// <param name="emptyMsg">字符串为空时，弹出的信息</param>
        /// <returns></returns>
        public static bool ValidateIsNullOrEmpty(this string str,string emptyMsg)
        {
            if(str.IsNullOrEmpty())
            {
                showMsg(emptyMsg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证字符串数组是否含有空字符串，如果是，则弹出对应的信息，返回true，否则返回false
        /// </summary>
        /// <param name="arrStr"></param>
        /// <param name="arrFieldName">对应的字段名数组，例如其中之一为用户名，则弹出信息：请输入用户名！</param>
        /// <returns></returns>
        public static bool ValidateHasNullOrEmptyString(this string[] arrStr,string[] arrFieldName)
        {
            if (arrStr.Length != arrFieldName.Length)
                throw new Exception("验证数组长度和字段名数组长度必须相同！");
            bool result = false;
            for(int i=0;i<arrStr.Length;i++)
            {
                if(arrStr[i].IsNullOrEmpty())
                {
                    showMsg("请输入" + arrFieldName[i] + "！");
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 判断字符串是否等于另一字符串（不区分大小写），如果相等，则弹出信息，并且返回true，否则返回false。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strCompare"></param>
        /// <param name="equalMsg"></param>
        /// <returns></returns>
        public static bool ValidateIsEqualTo(this string str,string strCompare,string notEqualMsg)
        {
            if(str.IsEqualTo(strCompare))
            {
                showMsg(notEqualMsg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断字符串是否等于另一字符串（不区分大小写），如果不等，则弹出信息，并且返回true，否则返回false。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strCompare"></param>
        /// <param name="equalMsg"></param>
        /// <returns></returns>
        public static bool ValidateIsNotEqualTo(this string str, string strCompare, string notEqualMsg)
        {
            if (!str.IsEqualTo(strCompare))
            {
                showMsg(notEqualMsg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，如果为空，则弹出信息，并且返回true，否则返回false
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nullMsg">对象为空时，弹出的信息</param>
        /// <returns></returns>
        public static bool ValidateIsNull(this object obj, string nullMsg)
        {
            if (obj == null)
            {
                showMsg(nullMsg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断对象是否为不为空，如果不为空，则弹出信息，并且返回true，否则返回false
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="notNullMsg"></param>
        /// <returns></returns>
        public static bool ValidateIsNotNull(this object obj,string notNullMsg)
        {
            if(obj!=null)
            {
                showMsg(notNullMsg);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 如果布尔值为true，则弹出信息，并且返回true，否则返回false
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg">布尔值为true时，弹出的信息</param>
        /// <returns></returns>
        public static bool ValidateSuccess(this bool success,string msg)
        {
            if(success)
            {
                showMsg(msg);
                return true;
            }
            return false;
        }


    }
}
