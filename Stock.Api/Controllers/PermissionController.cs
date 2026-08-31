using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.Permission;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        public PermissionController(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> InsertAsync(PermissionInsertRequestDto permission)
        {
            Permission newPermission = _mapper.Map<Permission>(permission);
            Permission result = await _permissionService.InsertAsync(newPermission);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PermissionUpdateRequestDto permission)
        {
            Permission newPermission = _mapper.Map<Permission>(permission);
            Permission result = await _permissionService.UpdateAsync(newPermission);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _permissionService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            List<PermissionResponse> permissions = await _permissionService.ListAsync();
            return Ok(permissions);
        }
    }
}
