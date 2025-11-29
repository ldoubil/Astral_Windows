using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// ViewModel 基类，提供通用功能
/// </summary>
public abstract partial class ViewModelBase : ObservableObject
{
    /// <summary>
    /// 是否正在加载
    /// </summary>
    [ObservableProperty]
    private bool _isLoading;

    /// <summary>
    /// 错误消息
    /// </summary>
    [ObservableProperty]
    private string? _errorMessage;

    /// <summary>
    /// 是否有错误
    /// </summary>
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    /// <summary>
    /// 清除错误消息
    /// </summary>
    protected void ClearError()
    {
        ErrorMessage = null;
    }

    /// <summary>
    /// 设置错误消息
    /// </summary>
    protected void SetError(string? message)
    {
        ErrorMessage = message;
    }
}

