package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.Query;
import via.sdj3.grpcserverexample.entities.CategoryEntity;
import via.sdj3.grpcserverexample.entities.ItemEntity;

import java.util.Optional;

public interface CategoryRepository {

    @Query("SELECT c FROM CategoryEntity c WHERE c.categoryName = :categoryName")
    Optional<CategoryEntity> getByCategoryName(String categoryName);

    @Query("SELECT categoryName FROM CategoryEntity")
    Optional<CategoryEntity> getAllCategories(String categoryName);
}
