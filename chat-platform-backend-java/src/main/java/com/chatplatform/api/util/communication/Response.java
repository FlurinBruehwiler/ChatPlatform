package main.java.com.ohmyclass.api.util.communication;

import com.chatplatform.api.util.validation.ValidationResult;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Setter
@Getter
@NoArgsConstructor
public class Response<Payload> {

	private Payload payload;

	private ValidationResult validationResult;

	public Response(Payload payload) {
		this.payload = payload;
		this.validationResult = ValidationResult.ok();
	}

	public Response(Payload payload, ValidationResult validationResult) {
		this.payload = payload;
		this.validationResult = validationResult;
	}

	public static <Data> Response<Data> ok(Data payload) {
		return new Response<>(payload);
	}
}