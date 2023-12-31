package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.CategoryEntity;
import via.sdj3.grpcserverexample.repository.CategoryRepository;
import via.sdj3.protobuf.category.*;

import java.util.List;
import java.util.Optional;
@GrpcService

public class CategoryServiceImpl extends CategoryServiceGrpc.CategoryServiceImplBase {

    private CategoryRepository categoryRepository;

    public CategoryServiceImpl(CategoryRepository categoryRepository){ this.categoryRepository = categoryRepository;}

    @Override public void addCategory(AddCategoryRequest request, StreamObserver<AddCategoryResponse> responseObserver){
        try{
            CategoryEntity categoryToAdd = generateCategoryEntity(request.getCategory());
            CategoryEntity categoryEntity = categoryRepository.save(categoryToAdd);
            AddCategoryResponse response =  AddCategoryResponse.newBuilder().setCategory(generateGrpcCategory(categoryEntity)).build();
            System.out.println("The category was added");
            responseObserver.onNext(response);
            responseObserver.onCompleted();

        }catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error creating a new category"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }

    }

    @Override public void getCategoryByName(GetCategoryByNameRequest request, StreamObserver<GetCategoryByNameResponse> responseStreamObserver){
        try{
            Optional<CategoryEntity> thisCategory = categoryRepository.getCategoryByName(request.getName());
            if(thisCategory.isPresent()){
                CategoryEntity category = thisCategory.get();
                GetCategoryByNameResponse response = GetCategoryByNameResponse.newBuilder().setCategory(generateGrpcCategory(category)).build();
                responseStreamObserver.onNext(response);
                responseStreamObserver.onCompleted();
            }else {
                Status status = Status.NOT_FOUND.withDescription("Category not found");
                responseStreamObserver.onError(new StatusRuntimeException(status));
            }

        }catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error getting category by name"); //message in case on error
            responseStreamObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }


    @Override public void getAllCategories(GetAllCategoriesRequest request, StreamObserver<GetAllCategoriesResponse> responseStreamObserver){
        try{
            List<CategoryEntity> categories = categoryRepository.findAll();
            GetAllCategoriesResponse.Builder response = GetAllCategoriesResponse.newBuilder();
            for(CategoryEntity category : categories){
                GrpcCategory grpcCategory = generateGrpcCategory(category);
                response.addCategories(grpcCategory);
            }
            System.out.println("The categories are gathered");
            responseStreamObserver.onNext(response.build());
            responseStreamObserver.onCompleted();

        }catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error getting all categories"); //message in case on error
            responseStreamObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }

    }

    @Override
    public void deleteCategory(DeleteCategoryRequest request, StreamObserver<DeleteCategoryResponse> responseStreamObserver) {
        try {
            String categoryName = request.getName();
            Optional<CategoryEntity> category = categoryRepository.getCategoryByName(categoryName);
            if (category.isPresent()) {
                categoryRepository.delete(category.get());
                DeleteCategoryResponse response = DeleteCategoryResponse.newBuilder().build();
                responseStreamObserver.onNext(response);
                responseStreamObserver.onCompleted();
            } else {
                Status status = Status.NOT_FOUND.withDescription("Category not found");
                responseStreamObserver.onError(new StatusRuntimeException(status));
            }
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error deleting category");
            responseStreamObserver.onError(new StatusRuntimeException(status));
        }
    }



    private CategoryEntity generateCategoryEntity(GrpcCategory category)
    {
        CategoryEntity categoryEntity = new CategoryEntity();
        categoryEntity.setCategoryName(category.getName());
        return categoryEntity;
    }


    private GrpcCategory generateGrpcCategory(CategoryEntity categoryEntity)
    {
        return GrpcCategory.newBuilder()
                .setId(categoryEntity.getCategoryId())
                .setName(categoryEntity.getCategoryByName())
                .build();
    }
}