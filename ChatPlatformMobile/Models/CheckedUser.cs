using ChatPlatformBackend.DtoModels;

namespace ChatPlatformMobile.Models;

public class CheckedUser
{
    public CheckedUser(DtoUser User, bool Enabled)
    {
        this.User = User;
        this.Enabled = Enabled;
    }

    public DtoUser User { get; init; }
    public bool Enabled { get; init; }
    
}