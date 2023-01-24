package com.chatplatform.api.components.message.service.mapper;

import com.chatplatform.api.components.message.dto.MessageInDTO;
import com.chatplatform.api.components.message.dto.MessageOutDTO;
import com.chatplatform.api.components.message.entity.Message;
import com.chatplatform.api.components.preferences.service.mapper.APreferencesMapper;
import com.chatplatform.api.components.user.dto.in.UserInDTO;
import com.chatplatform.api.components.user.dto.out.UserOutDTO;
import com.chatplatform.api.components.user.entity.User;
import com.sun.istack.NotNull;
import org.mapstruct.*;

@Mapper(componentModel = "spring", uses = APreferencesMapper.class)
public abstract class MessageMapper {

	public abstract MessageOutDTO entityToOutDTO(@NotNull Message user);

	public abstract Message inDTOToEntity(MessageInDTO userIn);
}
