namespace Astral.Models;

/// <summary>
/// 导航项配置模型
/// </summary>
public record NavigationItemConfig
{
    /// <summary>
    /// 显示内容
    /// </summary>
    public required string Content { get; init; }

    /// <summary>
    /// 图标字符（FontIcon Glyph）
    /// </summary>
    public required string IconGlyph { get; init; }

    /// <summary>
    /// 导航标签
    /// </summary>
    public required string Tag { get; init; }

    /// <summary>
    /// 是否为页脚菜单项
    /// </summary>
    public bool IsFooterItem { get; init; }
}

