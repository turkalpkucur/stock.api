using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductGroupRequestDto productGroup)
        {
            ProductGroup newProductGroup = new ProductGroup()
            {
                Name= productGroup.Name
            };
            
            var result =
                await _productGroupService.InsertAsync(newProductGroup);

            return Ok(result);
        }
    }
}
