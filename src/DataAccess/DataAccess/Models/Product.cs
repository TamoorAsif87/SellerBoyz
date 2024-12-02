namespace DataAccess.Models;

public class Product:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<string> Images { get; set; }
    public int TotalSold { get; set; }
    public int InInventory { get; set; }
    public Guid ProductCollectionId { get; set; }
    public ProductCollection? ProductCollection { get; set; }
    public ICollection<ProductTag>? Tags { get; set; }
}
