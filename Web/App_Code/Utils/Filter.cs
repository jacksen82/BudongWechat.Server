using System;

/// <summary>
/// Format 的摘要说明
/// </summary>
public class Filter
{
    /// <summary>
    /// 转换题目分类编号为分类名称
    /// </summary>
    /// <param name="categoryId">int 分类编号</param>
    /// <returns>string 分类名称</returns>
    public static string categoryIdToName(int categoryId)
    {
        switch (categoryId)
        {
            case 101: return "电视";
            case 102: return "电影";
            case 103: return "动画";
            case 104: return "游戏";
            case 105: return "广告"; 
            case 106: return "音乐"; 
            default: return "其他"; 
        }
    }
}