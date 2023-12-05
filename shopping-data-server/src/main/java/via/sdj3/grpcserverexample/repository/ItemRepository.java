package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import via.sdj3.grpcserverexample.entities.ItemEntity;

import java.util.Optional;

public interface ItemRepository extends JpaRepository<ItemEntity, Integer> {

    @Query("SELECT i FROM ItemEntity i WHERE i.name = :name")
    Optional<ItemEntity> getByName(String name);

    @Query("SELECT i FROM ItemEntity i WHERE i.sellerId = :sellerId")
    Optional<ItemEntity> getBySellerId(int sellerId);

    @Query("SELECT i FROM ItemEntity i WHERE i.itemId = :itemId")
    Optional<ItemEntity> getByItemId(int itemId);

    @Query("SELECT i from ItemEntity i ORDER BY i.price DESC ")
    Optional<ItemEntity> getByLowestPriceToHighest (int price);

    @Query("SELECT i from ItemEntity i ORDER BY i.price ASC")
    Optional<ItemEntity> getByHighestPriceToLowest (int price);

    @Query("SELECT name FROM ItemEntity")
    Optional<ItemEntity> getAllItems(String name);


}

