using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 小程序应用操作类
/// </summary>
public class APPData
{
    /// <summary>
    /// 根据 APPId 获取应用信息
    /// </summary>
    /// <param name="appId">int 用户编号</param>
    /// <returns>Hash 用户信息</returns>
    public static Hash GetById(int appId)
    {
        string sql = "SELECT * FROM ta_app WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, appId);
        }
    }
    /// <summary>
    /// 根据 APPId 获取 accessToken
    /// </summary>
    /// <param name="appId">int 用户编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int SetAccessToken(int appId, string accessToken, DateTime expire)
    {
        string sql = "UPDATE ta_app SET accessToken=@1,accessTokenExpireIn=@2 WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, appId, accessToken, expire);
        }
    }
}