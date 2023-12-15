package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import via.sdj3.grpcserverexample.entities.CategoryEntity;

import java.util.List;
import java.util.Optional;

public interface CategoryRepository extends JpaRepository<CategoryEntity, String> {

    @Query("SELECT c FROM CategoryEntity c WHERE c.categoryName = :categoryName")
    Optional<CategoryEntity> getCategoryByName(String categoryName);

    @Query("SELECT categoryName FROM CategoryEntity")
    List<CategoryEntity> getAllCategories();

    @Modifying
    @Query("DELETE FROM CategoryEntity c WHERE c.categoryName = :categoryName")
    void deleteByCategoryName(@Param("categoryName") String categoryName);


}
