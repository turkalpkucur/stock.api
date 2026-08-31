namespace Stock.Entities.Dtos.Permission
{
    public class PermissionResponse
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string UserProfileName { get; set; }
        public string PageLink { get; set; }
    }
}
