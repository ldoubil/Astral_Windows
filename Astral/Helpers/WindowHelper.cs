using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace Astral.Helpers;

/// <summary>
/// 窗口辅助类
/// </summary>
public static class WindowHelper
{
    /// <summary>
    /// 配置窗口的标题栏样式
    /// </summary>
    public static void ConfigureTitleBar(Window window)
    {
        window.ExtendsContentIntoTitleBar = true;

        var windowHandle = WindowNative.GetWindowHandle(window);
        var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        if (appWindow != null)
        {
            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
    }
}

