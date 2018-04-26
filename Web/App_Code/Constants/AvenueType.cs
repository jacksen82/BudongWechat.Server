
/// <summary>
/// 余额途径枚举
/// </summary>
public enum AvenueType : int
{
    /// <summary>
    /// 注册奖励
    /// </summary>
    Register = 1100,
    /// <summary>
    /// 分享到群，仅该群首次分享有效
    /// </summary>
    ShareToGroup = 1100,
    /// <summary>
    /// 创建关卡
    /// </summary>
    MissionCreate = 1300,

    /// <summary>
    /// 获取提示
    /// </summary>
    GameTip = 2100,
    /// <summary>
    /// 跳过题目
    /// </summary>
    GameSkip = 2101,
    /// <summary>
    /// 重听题目音频
    /// </summary>
    GameReplay = 2102,
    /// <summary>
    /// 闯关奖励
    /// </summary>
    GameSuccess = 2200,

    /// <summary>
    /// 未知错误
    /// </summary>
    Unknown = 9999
}