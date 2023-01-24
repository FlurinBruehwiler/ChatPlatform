package com.chatplatform.api.components.chat.service.crud.impl;

import com.chatplatform.api.components.chat.dto.ChatOutDTO;
import com.chatplatform.api.components.chat.entity.Chat;
import com.chatplatform.api.components.chat.repository.IChatRepository;
import com.chatplatform.api.components.chat.service.crud.IChatService;
import com.chatplatform.api.components.chat.service.mapper.ChatMapper;
import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;
import com.chatplatform.api.components.message.entity.Message;
import com.chatplatform.api.components.message.repository.IMessageRepository;
import com.chatplatform.api.components.message.service.mapper.MessageMapper;
import lombok.AllArgsConstructor;
import lombok.extern.log4j.Log4j2;

@Log4j2
@Component
@AllArgsConstructor
public class ChatService implements IChatService {

	private final IChatRepository repo;

	private final IMessageRepository messageRepo;

	private final main.java.com.ohmyclass.api.components.user.repository.IUserRepository userRepository;

	private final ChatMapper mapper;

	private final MessageMapper messageMapper;

	@Override
	public String getUniqueChatName(Integer chatId) {
		return repo.findById(chatId).getName();
	}

	@Override
	public ChatOutDTO getChatById(Integer chatId) {
		return mapper.entityToOutDTO(repo.findById(chatId));
	}

	@Override
	public ChatOutDTO getChatByIdWithAscos(Integer chatId) {

		Chat chat = repo.findById(chatId);

		chat.setMessages(messageRepo.findAllByChatId(chatId));

		return mapper.entityToOutDTO(chat);
	}

	@Override
	public MessageOutDTO sendMessage(Integer chatId, MessageInDTO message) {

		Message messageInDB = messageRepo.save(messageMapper.inDTOToEntity(message));

		Chat chat = repo.findById(chatId);

		chat.addMessage(messageInDB);

		return mapper.entityToOutDTO(repo.save(chat));
	}

	@Override
	public ChatOutDTO inviteUserToChat(Integer chatId, Integer userId) {

		main.java.com.ohmyclass.api.components.user.entity.User user = userRepository.findById(userId);

		Chat chat = repo.findById(chatId);

		chat.addUser(user.getUsername());

		return mapper.entityToOutDTO(repo.save(chat));
	}
}
