namespace ChatPlatformBackend.DtoModels;

public record DtoMessage(string MessageContent, string Username, int ChatId, int MessageId);