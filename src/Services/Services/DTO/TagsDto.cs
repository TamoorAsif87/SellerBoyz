namespace Services.DTO;

public class TagsDto
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Tag { get; set; }
}
