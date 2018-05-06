using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 关联类型
/// </summary>
public enum RelateType: int
{
    /// <summary>
    /// 来自自己
    /// </summary>
    FromSelf = 101,
    /// <summary>
    /// 好友分享
    /// </summary>
    FromFriend = 201,
    /// <summary>
    /// 群分享
    /// </summary>
    FromGroup = 301,
    /// <summary>
    /// 来自推荐
    /// </summary>
    FromRecommend = 401,
    /// <summary>
    /// 来自热门
    /// </summary>
    FromHot = 501,
    /// <summary>
    /// 陌生人
    /// </summary>
    FromStranger = 901
}