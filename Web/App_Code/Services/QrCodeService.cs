using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// 二维码业务操作类
/// </summary>
public class QrCodeService
{
    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="path">string 启动路径</param>
    /// <param name="scene">string 二维码需要包含的信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Create(Hash client, string page, string scene)
    {
        Hash accessToken = APPService.GetAccessToken(client.ToInt("appId"));
        if (accessToken.ToInt("code") == 0)
        {
            byte[] buffer;
            string qrcodeUrl = "/Uploads/QrCode/qrcode_" + Files.NewFileName(".png");
            Hash query = new Hash();
            query["scene"] = scene;
            query["page"] = page;
            query["width"] = "430";
            Hash result = API.GetQrCode(accessToken.ToHash("data").ToString("accessToken"), query.ToJSON(), out buffer);
            if (result.ToInt("errcode") == 0)
            {
                Files.SaveAllBytes(Files.MapPath(qrcodeUrl), buffer);
                APPQrCodeData.Create(client.ToInt("appId"), scene, page, qrcodeUrl);
                return new Hash((int)CodeType.OK, "成功");
            }
            return new Hash((int)CodeType.WechatAPIError, result.ToString("errmsg"));
        }
        return new Hash((int)CodeType.WechatAccessTokenInvalid,"accessToken 无效");
    }
}
