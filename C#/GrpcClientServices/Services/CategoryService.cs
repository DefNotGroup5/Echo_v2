using Domain.Account.Models;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class CategoryService 
{
   private readonly GrpcChannel _channel;
    
    public CategoryService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }


   public async Task<Category?> AddCategoryAsync(Category category)
   {
     try
     {
          GrpcCategory categoryToAdd = GenerateGrpcCategory(category);
         var client = new GrpcClientServices.CategoryService.CategoryServiceClient(_channel);
         var reply = await client.AddCategoryAsync(new AddCategoryRequest()
         {
             Category = categoryToAdd
         });
         Category? categoryToReturn = GenerateCategory(reply.Category);
         Console.WriteLine(reply);
         return categoryToReturn;
     }
     catch (Exception e)
     {
         Console.WriteLine(e.Message);
     }

     return null;
   }

   public async Task<Category?> GetCategoryByNameAsync(string? categoryName)
   {
       try
       {
           var client = new GrpcClientServices.CategoryService.CategoryServiceClient(_channel);
           var reply = await client.GetCategoryByNameAsync(new GetCategoryByNameRequest()
           {
               Name = categoryName
           });
           Category? category = GenerateCategory(reply.Category);
           return category;
       }
       catch (Exception e)
       {
           Console.WriteLine(e.Message);
       }

       return null;
   }


  public async Task<ICollection<Category?>> GetAllCategories()
   {
       try
       {
           var client = new GrpcClientServices.CategoryService.CategoryServiceClient(_channel);
           var reply = await client.GetAllCategoriesAsync(new GetAllCategoriesRequest());
           
           ICollection<Category?> categories = new List<Category?>();
           foreach (var category in reply.Categories)
           {
               categories.Add(GenerateCategory(category));
           }
           return categories;
       }
       catch (Exception e)
       {
           Console.WriteLine(e.Message);
       }

       return null;
   }

   public async Task DeleteCategory(string name)
   {
       try
       {
           var client = new GrpcClientServices.CategoryService.CategoryServiceClient(_channel);
           var reply = await client.DeleteCategoryAsync(new DeleteCategoryRequest()
           {
                Name =  name
           });
           DeleteCategory(name);
       }
       catch (Exception e)
       {
           Console.WriteLine(e.Message);
       }
   }

   private Category? GenerateCategory(GrpcCategory category)
   {
       Category? generatedCategory = null;

       if (category != null)
           generatedCategory = new Category(category.Name)
           {
               
               Id = category.Id
           };

       return generatedCategory;
   }


   private GrpcCategory GenerateGrpcCategory(Category category)
   {

       GrpcCategory generatedGrpcCategory = new GrpcCategory()
       {
           Id = category.Id,
           Name = category.CategoryName,
       };

       return generatedGrpcCategory;
   }
   
}