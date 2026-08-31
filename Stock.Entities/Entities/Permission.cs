namespace Stock.Entities.Entities
{
    public class Permission
    {
        public int Id  { get; set; }
        public int UserProfileId { get; set; }
        public string PageLink { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}