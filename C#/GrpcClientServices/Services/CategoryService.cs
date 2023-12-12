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
     /*try
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
     }*/

     return null;
   }

   public async Task<Category?> GetCategoryByEmailAsync(Category? category)
   {
      /* try
       {
           var client = new GrpcClientServices.CategoryService.CategoryServiceClient(_channel);
           var reply = await client.GetByCategoryByEmailAsync(new GetCategoryByNameRequest()
           {
               Category = category
           });
           category = GenerateCategory(reply.Category);
           return category;
       }
       catch (Exception e)
       {
           Console.WriteLine(e.Message);
       }*/

       return null;
   }

  /* private Category? GenerateCategory(GrpcCategory category)
   {
       Category? generatedCategory = null;

       if (category != null)
           generatedCategory = new Category(category.Name)
           {
               CategoryName = category.Name,
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
   }*/
   
}