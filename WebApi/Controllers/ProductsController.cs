using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController :ControllerBase
{
    private readonly IDataService _dataService;

    public ProductsController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _dataService.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}
