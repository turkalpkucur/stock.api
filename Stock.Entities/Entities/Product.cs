namespace Stock.Entities.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ProductGroupId { get; set; }

        public string? Description { get; set; }
        public ProductGroup ProductGroup { get; set; } 
    }
}
