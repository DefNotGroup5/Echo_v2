package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.ItemEntity;

import java.util.List;
import java.util.Optional;

@Repository

public interface ItemRepository extends JpaRepository<ItemEntity, Integer> {

    @Query("SELECT i FROM ItemEntity i WHERE i.name = :name")
    Optional<ItemEntity> getByName(String name);

    @Query("SELECT i FROM ItemEntity i WHERE i.sellerId = :sellerId")
    List<ItemEntity> getItemsBySeller(@Param("sellerId") int sellerId);

    @Query("SELECT i FROM ItemEntity i WHERE i.id = :id")
    Optional<ItemEntity> getItemById(int id);
    @Query("SELECT i from ItemEntity i ORDER BY i.price DESC ")
    Optional<ItemEntity> getByLowestPriceToHighest (int price);

    @Query("SELECT i from ItemEntity i ORDER BY i.price ASC")
    Optional<ItemEntity> getByHighestPriceToLowest (int price);

    @Query("SELECT name FROM ItemEntity")
    Optional<ItemEntity> getAllItems(String name);
}

