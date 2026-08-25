using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Entities.Entities
{
    public class ProductGroup
    {
        [Column("product_group_id")]
        public int Id { get; set; }

        public string Name { get; set; }
 
    }
}
