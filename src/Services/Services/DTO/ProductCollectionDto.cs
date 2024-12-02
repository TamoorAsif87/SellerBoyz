namespace Services.DTO;

public class ProductCollectionDto
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string CollectionName { get; set; }
}
