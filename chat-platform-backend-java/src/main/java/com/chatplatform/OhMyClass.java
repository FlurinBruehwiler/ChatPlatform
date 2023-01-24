package main.java.com.ohmyclass;

import com.chatplatform.server.properties.ApiProperties;
import lombok.extern.log4j.Log4j2;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.security.servlet.SecurityAutoConfiguration;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.context.event.EventListener;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.web.servlet.mvc.method.annotation.RequestMappingHandlerMapping;

@Log4j2
@SpringBootApplication(exclude = SecurityAutoConfiguration.class)
@EnableConfigurationProperties
@EnableGlobalMethodSecurity(securedEnabled = true, prePostEnabled = true)
public class OhMyClass {

	public static void main(String[] args) {

		SpringApplication.run(OhMyClass.class, args);
	}

	@EventListener
	public void handleContextRefresh(ContextRefreshedEvent event) {

		// This is a hack to force Spring to load all the endpoints at startup.
		event.getApplicationContext().getBean(RequestMappingHandlerMapping.class).getHandlerMethods()
				.forEach((key, value) -> System.out.println(key + " - " + value));
	}
}
