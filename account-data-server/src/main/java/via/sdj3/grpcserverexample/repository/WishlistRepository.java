package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.entities.WishlistEntity;

import java.util.List;
import java.util.Optional;

@Repository
public interface WishlistRepository extends JpaRepository<WishlistEntity, Integer>
{
  @Query("SELECT wi FROM WishlistEntity wi WHERE wi.id = :id")
  Optional<WishlistEntity> getById(@Param("id") int id);

  @Query("SELECT wi FROM WishlistEntity wi WHERE wi.item = :item")
  Optional<WishlistEntity> getByItem(@Param("item") ItemEntity item);

  @Query("SELECT wi FROM WishlistEntity wi WHERE wi.user = :user")
  List<WishlistEntity> getByUser(@Param("user") UserEntity user);
}
