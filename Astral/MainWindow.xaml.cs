using Astral.ViewModels;
using Astral.Views;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinRT.Interop;

namespace Astral
{
    /// <summary>
    /// 主窗口，包含侧边栏导航
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            RootGrid.DataContext = ViewModel;
            
            // 从 ViewModel 设置导航项
            InitializeNavigationItems();
            
            // 设置初始页面
            if (ViewModel.CurrentPageType != null)
            {
                ContentFrame.Navigate(ViewModel.CurrentPageType);
            }
            
            // 监听当前页面类型变化
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            
            // 设置扩展标题栏，内容延伸到标题栏区域（WinUI风格）
            ExtendsContentIntoTitleBar = true;
            
            // 获取AppWindow以设置系统标题栏样式
            var windowHandle = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            
            if (appWindow != null)
            {
                // 设置标题栏样式，让内容延伸到标题栏区域
                appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                // 使标题栏按钮透明，使用系统主题
                appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            
            // 延迟设置标题栏，确保控件已完全加载
            RootGrid.Loaded += RootGrid_Loaded;
        }

        private void InitializeNavigationItems()
        {
            // 清空现有的导航项
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

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // 当 CurrentPageType 变化时，导航到新页面
            if (e.PropertyName == nameof(ViewModel.CurrentPageType) && ViewModel.CurrentPageType != null)
            {
                var currentPageType = ContentFrame.CurrentSourcePageType;
                
                // 如果目标页面与当前页面不同，进行导航
                // 注意：避免在 OnContentFrameNavigated 触发时造成循环更新
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
                var shouldNavigate = ViewModel.HandleNavigationItemInvoked(item, currentPageType);
                
                // 如果需要导航，将在 ViewModel_PropertyChanged 中处理
                // 这里不需要额外操作，因为 CurrentPageType 的变化会触发导航
            }
        }
    }
}
