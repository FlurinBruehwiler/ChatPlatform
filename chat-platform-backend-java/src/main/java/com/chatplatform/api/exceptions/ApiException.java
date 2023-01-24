package main.java.com.ohmyclass.api.exceptions;

import com.chatplatform.api.util.validation.ValidationResultEntry;
import com.chatplatform.api.util.validation.http.ValidationStatus;
import org.springframework.http.HttpStatus;

import java.time.ZonedDateTime;

public record ApiException(String message, Throwable cause, HttpStatus status, ZonedDateTime timestamp) {

	public ValidationResultEntry toValidationResultEntry() {
		return new ValidationResultEntry(ValidationStatus.resolveTypeByHttpStatusId(status.value()), message, null/*, List.of(cause.getCause().toString())*/);
	}
}
