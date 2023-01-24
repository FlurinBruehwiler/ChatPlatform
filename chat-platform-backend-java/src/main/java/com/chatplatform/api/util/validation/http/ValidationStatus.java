package main.java.com.ohmyclass.api.util.validation.http;

import lombok.Getter;

public enum ValidationStatus {
	OK(200, Series.SUCCESSFUL, "OK"),
	INFO(204, Series.INFORMATIONAL, "Information"),
	WARNING(400, Series.SUCCESSFUL, "Warning"),
	ERROR(401, Series.SERVER_ERROR, "Error");

	int value;
	Series series;
	String reasonPhrase;

	ValidationStatus(int value, Series series, String reasonPhrase) {}

	public static ValidationStatus resolveTypeByHttpStatusId(int id) {
		for (ValidationStatus status : values()) {
			if (status.value == id) {
				return status;
			}
		}
		return null;
	}

	@Getter
	public enum Series {
		INFORMATIONAL(1),
		SUCCESSFUL(2),
		SERVER_ERROR(5);

		private int value;

		Series(int value) {}

		boolean isSuccessful(int status) {
			return status == SUCCESSFUL.value;
		}
	}
}
