using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// MissionClientData 的摘要说明
/// </summary>
public class MissionClientData
{
    /// <summary>
    /// 根据编号获取指定用户在指定关卡的游戏信息
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 关卡信息</returns>
    public static Hash GetByIdAndClientId(int missionId, int clientId)
    {
        string sql = "SELECT tm_mission.*, " +
            "   tm_mission_client.clientId AS missionClientId, " +
            "   tm_mission_client.score, " +
            "   tm_mission_client.subjectIndex, " +
            "   tm_mission_client.secondCount " +
            "FROM tm_mission LEFT JOIN tm_mission_client ON tm_mission.id=tm_mission_client.missionId AND tm_mission_client.clientId=@1 " +
            "WHERE tm_mission.id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, missionId, clientId);
        }
    }
    /// <summary>
    /// 获取关卡排行榜
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash Rank(int missionId)
    {
        string sql = "SELECT tm_mission_client.*, " +
            "   tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl " +
            "FROM tm_mission_client LEFT JOIN tc_client ON tm_mission_client.clientId=tc_client.id " +
            "WHERE tm_mission_client.missionId=@0 AND " +
            "   tm_mission_client.clientId NOT IN (SELECT clientId FROM tm_mission WHERE id=@0) " +
            "ORDER BY tm_mission_client.subjectIndex DESC, tm_mission_client.secondCount ASC ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, missionId);
        }
    }
    /// <summary>
    /// 开始关卡
    /// </summary>
    /// <param name="clientId">int 客户端编号 </param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 返回受影响的行数</returns>
    public static int Create(int clientId, int missionId)
    {
        string sql = "INSERT INTO tm_mission_client (clientId,missionId,score,subjectIndex,secondCount) VALUES(@0,@1,0,0,0) ON DUPLICATE KEY UPDATE clientId=VALUES(clientId), missionId=VALUES(missionId)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId);
        }
    }
    /// <summary>
    /// 更新用户关卡游戏信息
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Update(int clientId, int missionId)
    {
        string sql = "UPDATE tm_mission_client SET " +
            "   score=(SELECT SUM(if(resultType=100,1,0)) FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1)," +
            "   subjectIndex=(SELECT COUNT(*) FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1)," +
            "   secondCount=(SELECT SUM(secondCount) FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1) " +
            "WHERE clientId=@0 AND missionId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId);
        }
    }
    /// <summary>
    /// 清除游戏记录
    /// </summary>
    /// <param name="clientId">int 客户端编号 </param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 返回受影响的行数</returns>
    public static void Clear(int clientId, int missionId)
    {
        string sql = "DELETE FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            ado.NonQuery(sql, clientId, missionId);
        }
    }
}