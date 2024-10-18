using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    IDataService _dataService;

    public CategoriesController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok(_dataService.GetCategories());
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);

        if(category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryModel model)
    {
        var category = _dataService.CreateCategory(model.Name, model.Description);
        return Ok(category);
    }
}
