using System.Collections.ObjectModel;
using Astral.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Astral.ViewModels;

/// <summary>
/// 服务器页面ViewModel
/// </summary>
public partial class ServersViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ServerModel> _servers = new();

    public ServersViewModel()
    {
        // 示例数据
        Servers.Add(new ServerModel { Name = "服务器 1", Address = "example.com", Status = "在线" });
        Servers.Add(new ServerModel { Name = "服务器 2", Address = "example2.com", Status = "离线" });
    }
}

