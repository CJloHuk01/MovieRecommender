using System.ComponentModel.DataAnnotations;

namespace MovieRecommender.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        public List<User> Users { get; set; } = new();
    }
}
