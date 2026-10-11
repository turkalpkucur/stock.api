namespace Stock.Entities.Dtos.User
{
    public class UserInsertRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserProfileId { get; set; }
    }
}
