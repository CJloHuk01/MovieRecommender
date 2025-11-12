using System.ComponentModel.DataAnnotations;

namespace MovieRecommender.Models.DTO
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty; 

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; } = 2;
    }
}
