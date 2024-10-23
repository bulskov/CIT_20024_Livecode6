using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController :BaseController
{
    private readonly IDataService _dataService;

    public ProductsController(
        IDataService dataService,
        LinkGenerator linkGenerator)
        :base(linkGenerator)
    {
        _dataService = dataService;
    }

    [HttpGet(Name = nameof(GetProducts))]
    public IActionResult GetProducts(int page = 0, int pageSize = 10)
    {
        var products = _dataService.GetProducts();

        var result = CreatePaging(
            nameof(GetProducts),
            page,
            pageSize,
            _dataService.NumberOfProducts(),
            products);

        return Ok(result);
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
