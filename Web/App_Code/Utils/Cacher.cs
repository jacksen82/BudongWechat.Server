using System;
using System.Web;
using System.Web.Caching;
using Budong.Common.Utils;

/// <summary>
/// 缓存访问工具
/// </summary>
public class Cacher
{
    /// <summary>
    /// 获取指定键名获取缓存
    /// </summary>
    /// <param name="key">string 键名</param>
    /// <returns>object 键值</returns>
    public static object Get(string key)
    {
        return HttpContext.Current.Cache[key];
    }
    /// <summary>
    /// 获取指定键名获取缓存 [ 整形 ]
    /// </summary>
    /// <param name="key">string 键名</param>
    /// <param name="def">int 默认值</param>
    /// <returns>int 键值</returns>
    public static int GetInt(string key, int def = 0)
    {
        return Parse.ToInt(HttpContext.Current.Cache[key], def);
    }
    /// <summary>
    /// 设置指定键名键值
    /// </summary>
    /// <param name="key">string 键名</param>
    /// <param name="value">object 键值</param>
    /// <param name="expire">DateTime 过期时间</param>
    public static void Set(string key, object value, DateTime expire)
    {
        if (HttpContext.Current.Cache[key] == null)
        {
            HttpContext.Current.Cache.Add(key, value, null, DateTime.Now.AddSeconds(300),Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
        else
        {
            HttpContext.Current.Cache[key] = value;
        }
    }
}