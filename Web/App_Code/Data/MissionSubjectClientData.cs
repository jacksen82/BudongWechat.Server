using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// MissionSubjectClientData 的摘要说明
/// </summary>
public class MissionSubjectClientData
{
    /// <summary>
    /// 插入用户关卡题目记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <param name="resultType">int 结果类型 [ 100 = 成功, 10 = 跳过 ]</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, int missionId, int subjectId, int resultType, int secondCount)
    {
        string sql = "INSERT INTO tm_mission_subject_client (clientId,missionId,subjectId,resultType,secondCount) VALUES(@0,@1,@2,@3,@4) ON DUPLICATE KEY UPDATE clientId=VALUES(clientId), missionId=VALUES(missionId) AND subjectId=VALUES(subjectId)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, missionId, subjectId, resultType, secondCount);
        }
    }
}