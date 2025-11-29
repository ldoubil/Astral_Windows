namespace Astral.Models;

/// <summary>
/// 服务器模型
/// </summary>
public class ServerModel
{
    /// <summary>
    /// 服务器名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 服务器地址
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// 服务器状态
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

