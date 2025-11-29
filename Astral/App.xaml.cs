using Microsoft.UI.Xaml;

namespace Astral;

/// <summary>
/// 提供应用程序特定的行为来补充默认的 Application 类
/// </summary>
public partial class App : Application
{
    private Window? _mainWindow;

    /// <summary>
    /// 初始化单例应用程序对象
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 在应用程序启动时调用
    /// </summary>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _mainWindow = new MainWindow();
        _mainWindow.Activate();
    }
}
