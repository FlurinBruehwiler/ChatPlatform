package main.java.com.ohmyclass.api.components.user.repository;

import com.chatplatform.api.components.user.entity.User;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface IUserRepository extends CrudRepository<User, Long> {

	Optional<User> findById(Long id);

	Optional<User> findByUsername(String username);

	Optional<User> findByEmailAndPassword(String email, String password);

	Optional<User> findByUsernameOrEmail(String username, String email);

	Optional<User> findByUsernameAndPassword(String username, String password);
}
