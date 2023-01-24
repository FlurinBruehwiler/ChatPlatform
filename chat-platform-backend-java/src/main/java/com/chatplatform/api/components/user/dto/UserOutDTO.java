package com.chatplatform.api.components.user.dto;

import com.chatplatform.api.components.preferences.dto.out.PreferencesOutDTO;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class UserOutDTO {

	private Long id;

	private String username;

	private PreferencesOutDTO preferencesOut;
}
