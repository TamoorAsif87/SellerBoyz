namespace Services.DTO;

public class ProductDto
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public List<string>? Images { get; set; }
    [Range(0, 10000)]
    public int TotalSold { get; set; }
    [Range(0,100000)]
    public int InInventory { get; set; }
    public Guid ProductCollectionId { get; set; }
    public ProductCollectionDto? ProductCollection { get; set; }
    public ICollection<ProductTagDto>? Tags { get; set; }
}
