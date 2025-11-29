using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 主页ViewModel
/// </summary>
public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string _welcomeMessage = "欢迎使用 Astral";
}

