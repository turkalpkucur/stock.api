using Stock.Entities.Dtos.Permission;

namespace Stock.Entities.Dtos.User
{
    public class LoginUserDto
    {
        public int UserProfileId { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
