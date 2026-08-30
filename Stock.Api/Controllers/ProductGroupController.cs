using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.ProductGroup;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;
        private readonly IMapper _mapper;
        public ProductGroupController(IProductGroupService productGroupService, IMapper mapper)
        {
            _productGroupService = productGroupService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _productGroupService.ListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(ProductGroupInsertRequestDto productGroup)
        {
            ProductGroup newProductGroup = _mapper.Map<ProductGroup>(productGroup);

            var result =
                await _productGroupService.InsertAsync(newProductGroup);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductGroupUpdateRequestDto productGroup)
        {
            ProductGroup updatedProductGroup = _mapper.Map<ProductGroup>(productGroup);

            var result = await _productGroupService.UpdateAsync(updatedProductGroup);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productGroupService.DeleteAsync(id);

            return Ok();
        }

    
    }
}
