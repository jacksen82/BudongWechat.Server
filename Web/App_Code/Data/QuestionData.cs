using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 题目操作类
/// </summary>
public class QuestionData
{
    /// <summary>
    /// 获取题目信息
    /// </summary>
    /// <param name="questionId">int 题目编号</param>
    /// <returns>Hash 题目详细信息</returns>
    public static Hash GetByQuestionId(int questionId)
    {
        string sql = "SELECT * FROM tq_question WHERE questionId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, questionId);
        }
    }
}