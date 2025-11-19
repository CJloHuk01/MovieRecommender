using System.ComponentModel.DataAnnotations;

namespace MovieRecommender.Models.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Необходимо ввести логин или почту")]
        public string LoginOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [MinLength(8, ErrorMessage = "Минимальная длина пароля 8 символов")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Необходимо указать роль")]
        public int RoleId { get; set; } = 2;
    }
}
