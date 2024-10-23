using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public CategoriesController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = nameof(GetCategories))]
    public IActionResult GetCategories(int page = 0, int pageSize = 2)
    {
        var categories = _dataService
            .GetCategories(page, pageSize)
            .Select(CreateCategoryModel);
        var numberOfItems = _dataService.NumberOfCategories();
        object result = CreatePaging(
            nameof(GetCategories),
            page, 
            pageSize, 
            numberOfItems, 
            categories);
        return Ok(result);
    }

    


    [HttpGet("{id}", Name = nameof(GetCategory))]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }
        var model = CreateCategoryModel(category);

        return Ok(model);
    }

   

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryModel model)
    {
        var category = _dataService.CreateCategory(model.Name, model.Description);
        return Ok(CreateCategoryModel(category));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var result = _dataService.DeleteCategory(id);

        if (result)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, UpdateCategoryModel model)
    {
        var category = _dataService.GetCategory(id);

        if(category == null)
        {
            return NotFound();
        }

        

        category.Name = model.Name;
        category.Description = model.Description;


        _dataService.UpdateCategory(category);

        return NoContent();
    }



    private CategoryModel? CreateCategoryModel(Category? category)
    {
        if(category == null)
        {
            return null;
        }

        var model = category.Adapt<CategoryModel>();
        model.Url = GetUrl(category.Id);

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(
            HttpContext, 
            nameof(GetCategory), new { id });
    }

    private string? GetLink(string linkName, int page, int pageSize)
    {
        return _linkGenerator.GetUriByName(
                    HttpContext,
                    linkName,
                    new { page, pageSize }
                    );
    }

    private object CreatePaging<T>(string linkName, int page, int pageSize, int total, IEnumerable<T?> items)
    {
        

        var numberOfPages =
            (int)Math.Ceiling(total / (double)pageSize);

        var curPage = GetLink(linkName, page, pageSize);

        var nextPage =
            page < numberOfPages - 1
            ? GetLink(linkName, page + 1, pageSize)
            : null;

        var prevPage =
            page > 0
            ? GetLink(linkName, page - 1, pageSize)
            : null;

        var result = new
        {
            CurPage = curPage,
            NextPage = nextPage,
            PrevPage = prevPage,
            NumberOfItems = total,
            NumberPages = numberOfPages,
            Items = items
        };
        return result;
    }
}
