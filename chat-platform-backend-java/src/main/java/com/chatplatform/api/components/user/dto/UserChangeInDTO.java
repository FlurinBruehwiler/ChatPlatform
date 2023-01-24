package com.chatplatform.api.components.user.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class UserChangeInDTO extends UserInDTO {

	private String newEmail;

	private String newPassword;
}
