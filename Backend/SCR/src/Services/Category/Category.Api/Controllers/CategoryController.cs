using System.Data;
using Category.Api.Db;
using Category.Api.Filters;
using Category.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Common;

namespace Category.Api.Controllers;

[ApiController]
[Route("category")]
[TypeFilter(typeof(CustomExceptionFilter))]
public class CategoryController : ControllerBase
{


   private CategoryDbContext _context;

   public CategoryController(CategoryDbContext context)
   {
      _context = context;
   }
   [HttpGet]
   [Route("getTree")]
   [ProducesResponseType(200)]
   public async Task<ApiResult<IEnumerable<Tree>>> GetCategoryTrees(int? parentId)
   {
      var iq = await _context.Categories.ToListAsync();
      var data = iq.Select(t=>new Tree()
      {
         Name =t.Name,
         Id = t.Id.ToString(),
         ParentId = t.ParentId.ToString()
      }).ToList();
     var tree=Tree.GetTrees(data,parentId.ToString());
     return ApiResult<IEnumerable<Tree>>.Success(tree);
   }
   [HttpGet]
   [Route("getList")]
   public async Task<ApiResult<IEnumerable<Tree>>> GetCategorys(int? parentId)
   {

      var data = new List<Models.CategoryType>();
  
      data = await _context.Categories.Where(t => t.ParentId == parentId).ToListAsync();

      var result = data.Select(t => new Tree()
      {
         Name =t.Name,
         Id = t.Id.ToString(),
         ParentId =t.ParentId.HasValue?t.ParentId.ToString():null,
         
      });

      return ApiResult<IEnumerable<Tree>>.Success(result);
   }
   
   [HttpGet]
   [Route("get")]
   public async Task<ApiResult<Models.CategoryType>> GetCategory(int id)
   {


      
      Models.CategoryType data = null;
  
      data = await _context.Categories.FirstOrDefaultAsync(t =>t.Id== id);
      

      
      

      return ApiResult<Models.CategoryType>.Success(data);
   }


}