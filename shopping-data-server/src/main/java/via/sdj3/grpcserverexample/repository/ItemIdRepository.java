package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.ItemEntityId;

@Repository
public interface ItemIdRepository extends JpaRepository<ItemEntityId, Integer>
{
}
