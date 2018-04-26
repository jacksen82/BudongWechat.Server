using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端分享操作类
/// </summary>
public class ClientShareData
{
    /// <summary>
    /// 插入分享记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, int missionId, string openGId)
    {
        string sql = "INSERT INTO tc_client_share (clientId,missionId,openGId) VALUES(@0,@1,@2)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId, openGId);
        }
    }
}