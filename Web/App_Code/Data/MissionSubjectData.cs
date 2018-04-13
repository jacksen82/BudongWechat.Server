using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// MissionSubjectData 的摘要说明
/// </summary>
public class MissionSubjectData
{
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
}