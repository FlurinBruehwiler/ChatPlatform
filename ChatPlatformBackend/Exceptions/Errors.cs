namespace ChatPlatformBackend.Exceptions;

public static class Errors
{
    public static readonly Error WrongPassword = new Error("WrongPassword", "The password does not match the user");
    public static readonly Error UsernameAlreadyExists = new Error("UsernameExists", "Another user with this username already exists");
    public static readonly Error ChatNotFound = new Error("ChatNotFound", "No chat with this id found");
    public static readonly Error NoAuth = new Error("NoAuth", "No authentication provided");
    public static readonly Error UserNotFound = new Error("UserNotFound", "No User with this username found");
}