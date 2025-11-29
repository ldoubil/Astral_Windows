using Astral.Constants;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 设置页面ViewModel
/// </summary>
public partial class SettingsViewModel : ViewModelBase
{
    /// <summary>
    /// 深色模式开关
    /// </summary>
    [ObservableProperty]
    private bool _darkMode = AppConstants.Defaults.DarkMode;

    /// <summary>
    /// 语言设置
    /// </summary>
    [ObservableProperty]
    private string _language = AppConstants.Defaults.Language;

    /// <summary>
    /// 可用语言列表
    /// </summary>
    public IReadOnlyList<string> AvailableLanguages { get; } = new[]
    {
        "简体中文",
        "English"
    };

    public SettingsViewModel()
    {
        LoadSettings();
    }

    /// <summary>
    /// 加载设置
    /// </summary>
    private void LoadSettings()
    {
        // 可以从本地存储或配置文件加载设置
        // 这里暂时使用默认值
    }

    /// <summary>
    /// 保存设置
    /// </summary>
    public void SaveSettings()
    {
        // 保存设置到本地存储或配置文件
        // 这里可以添加实际的保存逻辑
    }

    partial void OnDarkModeChanged(bool value)
    {
        // 当深色模式改变时，可以触发主题切换
        // 这里可以添加主题切换逻辑
    }

    partial void OnLanguageChanged(string value)
    {
        // 当语言改变时，可以触发语言切换
        // 这里可以添加语言切换逻辑
        SaveSettings();
    }
}
