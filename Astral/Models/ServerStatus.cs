namespace Astral.Models;

/// <summary>
/// 服务器状态枚举
/// </summary>
public enum ServerStatus
{
    /// <summary>
    /// 在线
    /// </summary>
    Online,

    /// <summary>
    /// 离线
    /// </summary>
    Offline,

    /// <summary>
    /// 未知
    /// </summary>
    Unknown
}

/// <summary>
/// 服务器状态扩展方法
/// </summary>
public static class ServerStatusExtensions
{
    /// <summary>
    /// 获取状态的显示文本
    /// </summary>
    public static string GetDisplayText(this ServerStatus status) => status switch
    {
        ServerStatus.Online => "在线",
        ServerStatus.Offline => "离线",
        ServerStatus.Unknown => "未知",
        _ => "未知"
    };
}

