namespace Stock.Entities.Dtos.Product
{
    public class ProductUpdateRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int ProductGroupId { get; set; }
        public string? Description { get; set; }
    }
}
