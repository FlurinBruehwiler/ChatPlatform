package main.java.com.ohmyclass.server.properties;

import lombok.Getter;
import lombok.Setter;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.context.annotation.Configuration;

import java.util.List;
import java.util.Map;

@ConfigurationProperties(prefix = "constants.jwt")
@Configuration
@Getter
@Setter
public class JwtConstants {

	private String tokenPrefix;

	private String secret;

	private List<String> uriwhitelist;

	private Map<String, Integer> expiration;

	private Map<String, String> claims;
}
