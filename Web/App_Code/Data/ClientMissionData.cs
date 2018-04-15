using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// MissionData 的摘要说明
/// </summary>
public class ClientMissionData
{
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
    /// 获取关卡题目数量
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 题目数量</returns>
    public static int GetSubjectCount(int missionId)
    {
        string sql = "SELECT COUNT(*) FROM tm_mission_subject WHERE missionId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, missionId);
        }
    }
    /// <summary>
    /// 获取关卡分类集合
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>object[] 分类集合</returns>
    public static object[] GetCategoryIds(int missionId)
    {
        string sql = "SELECT categoryId FROM tm_mission_subject WHERE missionId=@0 GROUP BY categoryId ORDER BY COUNT(*) DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetValues(sql, missionId);
        }
    }
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
    /// 获取用户的所有关卡
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 关卡集合</returns>
    public static Hash List(int clientId)
    {
        string sql = "SELECT * FROM tm_mission WHERE clientId=@0 ORDER BY updateTime DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
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
    /// 更新关卡标签及题目数量
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="tags">string 标签</param>
    /// <param name="subjectCount">int 题目数量</param>
    /// <returns>int 受影响的行数</returns>
    public static int Update(int missionId, string tags, int subjectCount)
    {
        string sql = "UPDATE tm_mission SET tags=@1,subjectCount=@2 WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, missionId, tags, subjectCount);
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
        string sql = "DELETE FROM tm_mission_client WHERE missionId=@0;" +
            "DELETE FROM tm_mission_subject_client WHERE missionId=@0;" +
            "DELETE FROM tm_mission_subject WHERE missionId=@0;";
        using (MySqlADO ado = new MySqlADO())
        {
            ado.NonQuery(sql, missionId);
        }
    }
}