using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.Product;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductInsertRequestDto product)
    {
        Product newProduct = _mapper.Map<Product>(product);
        Product result= await _productService.InsertAsync(newProduct);
        return Ok(result);
    }
}