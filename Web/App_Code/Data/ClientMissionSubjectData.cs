using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// ClientMissionSubjectData 的摘要说明
/// </summary>
public class ClientMissionSubjectData
{
    /// <summary>
    /// 获取新题目索引
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>int 索引</returns>
    public static int NewIndex(int missionId)
    {
        string sql = "SELECT MAX(`index`) FROM tm_mission_subject WHERE missionId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, missionId) + 1;
        }
    }
    /// <summary>
    /// 根据关卡标题获取关卡信息
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="title">string 关卡标题</param>
    /// <param name="exceptSubjectId">int 忽略题目</param>
    /// <returns>Hash 用户信息</returns>
    public static bool ExistsByTitle(int missionId, string title, int exceptSubjectId)
    {
        string sql = "SELECT COUNT(*) FROM tm_mission_subject WHERE missionId=@0 AND title=@1 AND id<>@2";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, missionId, title, exceptSubjectId) > 0;
        }
    }
    /// <summary>
    /// 根据编号获取题目信息
    /// </summary>
    /// <param name="subjectId">int 题目编号</param>
    /// <returns>Hash 关卡信息</returns>
    public static Hash GetById(int subjectId)
    {
        string sql = "SELECT * FROM tm_mission_subject WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, subjectId);
        }
    }
    /// <summary>
    /// 根据题目标题获取题目信息
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="title">string 关卡标题</param>
    /// <returns>Hash 关卡信息</returns>
    public static Hash GetByTitle(int missionId, string title)
    {
        string sql = "SELECT * FROM tm_mission_subject WHERE missionId=@0 AND title=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, missionId, title);
        }
    }
    /// <summary>
    /// 获取关卡所有题目
    /// </summary>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 关卡集合</returns>
    public static Hash List(int missionId)
    {
        string sql = "SELECT * FROM tm_mission_subject WHERE missionId=@0 ORDER BY `index` ASC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, missionId);
        }
    }
    /// <summary>
    /// 创建新题目
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="title">string 题目答案</param>
    /// <param name="tip">string 答案提示</param>
    /// <param name="categoryId">int 题目分类</param>
    /// <param name="index">int 题目索引</param>
    /// <param name="mp3Url">string 音频文件路径</param>
    /// <returns>int 受影响的行数</returns>
    public static int Add(int clientId, int missionId, string title, string tip, int categoryId, int index, string mp3Url)
    {
        string sql = "INSERT INTO tm_mission_subject (clientId,missionId,title,tip,categoryId,`index`,mp3Url) VALUES(@0,@1,@2,@3,@4,@5,@6) ON DUPLICATE KEY UPDATE missionId=VALUES(missionId), title=VALUES(title)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId, title, tip, categoryId, index, mp3Url);
        }
    }
    /// <summary>
    /// 修改题目信息
    /// </summary>
    /// <param name="subjectId">int 题目编号</param>
    /// <param name="title">string 题目答案</param>
    /// <param name="tip">string 答案提示</param>
    /// <param name="categoryId">int 题目分类</param>
    /// <returns>int 受影响的行数</returns>
    public static int Edit(int subjectId, string title, string tip, int categoryId)
    {
        string sql = "UPDATE tm_mission_subject SET title=@1,tip=@2,categoryId=@3 WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, subjectId, title, tip, categoryId);
        }
    }
    /// <summary>
    /// 删除题目
    /// </summary>
    /// <param name="subjectId">int 题目编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Delete(int subjectId)
    {
        string sql = "DELETE FROM tm_mission_subject WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, subjectId);
        }
    }
    /// <summary>
    /// 清理题目数据
    /// </summary>
    /// <param name="subjectId">int 题目编号</param>
    public static void Clear(int subjectId)
    {
        string sql = "DELETE FROM tm_mission_subject_client WHERE subjectId=@0;";
        using (MySqlADO ado = new MySqlADO())
        {
            ado.NonQuery(sql, subjectId);
        }
    }
}