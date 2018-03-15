using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端日志操作类
/// </summary>
public class ClientLogData
{
    /// <summary>
    /// 插入日志
    /// </summary>
    /// <param name="session3rd">string 三方标识</param>
    /// <param name="scene">int 来源场景</param>
    /// <param name="url">string 访问路径</param>
    /// <param name="query">string 请求参数</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(string session3rd, int scene, string url, string query)
    {
        string sql = "INSERT INTO tc_client_log (session3rd,scene,url,query) VALUES(@0,@1,@2,@3)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, session3rd, scene, url, query);
        }
    }
}