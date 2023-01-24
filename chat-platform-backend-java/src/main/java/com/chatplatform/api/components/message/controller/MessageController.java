package com.chatplatform.api.components.message.controller;

import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;
import com.chatplatform.api.components.message.service.crud.IMessageService;
import lombok.AllArgsConstructor;

@RestController
@AllArgsConstructor
public class MessageController {

	private final IMessageService messageService;

	public MessageOutDTO createMessage(MessageInDTO inDTO) {
		return messageService.createMessage(inDTO);
	}
}
