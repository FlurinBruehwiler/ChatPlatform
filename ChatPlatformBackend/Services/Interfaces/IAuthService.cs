using ChatPlatformBackend.Models;

namespace ChatPlatformBackend.Services.Interfaces;

public interface IAuthService
{
    public string CreateToken(User user);
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}