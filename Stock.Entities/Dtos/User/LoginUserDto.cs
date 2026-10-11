using Stock.Entities.Dtos.Permission;

namespace Stock.Entities.Dtos.User
{
    public class LoginUserDto : Stock.Entities.Entities.User
    {
        public int UserProfileId { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
