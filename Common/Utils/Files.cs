using System;
using System.IO;
using System.Web;

namespace Budong.Common.Utils
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class Files
    {
        /// <summary>
        /// 以时间戳生成文件名
        /// </summary>
        /// <param name="extension">string 扩展名(带.)</param>
        /// <returns>string 文件名</returns>
        public static string NewFileName(string extension)
        {
            return DateTime.Now.ToString("yyMMddHHmmssms") + extension;
        }
        /// <summary>
        /// 根据虚拟地址获得绝对路径
        /// </summary>
        /// <param name="fileUrl">string 虚拟地址</param>
        /// <returns>string 绝对路径</returns>
        public static string MapPath(string fileUrl)
        {
            return HttpContext.Current.Server.MapPath(fileUrl);
        }
        /// <summary>
        /// 保存二级制数据至文件
        /// </summary>
        /// <param name="filePath">string 文件路径(绝对路径)</param>
        /// <param name="bytes">byte[] 数据流</param>
        public static void SaveAllBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
