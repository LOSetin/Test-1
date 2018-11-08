using Arrow.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Arrow.Framework
{
    /// <summary>
    /// 将对象json序列化DES加密后，存到Cookie，一般大小限制为4k，过期时间为0，表示关闭浏览器则状态过期
    /// </summary>
    public class ArrowWebCookieStatus<T> : IArrowUserStatus<T> where T : class
    {
        private string GetExpireCookieName(string key)
        {
            return key + "_Expire";
        }

        #region 加密解密
        private static readonly string key = "k2hdw342";

        private string encrypt(string source)
        {
           return  EncryptHelper.DESEncrypt(source, key);
        }

        private string decrypt(string decryptString)
        {
            return EncryptHelper.DESDecrypt(decryptString, key);
        }
        #endregion

        #region 实现接口
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expire">过期时间（以分钟为单位）</param>
        public void SetValue(string key, T t , int expire=0)
        {
            string jsonString = JsonHelper.JsonSerializer<T>(t);
            string val = encrypt(jsonString);
            //过期时间转换为秒
            int expireTime = expire * 60;
            //增加一个Cookie，保存expireTme，保存时间为永久
            CookieHelper.SetValue(GetExpireCookieName(key), expireTime.ToString(), 1); 
            //保存状态
            CookieHelper.SetValue(key, val, expireTime);
        }

        /// <summary>
        /// 获取值，如果值不存在，则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue(string key) 
        {
            string val = CookieHelper.GetValue(key);
            if (val != null)
            {
                string jsonString = decrypt(val);
                T t = JsonHelper.JsonDeserialize<T>(jsonString);
                //获取过期时间
                int expireTime = CookieHelper.GetValue(GetExpireCookieName(key)).ToArrowInt();
                //更新Cookie
                CookieHelper.SetValue(key, val, expireTime); 
                return t;
            }
            else
            {
                return null;
            }
        }

        public void Remove(string key)
        {
            CookieHelper.Delete(key);
        }
        #endregion

    }
}
