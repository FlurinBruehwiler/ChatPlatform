package com.chatplatform.api.components.chat.dto;

import com.chatplatform.api.components.message.entity.Message;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class ChatInDTO {

	private Long id;

	private List<String> usernames;

	private List<Message> messages;

	private String name;
}
