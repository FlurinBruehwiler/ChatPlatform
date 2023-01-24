package com.chatplatform.api.components.chat.entity;

import com.chatplatform.api.components.message.entity.Message;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.List;

@Getter
@Setter
@Entity
@Table(name = "chat")
public class Chat {

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "id", nullable = false)
	private Long id;

	@Column
	private List<String> usernames;

	@Column
	private List<Message> messages;

	@Column
	private String name;

	public void addMessage(Message msg) {
		messages.add(msg);
	}

	public void addUser(String userName) {
		usernames.add(userName);
	}
}
