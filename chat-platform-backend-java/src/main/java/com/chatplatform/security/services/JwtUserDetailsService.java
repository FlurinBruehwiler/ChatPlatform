package main.java.com.ohmyclass.security.services;

import com.chatplatform.api.components.user.entity.User;
import com.chatplatform.api.components.user.repository.IUserRepository;
import lombok.RequiredArgsConstructor;
import lombok.extern.log4j.Log4j2;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Retrieves a {@link User} object from the database and
 * converts it into a {@link UserDetails} object.
 *
 * @author z-100
 */
@Log4j2
@Service
@RequiredArgsConstructor
public class JwtUserDetailsService implements UserDetailsService {

	private final IUserRepository userRepo;

	@Override
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {

		final User user = userRepo.findByUsername(username)
				.orElseThrow(() -> new UsernameNotFoundException("User not found"));

		log.debug("User found: {}", username);

		List<SimpleGrantedAuthority> authorities = user.getRoles().stream()
				.map(role -> new SimpleGrantedAuthority(role.getName()))
				.collect(Collectors.toList());

		return new org.springframework.security.core.userdetails.
				User(user.getUsername(), user.getPassword(), !authorities.isEmpty() ? authorities : new ArrayList<>());
	}
}
