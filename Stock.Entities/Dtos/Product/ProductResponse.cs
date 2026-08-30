namespace Stock.Entities.Dtos.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }

        public string? Description { get; set; }
    }
}
