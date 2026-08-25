using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(Product product)
    {
        var result =
            await _productService.InsertAsync(product);

        return Ok(result);
    }
}