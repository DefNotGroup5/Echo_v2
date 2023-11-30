package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;

import java.util.Optional;

public interface ItemRepository extends JpaRepository<ItemEntity, Integer> {

    @Query("SELECT i FROM ItemEntity i WHERE i.name = :name")
    Optional<ItemEntity> getByName(String name);

    @Query("SELECT i FROM ItemEntity i WHERE i.id = :id")
    Optional<ItemEntity> getById(int id);

    @Query("SELECT i from ItemEntity i ORDER BY i.price DESC ")
    Optional<ItemEntity> getByLowestPriceToHighest (int price);

    @Query("SELECT i from ItemEntity i ORDER BY i.price ASC")
    Optional<ItemEntity> getByHighestPriceToLowest (int price);

    @Query("SELECT name FROM ItemEntity")
    Optional<ItemEntity> getAllItems(String name);


}
