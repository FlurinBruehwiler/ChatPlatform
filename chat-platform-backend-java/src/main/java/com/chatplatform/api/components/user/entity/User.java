package main.java.com.ohmyclass.api.components.user.entity;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.chatplatform.api.components.preferences.entity.Preferences;
import com.chatplatform.api.components.role.entity.Role;
import lombok.Getter;
import lombok.Setter;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
@Entity
@Table(name = "user")
public class User {

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "id", nullable = false)
	private Long id;

	@Column(name = "username")
	private String username;

	@Column(name = "email")
	private String email;

	@Column(name = "password")
	private String password;

	@OneToMany(cascade = {CascadeType.ALL},
			orphanRemoval = true,
			mappedBy = "fkUser")
	private List<Role> roles;

	public void addRole(Role role) {
		if (roles == null)
			roles = new ArrayList<>();

		roles.add(role);
		role.setFkUser(this);
	}

	public void removeRole(Role role) {
		roles.remove(role);
		role.setFkUser(null);
	}
}
