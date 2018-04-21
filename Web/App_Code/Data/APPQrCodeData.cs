using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 小程序二维码操作类
/// </summary>
public class APPQrCodeData
{
    /// <summary>
    /// 根据编号获取二维码详细信息
    /// </summary>
    /// <param name="qrcodeId">int 二维码编号</param>
    /// <returns>Hash 返回信息</returns>
    public static Hash GetById(int qrcodeId)
    {
        string sql = "SELECT * FROM ta_app_qrcode WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, qrcodeId);
        }
    }
    /// <summary>
    /// 根据唯一编码获取二维码详细信息
    /// </summary>
    /// <param name="unionId">string 二维码编码</param>
    /// <returns>Hash 返回信息</returns>
    public static Hash GetByUnionId(string unionId)
    {
        string sql = "SELECT * FROM ta_app_qrcode WHERE unionId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, unionId);
        }
    }
    /// <summary>
    /// 生成小程序二维码
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="unionId">string 唯一编码</param>
    /// <param name="scene">string 携带数据</param>
    /// <param name="page">string 启动页面</param>
    /// <param name="qrcodeUrl">string 二维码地址</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int appId, string unionId, string scene, string page, string qrcodeUrl)
    {
        string sql = " INSERT INTO ta_app_qrcode (appId,unionId,scene,page,qrcodeUrl) VALUES(@0,@1,@2,@3,@4) ON DUPLICATE KEY UPDATE unionId=VALUES(unionId) ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, appId, unionId, scene, page, qrcodeUrl);
        }
    }
}