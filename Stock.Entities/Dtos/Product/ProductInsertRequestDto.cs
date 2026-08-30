namespace Stock.Entities.Dtos.Product
{
    public class ProductInsertRequestDto
    {
        public string Name { get; set; } 
        public int ProductGroupId { get; set; }
        public string? Description { get; set; }
    }
}
