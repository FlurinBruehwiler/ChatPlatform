using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ChatPlatformBackend.Models;
using ChatPlatformBackend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;

namespace ChatPlatformBackend.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

        var key = new SymmetricSecurityKey(UTF8.GetBytes(_configuration.GetSection("JwtSecret").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(100),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(UTF8.GetBytes(password));
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    public void AppendAccessToken(HttpResponse httpResponse, User user)
    {
        httpResponse.Cookies.Append("X-Access-Token2", CreateToken(user), new CookieOptions
        {
            SameSite = SameSiteMode.Strict,
            Secure = false,
            Domain = null,
            HttpOnly = false,
            IsEssential = true,
        });
        //Access-Control-Allow-Credentials
        httpResponse.Headers.AccessControlAllowCredentials = "true";
        httpResponse.Headers.AccessControlAllowOrigin = "true";
    }
}