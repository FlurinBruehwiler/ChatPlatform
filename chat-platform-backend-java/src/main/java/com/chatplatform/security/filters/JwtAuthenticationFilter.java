package main.java.com.ohmyclass.security.filters;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.chatplatform.security.util.JwtTokenUtil;
import lombok.extern.log4j.Log4j2;
import org.springframework.context.annotation.Lazy;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;
import org.springframework.stereotype.Component;

import javax.servlet.FilterChain;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import static org.springframework.http.MediaType.APPLICATION_JSON_VALUE;

/**
 * This filter is used to validate the users credentials sent by the client.
 *
 * @author z-100
 */
@Log4j2
@Component
public class JwtAuthenticationFilter extends UsernamePasswordAuthenticationFilter {

	private final JwtTokenUtil tokenUtil;

	public JwtAuthenticationFilter(@Lazy AuthenticationManager authenticationManager, JwtTokenUtil tokenUtil) {

		super.setAuthenticationManager(authenticationManager);
		this.tokenUtil = tokenUtil;
	}

	@Override
	public Authentication attemptAuthentication(HttpServletRequest request, HttpServletResponse response)
			throws AuthenticationException {

		String username = request.getParameter("username");
		String password = request.getParameter("password");

		UsernamePasswordAuthenticationToken authenticationToken =
				new UsernamePasswordAuthenticationToken(username, password);

		log.debug("Attempt authentication: {}", username);

		return super.getAuthenticationManager().authenticate(authenticationToken);
	}

	@Override
	protected void successfulAuthentication(HttpServletRequest request, HttpServletResponse response,
			FilterChain chain, Authentication authentication) throws IOException {

		response.setContentType(APPLICATION_JSON_VALUE);

		User user = (User) authentication.getPrincipal();

		String subject = user.getUsername();
		String issuer = request.getRequestURI();
		List<String> rolesClaim = user.getAuthorities().stream()
				.map(GrantedAuthority::getAuthority)
				.collect(Collectors.toList());

		Map<String, String> tokens = tokenUtil.generateNewTokenMap(subject, issuer, rolesClaim);

		log.debug("Return tokens of {}", subject);

		new ObjectMapper().writeValue(response.getOutputStream(), tokens);
	}
}
