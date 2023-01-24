namespace ChatPlatformBackend.Exceptions;

public static class Errors
{
    public static readonly Error WrongPassword = new("WrongPassword", "The password does not match the user");
    public static readonly Error UsernameAlreadyExists = new("UsernameExists", "Another user with this username already exists");
    public static readonly Error ChatNotFound = new("ChatNotFound", "No chat with this id found");
    public static readonly Error NoAuth = new("NoAuth", "No authentication provided");
    public static readonly Error UserNotFound = new("UserNotFound", "No User with this username found");
    public static readonly Error UserNotInChat = new("UserNotInChat", "The User is not member of the chat");
    public static readonly Error PasswordToWeak = new("PasswordToWeak", "Password must be at least 6 characters long");
    public static readonly Error UsernameToShort = new("UsernameToShort", "The username must be at least 3 characters long");
}