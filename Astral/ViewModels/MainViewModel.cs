using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Astral.Models;
using Astral.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 主窗口的ViewModel
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

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

    public MainViewModel(INavigationService? navigationService = null)
    {
        _navigationService = navigationService ?? new NavigationService();

        // 从服务获取导航配置并创建导航项
        var configs = _navigationService.GetNavigationConfigs();
        MenuItems = new ObservableCollection<NavigationViewItem>();
        FooterMenuItems = new ObservableCollection<NavigationViewItem>();

        foreach (var config in configs)
        {
            var navigationItem = CreateNavigationViewItem(config);
            
            if (config.IsFooterItem)
            {
                FooterMenuItems.Add(navigationItem);
            }
            else
            {
                MenuItems.Add(navigationItem);
            }
        }

        // 设置默认选中项和页面
        if (MenuItems.Count > 0)
        {
            SelectedItem = MenuItems[0];
        }
        
        CurrentPageType = _navigationService.GetDefaultPageType();
    }

    /// <summary>
    /// 根据导航配置创建 NavigationViewItem
    /// </summary>
    private static NavigationViewItem CreateNavigationViewItem(NavigationItemConfig config)
    {
        return new NavigationViewItem
        {
            Content = config.Content,
            Icon = new FontIcon { Glyph = config.IconGlyph },
            Tag = config.Tag
        };
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

        var targetPageType = _navigationService.GetPageTypeByTag(tag);
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
