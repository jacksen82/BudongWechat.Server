using System;
using System.Text;

namespace Budong.Common.Secret
{
    /// <summary>
    /// MD5 加密类
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="text">string 要加密的字符串</param>
        /// <returns>string 加密结果</returns>
        public static string Encode(string text)
        {
            byte[] input = Encoding.Default.GetBytes(text);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(input);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
