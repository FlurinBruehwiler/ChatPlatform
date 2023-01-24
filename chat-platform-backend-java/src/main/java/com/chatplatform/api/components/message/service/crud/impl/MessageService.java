package com.chatplatform.api.components.message.service.crud.impl;

import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;
import com.chatplatform.api.components.message.repository.IMessageRepository;
import com.chatplatform.api.components.message.service.mapper.MessageMapper;
import com.chatplatform.api.components.message.service.crud.IMessageService;
import lombok.AllArgsConstructor;
import lombok.extern.log4j.Log4j2;

@Log4j2
@Component
@AllArgsConstructor
public class MessageService implements IMessageService {

	private final IMessageRepository repo;

	private final MessageMapper mapper;

	@Override
	public MessageOutDTO createMessage(MessageInDTO inDTO) {

		return repo.save(mapper.inDTOToEntity(inDTO));
	}
}
