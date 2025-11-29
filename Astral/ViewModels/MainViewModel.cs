using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Astral.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 主窗口的ViewModel
/// </summary>
public partial class MainViewModel : ObservableObject
{
    /// <summary>
    /// 当前选中的导航项
    /// </summary>
    [ObservableProperty]
    private NavigationViewItem? _selectedItem;

    /// <summary>
    /// 当前页面类型
    /// </summary>
    [ObservableProperty]
    private Type? _currentPageType;

    /// <summary>
    /// 菜单导航项集合
    /// </summary>
    public ObservableCollection<NavigationViewItem> MenuItems { get; }

    /// <summary>
    /// 页脚菜单项集合
    /// </summary>
    public ObservableCollection<NavigationViewItem> FooterMenuItems { get; }

    /// <summary>
    /// 标签到页面类型的映射
    /// </summary>
    private readonly Dictionary<string, Type> _tagToPageTypeMap;

    public MainViewModel()
    {
        // 初始化导航项映射
        _tagToPageTypeMap = new Dictionary<string, Type>
        {
            { "Home", typeof(HomePage) },
            { "Servers", typeof(ServersPage) },
            { "Settings", typeof(SettingsPage) }
        };

        // 初始化菜单项
        MenuItems = new ObservableCollection<NavigationViewItem>
        {
            new NavigationViewItem
            {
                Content = "主页",
                Icon = new FontIcon { Glyph = "\uE10F" }, // Home icon
                Tag = "Home"
            },
            new NavigationViewItem
            {
                Content = "服务器",
                Icon = new FontIcon { Glyph = "\uE7ED" }, // Server icon
                Tag = "Servers"
            }
        };

        // 初始化页脚菜单项
        FooterMenuItems = new ObservableCollection<NavigationViewItem>
        {
            new NavigationViewItem
            {
                Content = "设置",
                Icon = new FontIcon { Glyph = "\uE713" }, // Settings icon
                Tag = "Settings"
            }
        };

        // 设置默认选中项和页面
        SelectedItem = MenuItems[0];
        CurrentPageType = typeof(HomePage);
    }

    /// <summary>
    /// 根据标签获取页面类型
    /// </summary>
    public Type? GetPageTypeByTag(string? tag)
    {
        if (string.IsNullOrEmpty(tag))
            return null;

        return _tagToPageTypeMap.TryGetValue(tag, out var pageType) ? pageType : null;
    }

    /// <summary>
    /// 处理导航项点击事件
    /// </summary>
    /// <param name="item">被点击的导航项</param>
    /// <param name="currentPageType">当前页面类型，用于检查是否需要导航</param>
    /// <returns>是否需要执行导航</returns>
    public bool HandleNavigationItemInvoked(NavigationViewItem? item, Type? currentPageType)
    {
        if (item?.Tag is not string tag)
            return false;

        var targetPageType = GetPageTypeByTag(tag);
        if (targetPageType == null)
            return false;

        // 如果已经在当前页面，不进行导航
        if (currentPageType == targetPageType)
        {
            // 即使不导航，也要更新选中项（以防UI状态不同步）
            SelectedItem = item;
            return false;
        }

        // 更新当前页面类型和选中项
        CurrentPageType = targetPageType;
        SelectedItem = item;
        return true;
    }
}

