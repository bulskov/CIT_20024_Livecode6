using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

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
        var products = _dataService
            .GetProducts(page, pageSize)
            .Select(CreateProductListModel);

        var result = CreatePaging(
            nameof(GetProducts),
            page,
            pageSize,
            _dataService.NumberOfProducts(),
            products);

        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(CreateProductModel(product));
    }

    private ProductListModel? CreateProductListModel(Product? product)
    {
        if (product == null)
        {
            return null;
        }

        var model = product.Adapt<ProductListModel>();
        model.Url = GetUrl(nameof(GetProduct), new { product.Id });
        model.Category = product.Category.Name;

        return model;
    }

    private ProductModel? CreateProductModel(Product? product)
    {
        if (product == null)
        {
            return null;
        }

        var model = product.Adapt<ProductModel>();
        model.Url = GetUrl(nameof(GetProduct), new { product.Id });
        model.CategoryUrl = 
            GetUrl(
                nameof(CategoriesController.GetCategory),
                new { product.Category?.Id });

        return model;
    }
}
