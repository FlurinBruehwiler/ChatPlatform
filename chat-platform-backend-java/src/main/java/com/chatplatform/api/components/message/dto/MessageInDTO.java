package com.chatplatform.api.components.message.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class MessageInDTO {

	private Long id;

	private String messageContent;

	private Integer chatId;

	private String username;
}
