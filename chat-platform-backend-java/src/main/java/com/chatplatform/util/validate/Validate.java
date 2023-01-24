package main.java.com.ohmyclass.util.validate;

import com.chatplatform.api.exceptions.ApiRequestException;

import java.util.Arrays;
import java.util.Objects;

public class Validate {

	@SafeVarargs
	public static <T> void notNull(T... t) {
		if (Arrays.stream(t).anyMatch(Objects::isNull))
			throw new ApiRequestException();
	}

	@SafeVarargs
	public static <T> void notNull(String message, T... t) {
		if (Arrays.stream(t).anyMatch(Objects::isNull))
			throw new ApiRequestException(message);
	}
}
