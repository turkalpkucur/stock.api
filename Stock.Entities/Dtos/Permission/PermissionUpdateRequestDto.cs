namespace Stock.Entities.Dtos.Permission
{
    public class PermissionUpdateRequestDto
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string PageLink { get; set; }
    }
}
