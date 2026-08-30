using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Entities.Entities
{
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
