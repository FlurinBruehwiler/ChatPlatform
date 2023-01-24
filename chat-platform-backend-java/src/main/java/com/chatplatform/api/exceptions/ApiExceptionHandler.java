package main.java.com.ohmyclass.api.exceptions;

import com.chatplatform.api.util.communication.Response;
import com.chatplatform.api.util.validation.ValidationResult;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

import java.time.ZoneId;
import java.time.ZonedDateTime;

/**
 * Handles various exceptions thrown by the API
 *
 * @author z-100
 */
@ControllerAdvice
public class ApiExceptionHandler {

	@ExceptionHandler(value = {ApiRequestException.class})
	public Response<ApiException> handleApiRequestException(ApiRequestException e) {

		HttpStatus status = HttpStatus.BAD_REQUEST;

		ApiException exception = new ApiException(
				e.getMessage(),
				null,
				status,
				ZonedDateTime.now(ZoneId.of("Z"))
		);

		ValidationResult validationResult = ValidationResult.ok();
		validationResult.add(exception.toValidationResultEntry());

		return new Response<>(exception, validationResult);
	}
}
