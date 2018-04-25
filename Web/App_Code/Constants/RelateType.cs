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
    /// 好友分享
    /// </summary>
    FromFriend = 101,
    /// <summary>
    /// 群分享
    /// </summary>
    FromGroup = 201
}