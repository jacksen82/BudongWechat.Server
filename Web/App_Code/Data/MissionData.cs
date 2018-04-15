using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// MissionData 的摘要说明
/// </summary>
public class MissionData
{
    /// <summary>
    /// 根据编号获取关卡信息
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 关卡信息</returns>
    public static Hash GetById(int missionId)
    {
        string sql = "SELECT * FROM tm_mission WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, missionId);
        }
    }
    /// <summary>
    /// 获取关卡列表
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash List(int clientId)
    {
        string sql = "SELECT missions.*,tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl,tm_mission_client.clientId AS missionClientId,tm_mission_client.score,tm_mission_client.subjectIndex,tm_mission_client.secondCount FROM " +
            "(" +
            "   (SELECT * FROM tm_mission WHERE subjectCount>0 AND clientId in (SELECT friendClientId FROM tc_client_friend WHERE clientId=@0)) " +
            "    UNION " +
            "   (SELECT * FROM tm_mission WHERE subjectCount>0 AND clientId in (SELECT clientId FROM tc_client_group WHERE openGId in (SELECT openGId FROM tc_client_group WHERE clientId=@0))) " +
            "   UNION " +
            "   (SELECT * FROM tm_mission WHERE subjectCount>0 AND clientId=@0) " +
            ") missions LEFT JOIN tc_client ON missions.clientId=tc_client.id LEFT JOIN tm_mission_client ON missions.id=tm_mission_client.missionId AND tm_mission_client.clientId=@0 " +
            "ORDER BY missions.updateTime DESC,missions.id DESC limit 100";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
        }
    }
    /// <summary>
    /// 获取更多关卡列表
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash Recommend(int clientId)
    {
        string sql = "SELECT missions.*,tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl,tm_mission_client.clientId AS missionClientId,tm_mission_client.score,tm_mission_client.subjectIndex,tm_mission_client.secondCount FROM " +
            "("+
            "   SELECT * FROM tm_mission WHERE grade>0 AND subjectCount>0 AND clientId!=@0 AND "+
            "       clientId NOT IN (SELECT friendClientId FROM tc_client_friend WHERE clientId=@0) AND "+
            "       clientId NOT IN (SELECT clientId FROM tc_client_group WHERE openGId in (SELECT openGId FROM tc_client_group WHERE clientId=@0))" +
            ") missions LEFT JOIN tc_client ON missions.clientId=tc_client.id LEFT JOIN tm_mission_client ON missions.id=tm_mission_client.missionId AND tm_mission_client.clientId=@0 " +
            "ORDER BY missions.updateTime DESC,missions.id DESC limit 100";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
        }
    }
    /// <summary>
    /// 更新关卡信息
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Update(int missionId)
    {
        string sql = "UPDATE tm_mission SET " +
            "   playerCount=(SELECT COUNT(*) FROM tm_mission_client WHERE missionId=@0 AND clientId!=tm_mission.clientId) " +
            "WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, missionId);
        }
    }
}