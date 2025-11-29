namespace Astral.Models;

/// <summary>
/// 服务器模型
/// </summary>
public record ServerModel
{
    /// <summary>
    /// 服务器名称
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// 服务器地址
    /// </summary>
    public required string Address { get; init; }

    /// <summary>
    /// 服务器状态
    /// </summary>
    public required string Status { get; init; }
}

