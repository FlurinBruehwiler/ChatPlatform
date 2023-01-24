package main.java.com.ohmyclass.api.components.user.service.crud.impl;

import com.auth0.jwt.interfaces.DecodedJWT;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.chatplatform.api.components.role.entity.Role;
import com.chatplatform.api.components.user.dto.in.UserChangeInDTO;
import com.chatplatform.api.components.user.dto.in.UserInDTO;
import com.chatplatform.api.components.user.dto.out.UserOutDTO;
import com.chatplatform.api.components.user.entity.User;
import com.chatplatform.api.components.user.repository.IUserRepository;
import com.chatplatform.api.components.user.service.crud.IUserService;
import com.chatplatform.api.components.user.service.mapper.AUserMapper;
import com.chatplatform.api.exceptions.ApiRequestException;
import com.chatplatform.api.util.communication.Request;
import com.chatplatform.api.util.communication.Response;
import com.chatplatform.security.util.JwtTokenUtil;
import com.chatplatform.util.validate.Validate;
import lombok.AllArgsConstructor;
import lombok.extern.log4j.Log4j2;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import static org.springframework.http.HttpHeaders.AUTHORIZATION;

@Log4j2
@Component
@AllArgsConstructor
public class UserService implements IUserService {

	private final IUserRepository userRepo;

	private final AUserMapper userMapper;

	private final PasswordEncoder passwordEncoder;

	private final JwtTokenUtil tokenUtil;

	@Override
	public Map<String, String> register(UserInDTO inDTO) {

//		Validate.notNull( "No Payload found", inDTO, inDTO.getPayload());

		UserInDTO userIn = inDTO/*.getPayload()*/;

		if (userRepo.findByUsername(userIn.getUsername()).isPresent())
			throw new ApiRequestException("Username already exists");

		User user = userMapper.inDTOToEntity(userIn);
		user.addRole(new Role("ROLE_USER"));
		saveUser(user);

		if (userRepo.findByUsername(userIn.getUsername()).isEmpty())
			throw new ApiRequestException("Register failed");

		List<String> roles = user.getRoles().stream()
				.map(Role::getName)
				.collect(Collectors.toList());

		return tokenUtil.generateNewTokenMap(user.getUsername(), "Registration", roles);
	}

	@Override
	public void refreshToken(HttpServletRequest request, HttpServletResponse response) {
		String authorizationHeader = request.getHeader(AUTHORIZATION);

		// Guard
		if (!tokenUtil.isValidBearer(authorizationHeader)) {

			throw new ApiRequestException("Invalid bearer token.");
		}

		try {
			DecodedJWT decodedJWT = tokenUtil.extractBearer.apply(authorizationHeader);

			if (tokenUtil.isTokenExpired(decodedJWT)) {

				throw new ApiRequestException("Token expired. Please login again");
			}

			String newAccessToken = createNewAccessToken(request, decodedJWT);

			new ObjectMapper().writeValue(response.getOutputStream(), newAccessToken);

		} catch (Exception e) {
			throw new ApiRequestException("Refreshing token failed");
		}
	}

	@Override
	public UserOutDTO getUser(String username) {

		Validate.notNull(username);

		return userMapper.entityToOutDTO(userRepo.findByUsername(username)
				.orElseThrow(() -> new ApiRequestException("User not found")));
	}

	@Override
	public UserOutDTO update(Request<UserChangeInDTO> userIn) {
		return null;
	}

	@Override
	public Boolean delete(Request<UserInDTO> userIn) {
		return null;
	}

	@Override
	public void passwordForgotten(HttpServletRequest request, HttpServletResponse response) {
	}

	private String createNewAccessToken(HttpServletRequest request, DecodedJWT decodedJWT) {

		User user = userRepo.findByUsername(decodedJWT.getSubject())
				.orElseThrow(() -> new ApiRequestException("User not found for token refresh"));

		String subject = user.getUsername();
		String issuer = request.getRequestURI();
		List<String> rolesClaim = user.getRoles().stream()
				.map(Role::getName)
				.collect(Collectors.toList());

		String newAccessToken = tokenUtil.generateNewAccessToken(subject, issuer, rolesClaim);

		List<SimpleGrantedAuthority> authorities = user.getRoles().stream()
				.map(role -> new SimpleGrantedAuthority(role.getName()))
				.collect(Collectors.toList());

		UsernamePasswordAuthenticationToken authenticationToken =
				new UsernamePasswordAuthenticationToken(user.getUsername(), null, authorities);

		SecurityContextHolder.getContext().setAuthentication(authenticationToken);

		return newAccessToken;
	}

	private User saveUser(User user) {
		log.info("Saving user {}", user.getUsername());
		user.setPassword(passwordEncoder.encode(user.getPassword()));
		return userRepo.save(user);
	}
}
