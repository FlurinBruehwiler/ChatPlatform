package com.chatplatform.api.components.message.service.crud;

import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;

public interface IMessageService {

	MessageOutDTO createMessage(MessageInDTO user);
}
