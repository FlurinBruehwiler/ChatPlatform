package com.chatplatform.api.components.message.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class MessageOutDTO {

	private Long id;

	private String messageContent;

	private int chatId;

	private String username;
}
