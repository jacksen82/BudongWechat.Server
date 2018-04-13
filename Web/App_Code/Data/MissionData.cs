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
    /// 根据编号获取指定用户在指定关卡的游戏信息
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 关卡信息</returns>
    public static Hash GetByIdAndClientId(int missionId, int clientId)
    {
        string sql = "SELECT *, "+
            "(SELECT COUNT(*) FROM tm_mission_client_subject WHERE clientId=@1 AND missionId=@0) AS subjectPassCount " +
            "FROM tm_mission WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, missionId, clientId);
        }
    }
    /// <summary>
    /// 获取关卡列表
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="pageId">int 页码</param>
    /// <param name="pageSize">int 页尺寸</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash List(int clientId, int pageId, int pageSize)
    {
        string sql = "SELECT missions.*,tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl FROM " +
            "(" +
            "   (SELECT * FROM tm_mission WHERE subjectCount>0 AND clientId in (SELECT friendClientId FROM tc_client_friend WHERE clientId=@0)) " +
            "    UNION " +
            "   (SELECT * FROM tm_mission WHERE subjectCount>0 AND clientId in (SELECT clientId FROM tc_client_group WHERE openGId in (SELECT openGId FROM tc_client_group WHERE clientId=@0))) " +
            "   UNION " +
            "   (SELECT * FROM tm_mission WHERE subjectCount>0 AND clientId=@0) " +
            ") missions LEFT JOIN tc_client ON missions.clientId=tc_client.id " +
            "ORDER BY missions.updateTime DESC,missions.id DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollectionByPageId(sql, pageId, pageSize, clientId);
        }
    }
}