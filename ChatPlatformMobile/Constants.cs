namespace ChatPlatformMobile;

public class Constants
{
    public const string TokenKey = "TokenKey";
    public static string Url = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:7087" : "http://localhost:7087";
}