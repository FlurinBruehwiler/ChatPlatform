using System.Collections.ObjectModel;
using ChatPlatformBackend.DtoModels;

namespace ChatPlatformMobile;

public partial class ChatOverviewPage
{
    private readonly SyncService _syncService;

    public ObservableCollection<DtoChat> Chats { get; set; }
    
    public ChatOverviewPage(SyncService syncService)
    {
        InitializeComponent();

        _syncService = syncService;
        
        Start();
    }

    private async void Start()
    {
        var user = await _syncService.GetCurrentUser();
        var chats = await _syncService.GetChats();
        Chats = new ObservableCollection<DtoChat>(chats);
    }
}