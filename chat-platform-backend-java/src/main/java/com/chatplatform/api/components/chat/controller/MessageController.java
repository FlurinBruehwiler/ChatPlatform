package com.chatplatform.api.components.chat.controller;

import com.chatplatform.api.components.chat.dto.ChatOutDTO;
import com.chatplatform.api.components.chat.service.crud.IChatService;
import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;
import lombok.AllArgsConstructor;

@RestController
@AllArgsConstructor
public class MessageController {

	private final IChatService chatService;

	public String getUniqueChatName(Integer groupId) {
		return chatService.getUniqueChatName(groupId);
	}

	public ChatOutDTO getChatById(Integer chatId) {
		return chatService.getChatById(chatId);
	}

	public ChatOutDTO getChatByIdWithAscos(Integer chatId) {
		return chatService.getChatByIdWithAscos(chatId);
	}

	public MessageOutDTO sendMessage(Integer chatId, MessageInDTO message) {
		return chatService.sendMessage(chatId, message);
	}

	public ChatOutDTO inviteUserToChat(Integer chatId, Integer userId) {
		return chatService.inviteUserToChat(chatId, userId);
	}
}
