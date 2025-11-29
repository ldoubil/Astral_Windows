using Astral.Constants;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 主页ViewModel
/// </summary>
public partial class HomeViewModel : ViewModelBase
{
    /// <summary>
    /// 欢迎消息
    /// </summary>
    [ObservableProperty]
    private string _welcomeMessage = $"欢迎使用 {AppConstants.AppName}";

    public HomeViewModel()
    {
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        IsLoading = true;
        try
        {
            // 可以在这里执行异步初始化操作
            await Task.Delay(1); // 示例：模拟异步操作
        }
        catch (Exception ex)
        {
            SetError($"初始化失败: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

