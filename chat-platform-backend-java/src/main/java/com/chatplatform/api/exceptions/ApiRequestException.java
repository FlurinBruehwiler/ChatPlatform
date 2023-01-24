package main.java.com.ohmyclass.api.exceptions;

/**
 * Exception thrown when an API request fails.
 *
 * @author z-100
 */
public class ApiRequestException extends RuntimeException {

	public ApiRequestException() {
		super();
	}

	public ApiRequestException(String message) {
		super(message);
	}

	public ApiRequestException(String message, Throwable cause) {
		super(message, cause);
	}
}
