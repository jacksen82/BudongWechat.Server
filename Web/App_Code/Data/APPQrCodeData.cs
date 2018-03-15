using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 小程序二维码操作类
/// </summary>
public class APPQrCodeData
{
    /// <summary>
    /// 生成小程序二维码
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="scene">string 携带数据</param>
    /// <param name="page">string 启动页面</param>
    /// <param name="qrcodeUrl">string 二维码地址</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int appId, string scene, string page, string qrcodeUrl)
    {
        string sql = " INSERT INTO ta_app_qrcode (appId,scene,page,qrcodeUrl) VALUES(@0,@1,@2,@3) ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, appId, scene, page, qrcodeUrl);
        }
    }
}