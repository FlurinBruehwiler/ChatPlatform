package com.chatplatform.api.components.user.controller;

import com.chatplatform.api.components.user.controller.IUserController;
import com.chatplatform.api.components.user.dto.in.UserChangeInDTO;
import com.chatplatform.api.components.user.dto.in.UserInDTO;
import com.chatplatform.api.components.user.dto.out.UserOutDTO;

import com.chatplatform.api.components.user.service.crud.IUserService;
import com.chatplatform.api.util.communication.Request;
import com.chatplatform.api.util.communication.Response;
import com.chatplatform.api.util.validation.ValidationResult;
import lombok.AllArgsConstructor;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.Map;

@RestController
@AllArgsConstructor
public class UserController implements IUserController {

	private final IUserService userService;

	@Override
	public Response<Map<String, String>> register(UserInDTO registration) {
		return new Response<>(userService.register(registration), ValidationResult.ok());
	}

	@Override
	public void refreshToken(HttpServletRequest request, HttpServletResponse response) {
		userService.refreshToken(request, response);
	}

	@Override
	public void passwordForgotten(HttpServletRequest request, HttpServletResponse response) {
		userService.passwordForgotten(request, response);
	}

	@Override
	public Response<UserOutDTO> getUser(String username) {
		return new Response<>(userService.getUser(username), ValidationResult.ok());
	}

	@Override
	public Response<UserOutDTO> updateUser(Request<UserChangeInDTO> userChangeIn) {
		return new Response<>(userService.update(userChangeIn), ValidationResult.ok());
	}

	@Override
	public Response<Boolean> deleteUser(Request<UserInDTO> user) {
		return new Response<>(userService.delete(user), ValidationResult.ok());
	}
}
