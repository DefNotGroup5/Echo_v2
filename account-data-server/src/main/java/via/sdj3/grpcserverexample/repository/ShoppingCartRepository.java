package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;
import via.sdj3.grpcserverexample.entities.ShoppingCartEntity;

@Repository
public interface ShoppingCartRepository extends JpaRepository<ShoppingCartEntity, Integer> {
    @Transactional
    @Modifying
    @Query("DELETE FROM ShoppingCartEntity s WHERE s.customer_id = :customerId")
    void deleteByCustomerId(int customerId);
}
