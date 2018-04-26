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
    /// 根据关卡标题获取关卡信息
    /// </summary>
    /// <param name="clientId">int 用户编号</param>
    /// <param name="title">string 关卡标题</param>
    /// <returns>Hash 关卡信息</returns>
    public static Hash GetByTitle(int clientId, string title)
    {
        string sql = "SELECT * FROM tm_mission WHERE clientId=@0 AND title=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId, title);
        }
    }
    /// <summary>
    /// 根据关卡标题获取关卡信息
    /// </summary>
    /// <param name="title">string 关卡标题</param>
    /// <param name="exceptMissionId">int 忽略关卡</param>
    /// <returns>Hash 用户信息</returns>
    public static bool ExistsByTitle(string title, int exceptMissionId)
    {
        string sql = "SELECT COUNT(*) FROM tm_mission WHERE title=@0 AND id<>@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, title, exceptMissionId) > 0;
        }
    }
    /// <summary>
    /// 获取用户的所有关卡
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 关卡集合</returns>
    public static Hash All(int clientId)
    {
        string sql = "SELECT * FROM tm_mission WHERE clientId=@0 ORDER BY createTime DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
        }
    }
    /// <summary>
    /// 获取推荐关卡列表
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="pageId">int 页码</param>
    /// <param name="pageSize">int 页尺寸</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash List(int clientId, int pageId, int pageSize)
    {
        string sql = "SELECT tm_mission.*,tc_client.id AS authorId,tc_client.nick,tc_client.gender,tc_client.birthyear,tc_client.avatarUrl,tm_mission_client.clientId AS missionClientId,tm_mission_client.score,tm_mission_client.subjectIndex,tm_mission_client.secondCount " +
            "FROM (" +
            "   SELECT missionId, MIN(type) AS type FROM( "+
            "      (SELECT id AS missionId, 301 AS type FROM tm_mission WHERE clientId=@0 OR grade>0) "+
            "      UNION "+
            "      (SELECT missionId, type FROM tc_client_mission where clientId=@0) "+
            "   ) mids GROUP BY missionId "+
            ") missions LEFT JOIN tm_mission ON missions.missionId=tm_mission.id LEFT JOIN tc_client ON tm_mission.clientId=tc_client.id LEFT JOIN tm_mission_client ON tm_mission.id=tm_mission_client.missionId AND tm_mission_client.clientId=@0 "+
            "WHERE tm_mission.subjectCount>@1 " +
            "ORDER BY date(tm_mission.updateTime) DESC,type ASC, tm_mission.id DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollectionByPageId(sql, pageId, pageSize, clientId, Settings.MIN_SUBJECT_COUNT);
        }
    }
    /// <summary>
    /// 创建新关卡
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="title">string 关卡标题</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, string title)
    {
        string sql = "INSERT INTO tm_mission (clientId,title) VALUES(@0,@1) ON DUPLICATE KEY UPDATE title=VALUES(title)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, title);
        }
    }
    /// <summary>
    /// 修改关卡标题
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="title">string 关卡标题</param>
    /// <returns>int 受影响的行数</returns>
    public static int Edit(int missionId, string title)
    {
        string sql = "UPDATE tm_mission SET title=@1 WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, missionId, title);
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
            "   tags=(SELECT GROUP_CONCAT(categoryName separator ',') FROM (SELECT categoryName FROM tm_mission_subject WHERE missionId =@0 GROUP BY categoryId ORDER BY COUNT(*) DESC) TAGS), " +
            "   playerCount=IFNULL((SELECT COUNT(*) FROM tm_mission_client WHERE missionId=@0 AND clientId!=tm_mission.clientId),0), " +
            "   subjectCount=IFNULL((SELECT COUNT(*) FROM tm_mission_subject WHERE missionId=@0),0) " +
            "WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, missionId);
        }
    }
    /// <summary>
    /// 删除关卡
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Delete(int missionId)
    {
        string sql = "DELETE FROM tm_mission WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, missionId);
        }
    }
    /// <summary>
    /// 清理关卡数据
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    public static void Clear(int missionId)
    {
        string sql = "DELETE FROM tc_client_mission WHERE missionId=@0;" +
            "DELETE FROM tm_mission_client WHERE missionId=@0;" +
            "DELETE FROM tm_mission_subject_client WHERE missionId=@0;" +
            "DELETE FROM tm_mission_subject WHERE missionId=@0;";
        using (MySqlADO ado = new MySqlADO())
        {
            ado.NonQuery(sql, missionId);
        }
    }
}