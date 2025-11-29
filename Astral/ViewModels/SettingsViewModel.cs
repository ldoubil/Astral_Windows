using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 设置页面ViewModel
/// </summary>
public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _darkMode;

    [ObservableProperty]
    private string _language = "简体中文";
}

