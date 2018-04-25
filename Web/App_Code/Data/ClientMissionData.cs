using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// MissionData 的摘要说明
/// </summary>
public class ClientMissionData
{
    /// <summary>
    /// 添加客户端关卡关系
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="fromClientId">int 来源客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, int missionId, int fromClientId, string openGId)
    {
        string sql = "INSERT INTO tc_client_mission (clientId,missionId,fromClientId,fromOpenGId,type) VALUES(@0,@1,@2,@3,@4) ON DUPLICATE KEY UPDATE clientId=VALUES(clientId), missionId=VALUES(missionId), fromClientId=VALUES(fromClientId), fromOpenGId=VALUES(fromOpenGId)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId, fromClientId, openGId, (int)(Genre.IsNull(openGId) ? RelateType.FromFriend : RelateType.FromGroup));
        }
    }
}