package main.java.com.ohmyclass.server.properties;

import lombok.Getter;
import lombok.Setter;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.context.annotation.Configuration;

import java.util.Map;

@ConfigurationProperties(prefix = "constants.api")
@Configuration
@Getter
@Setter
public class ApiProperties {

	private Map<String, String> urls;
}
