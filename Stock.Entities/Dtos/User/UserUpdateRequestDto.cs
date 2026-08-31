namespace Stock.Entities.Dtos.User
{
    public  class UserUpdateRequestDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int UserProfileId { get; set; }
    }
}
