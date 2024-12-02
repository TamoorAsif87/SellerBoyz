using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class RegisterDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
