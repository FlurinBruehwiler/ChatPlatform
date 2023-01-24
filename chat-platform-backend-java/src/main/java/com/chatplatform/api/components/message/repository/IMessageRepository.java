package com.chatplatform.api.components.message.repository;

import com.chatplatform.api.components.message.entity.Message;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IMessageRepository extends CrudRepository<Message, Long> {}
