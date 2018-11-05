using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// ClientLiveData 的摘要说明
/// </summary>
public class ClientLiveData
{
    /// <summary>
    /// 今天为好友激活的次数
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="toClientId">int 激活客户端编号</param>
    /// <returns>int 激活次数</returns>
    public static int GetCountAtToday(int clientId, int toClientId)
    {
        string sql = "SELECT COUNT(*) FROM tc_client_live WHERE clientId=@0 AND toClientId=@1 AND createTime>@2";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, clientId, toClientId, DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
    /// <summary>
    /// 根据复活卡激活列表
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 激活列表</returns>
    public static Hash List(int clientId)
    {
        string sql = "SELECT tcs.*,tc.avatarUrl,tc.nick,tc.gender FROM tc_client_live tcs LEFT JOIN tc_client tc ON tcs.clientId=tc.clientId WHERE tcs.toClientId=@0 ORDER BY tcs.createTime DESC ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
        }
    }
    /// <summary>
    /// 激活复活卡
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="toClientId">int 激活客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Activate(int clientId, int toClientId, string openGId)
    {
        string sql = "INSERT INTO tc_client_live (clientId,toClientId,openGId) VALUES(@0,@1,@2)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, toClientId, openGId);
        }
    }
}