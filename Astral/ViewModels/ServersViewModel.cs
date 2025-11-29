using System.Collections.ObjectModel;
using Astral.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 服务器页面ViewModel
/// </summary>
public partial class ServersViewModel : ViewModelBase
{
    /// <summary>
    /// 服务器列表
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ServerModel> _servers = new();

    public ServersViewModel()
    {
        LoadServersAsync();
    }

    /// <summary>
    /// 异步加载服务器列表
    /// </summary>
    private async void LoadServersAsync()
    {
        IsLoading = true;
        ClearError();

        try
        {
            await Task.Delay(500); // 模拟异步加载

            // 示例数据
            Servers = new ObservableCollection<ServerModel>
            {
                new ServerModel
                {
                    Name = "服务器 1",
                    Address = "example.com",
                    Status = ServerStatus.Online.GetDisplayText()
                },
                new ServerModel
                {
                    Name = "服务器 2",
                    Address = "example2.com",
                    Status = ServerStatus.Offline.GetDisplayText()
                }
            };
        }
        catch (Exception ex)
        {
            SetError($"加载服务器列表失败: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

