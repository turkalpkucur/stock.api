using AutoMapper;
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
        private readonly IMapper _mapper;
        public ProductGroupController(IProductGroupService productGroupService, IMapper mapper)
        {
            _productGroupService = productGroupService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductGroupRequestDto productGroup)
        {
            ProductGroup newProductGroup = _mapper.Map<ProductGroup>(productGroup);

            var result =
                await _productGroupService.InsertAsync(newProductGroup);

            return Ok(result);
        }
    }
}
