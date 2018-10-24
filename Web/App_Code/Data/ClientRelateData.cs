﻿using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端关系操作类
/// </summary>
public class ClientRelateData
{
    /// <summary>
    /// 判断两个客户端的关系类型
    /// </summary>
    /// <param name="fromClientId">int 邀请客户端编号</param>
    /// <param name="toClientId">int 被邀请客户端编号</param>
    /// <returns>int 关系类型</returns>
    public static RelateType GetRelateType(int fromClientId, int toClientId)
    {
        string sql = "SELECT relateType FROM tc_client_relate WHERE fromClientId=@0 AND toClientId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return (RelateType)ado.GetInt(sql, fromClientId, toClientId);
        }
    }
    /// <summary>
    /// 创建关系条目
    /// </summary>
    /// <param name="fromClientId">int 邀请客户端编号</param>
    /// <param name="toClientId">int 被邀请客户端编号</param>
    /// <param name="relateType">RelateType 关系类型</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int fromClientId, int toClientId, RelateType relateType)
    {
        string sql = "INSERT INTO tc_client_relate (fromClientId, toClientId, relateType, openGId) VALUES(@0, @1, @2, @3) ON DUPLICATE KEY UPDATE fromClientId=VALUES(fromClientId), toClientId=VALUES(toClientId), relateType=VALUES(relateType), openGId=VALUES(openGId), updateTime=Now()";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, fromClientId, toClientId, (int)relateType, String.Empty);
        }
    }
    /// <summary>
    /// 更新关系条目
    /// </summary>
    /// <param name="fromClientId">int 邀请客户端编号</param>
    /// <param name="toClientId">int 被邀请客户端编号</param>
    /// <param name="relateType">RelateType 关系类型</param>
    /// <returns>int 受影响的行数</returns>
    public static int Update(int fromClientId, int toClientId, RelateType relateType)
    {
        string sql = "UPDATE tc_client_relate SET relateType=IF(relateType>@2,@2,relateType) WHERE fromClientId=@0 AND toClientId=@1 ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, fromClientId, toClientId, (int)relateType);
        }
    }
}