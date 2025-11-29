using Astral.Helpers;
using Astral.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Astral;

/// <summary>
/// 主窗口，包含侧边栏导航
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        
        // 创建 ViewModel
        ViewModel = new MainViewModel();
        RootGrid.DataContext = ViewModel;
        
        // 初始化导航项
        InitializeNavigationItems();
        
        // 设置初始页面
        NavigateToInitialPage();
        
        // 监听当前页面类型变化
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        
        // 配置标题栏
        WindowHelper.ConfigureTitleBar(this);
        
        // 延迟设置标题栏可拖拽区域
        RootGrid.Loaded += RootGrid_Loaded;
    }

    private void InitializeNavigationItems()
    {
        RootNavigationView.MenuItems.Clear();
        RootNavigationView.FooterMenuItems.Clear();
        
        // 从 ViewModel 添加菜单项
        foreach (var item in ViewModel.MenuItems)
        {
            RootNavigationView.MenuItems.Add(item);
        }
        
        // 从 ViewModel 添加页脚菜单项
        foreach (var item in ViewModel.FooterMenuItems)
        {
            RootNavigationView.FooterMenuItems.Add(item);
        }
        
        // 设置初始选中项
        if (ViewModel.SelectedItem != null)
        {
            RootNavigationView.SelectedItem = ViewModel.SelectedItem;
        }
    }

    private void NavigateToInitialPage()
    {
        if (ViewModel.CurrentPageType != null)
        {
            ContentFrame.Navigate(ViewModel.CurrentPageType);
        }
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // 当 CurrentPageType 变化时，导航到新页面
        if (e.PropertyName == nameof(ViewModel.CurrentPageType) && ViewModel.CurrentPageType != null)
        {
            var currentPageType = ContentFrame.CurrentSourcePageType;
            
            // 如果目标页面与当前页面不同，进行导航
            if (currentPageType != ViewModel.CurrentPageType)
            {
                ContentFrame.Navigate(ViewModel.CurrentPageType);
            }
        }
    }

    private void RootGrid_Loaded(object sender, RoutedEventArgs e)
    {
        // 设置自定义标题栏可拖拽区域
        SetTitleBar(titleBar);
    }

    private void TitleBar_BackRequested(TitleBar sender, object args)
    {
        if (ContentFrame.CanGoBack)
        {
            ContentFrame.GoBack();
        }
    }

    private void TitleBar_PaneToggleRequested(TitleBar sender, object args)
    {
        RootNavigationView.IsPaneOpen = !RootNavigationView.IsPaneOpen;
    }

    private void OnContentFrameNavigated(object sender, NavigationEventArgs e)
    {
        // 更新返回按钮的可见性
        titleBar.IsBackButtonVisible = ContentFrame.CanGoBack;
    }

    private void OnNavigationViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        // 使用 ViewModel 处理导航逻辑
        if (args.InvokedItemContainer is NavigationViewItem item)
        {
            var currentPageType = ContentFrame.CurrentSourcePageType;
            ViewModel.HandleNavigationItemInvoked(item, currentPageType);
        }
    }
}
