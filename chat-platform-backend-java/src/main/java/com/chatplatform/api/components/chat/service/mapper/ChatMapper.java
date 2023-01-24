package com.chatplatform.api.components.chat.service.mapper;

import com.chatplatform.api.components.chat.dto.ChatInDTO;
import com.chatplatform.api.components.chat.dto.ChatOutDTO;
import com.chatplatform.api.components.chat.entity.Chat;
import com.sun.istack.NotNull;
import org.mapstruct.*;

@Mapper(componentModel = "spring")
public abstract class ChatMapper {

	public abstract ChatOutDTO entityToOutDTO(@NotNull Chat user);

	public abstract Chat inDTOToEntity(ChatInDTO userIn);
}
