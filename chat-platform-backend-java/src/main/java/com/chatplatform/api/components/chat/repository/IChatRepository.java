package com.chatplatform.api.components.chat.repository;

import com.chatplatform.api.components.chat.entity.Chat;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IChatRepository extends CrudRepository<Chat, Long> {}
