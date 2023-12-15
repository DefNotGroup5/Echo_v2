package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.UserEntity;

import java.util.List;
import java.util.Optional;

@Repository //Important
public interface UserRepository extends JpaRepository<UserEntity, Integer> {

    //Weird queries. Kinda ew
    @Query("SELECT u FROM UserEntity u WHERE u.id = :id")
    Optional<UserEntity> getById(int id);
    @Query("SELECT u FROM UserEntity u WHERE u.email = :email")
    Optional<UserEntity> getByEmail(String email);

    @Query("SELECT u FROM UserEntity u WHERE u.country = :country")
    Optional<UserEntity> getByCountry(String country);

    @Query("SELECT u FROM UserEntity u WHERE u.isSeller = true")
    List<UserEntity> findByIsSeller();
    @Modifying
    @Query("DELETE FROM UserEntity u WHERE u.id = :userIdToDelete")
    void deleteUserById(@Param("userIdToDelete") int userIdToDelete);

}
