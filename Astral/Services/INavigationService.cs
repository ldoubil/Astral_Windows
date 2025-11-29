using Astral.Models;

namespace Astral.Services;

/// <summary>
/// 导航服务接口
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// 获取所有导航项配置
    /// </summary>
    IReadOnlyList<NavigationItemConfig> GetNavigationConfigs();

    /// <summary>
    /// 根据标签获取页面类型
    /// </summary>
    Type? GetPageTypeByTag(string? tag);

    /// <summary>
    /// 根据标签获取导航项配置
    /// </summary>
    NavigationItemConfig? GetConfigByTag(string? tag);

    /// <summary>
    /// 获取默认页面类型
    /// </summary>
    Type GetDefaultPageType();
}

