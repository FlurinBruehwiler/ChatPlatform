package main.java.com.ohmyclass.security.util;

import com.auth0.jwt.JWT;
import com.auth0.jwt.JWTVerifier;
import com.auth0.jwt.algorithms.Algorithm;
import com.auth0.jwt.interfaces.Claim;
import com.auth0.jwt.interfaces.DecodedJWT;
import com.chatplatform.server.properties.JwtConstants;
import org.springframework.stereotype.Component;

import java.nio.charset.StandardCharsets;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Function;

/**
 * Util to handle JWT tokens
 *
 * @author z-100
 */
@Component
public class JwtTokenUtil {

	private JwtConstants constants;

	public Function<String, DecodedJWT> extractBearer = authHeaderIn -> {
		String token = authHeaderIn.substring(constants.getTokenPrefix().length());
		JWTVerifier verifier = JWT.require(algorithm()).build();

		return verifier.verify(token);
	};


	public JwtTokenUtil(JwtConstants constants) {
		this.constants = constants;
	}

	public Map<String, String> generateNewTokenMap(String subject, String issuer, List<?> roles) {

		Map<String, String> tokens = new HashMap<>();
		tokens.put("access_token", generateNewAccessToken(subject, issuer, roles));
		tokens.put("refresh_token", generateNewRefreshToken(subject, issuer));

		return tokens;
	}

	public String generateNewAccessToken(String subject, String issuer, List<?> roles) {
		return JWT.create()
				.withSubject(subject)
				.withExpiresAt(new Date(System.currentTimeMillis() + constants.getExpiration().get("access_token")))
				.withIssuer(issuer)
				.withClaim("rls", roles)
				.sign(algorithm());
	}

	public String generateNewRefreshToken(String subject, String issuer) {
		return JWT.create()
				.withSubject(subject)
				.withExpiresAt(new Date(System.currentTimeMillis() + constants.getExpiration().get("refresh_token")))
				.withIssuer(issuer)
				.sign(algorithm());
	}

	public String getUsernameFromToken(DecodedJWT token) {

		return token.getSignature();
	}

	public Date getExpirationDateFromToken(DecodedJWT token) {

		return token.getExpiresAt();
	}

	public boolean isTokenExpired(DecodedJWT token) {

		final Date expiration = getExpirationDateFromToken(token);

		return expiration.before(new Date());
	}

	public boolean isValidBearer(String token) {

		return token != null && token.startsWith(constants.getTokenPrefix());
	}

	private Map<String, Claim> getAllClaimsFromToken(DecodedJWT token) {

		return token.getClaims();
	}

	private Algorithm algorithm() {

		return Algorithm.HMAC512(constants.getSecret().getBytes(StandardCharsets.UTF_8));
	}
}
