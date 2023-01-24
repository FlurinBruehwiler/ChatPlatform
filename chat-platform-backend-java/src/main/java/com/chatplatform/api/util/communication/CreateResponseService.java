package main.java.com.ohmyclass.api.util.communication;

import com.chatplatform.api.util.validation.ValidationResult;
import com.chatplatform.api.util.validation.http.ValidationStatus;

import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class CreateResponseService {

	public static <T extends Response> void newError(T t, String message, ValidationResult validationResult) {
		validationResult.add(ValidationStatus.ERROR, message);
		t.setValidationResult(validationResult);
	}

	public static <T extends Response> void newSuccess(T t, String message, ValidationResult validationResult) {
		validationResult.add(ValidationStatus.OK, message);
		t.setValidationResult(validationResult);
	}

	public static <T extends HttpServletResponse> void newError(T t, int status, String message) throws IOException {
		t.setStatus(status);
		t.getWriter().write(message);
	}
}
