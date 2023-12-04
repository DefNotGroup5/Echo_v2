package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.UserEntity;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<UserEntity, Integer> {

    @Query("SELECT u FROM UserEntity u WHERE u.email = :email")
    Optional<UserEntity> getByEmail(String email);

    @Query("SELECT u FROM UserEntity u WHERE u.country = :country")
    Optional<UserEntity> getByCountry(String country);
    
    List<UserEntity> findByIsSeller(boolean isSeller);

    @Query("update UserEntity u set u.isAuthorizedSeller = :isAuthorized WHERE u.id = :id")
    void setSellerAuthorization(int id, boolean isAuthorized);
}
