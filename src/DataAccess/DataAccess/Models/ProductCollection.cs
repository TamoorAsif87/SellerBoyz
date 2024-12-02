namespace DataAccess.Models;

public class ProductCollection:BaseEntity
{
    public string CollectionName { get; set; }
    public ICollection<Product>? Products { get; set; }
}
