package com.chatplatform.api.components.chat.service.crud;

import com.chatplatform.api.components.chat.dto.ChatOutDTO;
import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;

public interface IChatService {

	String getUniqueChatName(Integer groupId);

	ChatOutDTO getChatById(Integer chatId);

	ChatOutDTO getChatByIdWithAscos(Integer chatId);

	MessageOutDTO sendMessage(Integer chatId, MessageInDTO message);

	ChatOutDTO inviteUserToChat(Integer chatId, Integer userId);
}
