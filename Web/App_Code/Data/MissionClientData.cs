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
            "   tm_mission_client.first, " +
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
    /// 获取关卡全国排行榜
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash RankTop10(int clientId, int missionId)
    {
        string sql = "SELECT tm_mission_client.*, " +
            "   tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl " +
            "FROM tm_mission_client LEFT JOIN tc_client ON tm_mission_client.clientId=tc_client.id " +
            "WHERE tm_mission_client.missionId=@0 AND tc_client.actived>0 AND " +
            "   tm_mission_client.clientId!=(SELECT clientId FROM tm_mission WHERE id=@0) " +
            "ORDER BY tm_mission_client.score DESC, tm_mission_client.secondCount ASC, tm_mission_client.subjectIndex DESC LIMIT 10";
        using (MySqlADO ado = new MySqlADO())
        {
            Hash result = new Hash();
            HashCollection data = new HashCollection();
            HashCollection clients = ado.GetHashCollection(sql, missionId).ToHashCollection("data");
            for (int i = 0; i < clients.Count; i++)
            {
                if (i < 10 || clients[i].ToInt("clientId") == clientId)
                {
                    clients[i]["index"] = i;
                    data.Add(clients[i]);
                }
            }
            result["data"] = data;
            return result;
        }
    }
    /// <summary>
    /// 获取关卡好友排行榜
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash RankInFriend(int clientId, int missionId)
    {
        string sql = "SELECT tm_mission_client.*,clients.openGId, " +
            "   tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl " +
            "FROM ( " +
            "   SELECT clientid, openGId FROM ( " +
            "       (SELECT friendClientId as clientId, '' as openGId FROM tc_client_friend WHERE clientId=@0) " +
            "       UNION " +
            "       (SELECT clientId, openGId FROM tc_client_group WHERE openGId IN (SELECT openGId FROM tc_client_group WHERE clientid=@0)) " +
            "   ) clients GROUP BY clientId " +
            ") clients LEFT JOIN tm_mission_client ON clients.clientid=tm_mission_client.clientId LEFT JOIN tc_client ON clients.clientId=tc_client.id " +
            "WHERE tc_client.id>0 AND tc_client.actived>0 AND tm_mission_client.missionId=@1 AND clients.clientId!=(SELECT clientId FROM tm_mission WHERE id=@1) " +
            "ORDER BY tm_mission_client.score DESC, tm_mission_client.secondCount ASC, tm_mission_client.subjectIndex DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId, missionId);
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
            "   score=IFNULL((SELECT SUM(if(resultType=100,1,0)) FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1),0)," +
            "   subjectIndex=IFNULL((SELECT COUNT(*) FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1),0)," +
            "   secondCount=IFNULL((SELECT SUM(secondCount) FROM tm_mission_subject_client WHERE clientId=@0 AND missionId=@1),0) " +
            "WHERE clientId=@0 AND missionId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId);
        }
    }
    /// <summary>
    /// 设置用户关卡已不是第一次
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int First(int clientId, int missionId)
    {
        string sql = "UPDATE tm_mission_client SET `first`=1 WHERE clientId=@0 AND missionId=@1";
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