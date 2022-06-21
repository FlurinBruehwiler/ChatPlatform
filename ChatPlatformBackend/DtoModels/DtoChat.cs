namespace ChatPlatformBackend.DtoModels;

public record DtoChat(List<string> Usernames, List<DtoMessage> Messages, string Name, int ChatId);