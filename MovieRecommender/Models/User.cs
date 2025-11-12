using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MovieRecommender.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = String.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public object Ratings { get; internal set; }
    }
}
