using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Budong.Common.Utils
{
    /// <summary>
    /// 基础类型判断
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// 判断指定对象是否为空
        /// </summary>
        /// <param name="obj">object 对象</param>
        /// <param name="type">Type 类型</param>
        /// <returns>bool 是否为空，true 为空</returns>
        public static bool IsNull(object obj, Type type = null)
        {
            if (obj == null) return true;
            else
            {
                switch (obj.GetType().Name)
                {
                    case "String": return obj.ToString().Trim() == "";
                    case "DBNull": return true;
                    default: return false;
                }
            }
        }
        /// <summary>
        /// 判断指定对象是否为指定类型
        /// </summary>
        /// <param name="obj">object 对象</param>
        /// <param name="type">string 类型</param>
        /// <returns>bool 是否指定类型，true 是</returns>
        public static bool IsType(object obj, string type)
        {
            if (obj == null) return false;
            else
            {
                if (obj.GetType().Name.Equals(type, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 判断两个对象是否一致
        /// </summary>
        /// <param name="obj">object 目标对象</param>
        /// <param name="value">object 比较对象</param>
        /// <param name="rigorous">bool 是否严谨模式</param>
        /// <returns>bool 是否一致</returns>
        public static bool IsEquals(object obj, object value, bool rigorous = false)
        {
            if (rigorous == true)
            {
                return obj == value;
            }
            else
            {
                return Convert.ToString(obj).ToLower() == Convert.ToString(value).ToLower();
            }
        }
        /// <summary>
        /// 判断指定数字是否为手机号码
        /// </summary>
        /// <param name="obj">long 数字</param>
        /// <returns>bool 是否合法</returns>
        public static bool IsMobile(long obj)
        {
            if (obj > 10000000000 && obj < 19900000000)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取指定字符串的长度 （汉字占两个字节）
        /// </summary>
        /// <param name="str">string 字符串</param>
        /// <returns>int 返回字符串的长度</returns>
        public static int Length(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
    }
}
