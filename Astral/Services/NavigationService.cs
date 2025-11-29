using System.Collections.Frozen;
using Astral.Constants;
using Astral.Models;
using Astral.Views;

namespace Astral.Services;

/// <summary>
/// 导航服务实现
/// </summary>
public sealed class NavigationService : INavigationService
{
    private static readonly NavigationItemConfig[] NavigationConfigs = new[]
    {
        new NavigationItemConfig
        {
            Content = "主页",
            IconGlyph = Icons.Home,
            Tag = NavigationTags.Home,
            IsFooterItem = false
        },
        new NavigationItemConfig
        {
            Content = "服务器",
            IconGlyph = Icons.Server,
            Tag = NavigationTags.Servers,
            IsFooterItem = false
        },
        new NavigationItemConfig
        {
            Content = "设置",
            IconGlyph = Icons.Settings,
            Tag = NavigationTags.Settings,
            IsFooterItem = true
        }
    };

    private static readonly FrozenDictionary<string, Type> TagToPageTypeMap = new Dictionary<string, Type>
    {
        { NavigationTags.Home, typeof(HomePage) },
        { NavigationTags.Servers, typeof(ServersPage) },
        { NavigationTags.Settings, typeof(SettingsPage) }
    }.ToFrozenDictionary();

    /// <inheritdoc/>
    public IReadOnlyList<NavigationItemConfig> GetNavigationConfigs() => NavigationConfigs;

    /// <inheritdoc/>
    public Type? GetPageTypeByTag(string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return null;

        return TagToPageTypeMap.TryGetValue(tag, out var pageType) ? pageType : null;
    }

    /// <inheritdoc/>
    public NavigationItemConfig? GetConfigByTag(string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return null;

        return NavigationConfigs.FirstOrDefault(config => config.Tag == tag);
    }

    /// <inheritdoc/>
    public Type GetDefaultPageType() => typeof(HomePage);
}

