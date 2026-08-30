using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Entities.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ProductGroupId { get; set; }

        public string? Description { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
   
    }
}
