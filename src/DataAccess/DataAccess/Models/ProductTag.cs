namespace DataAccess.Models;

public class ProductTag
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid TagsId { get; set; }
    public Product Product { get; set; }
    public Tags Tags { get; set; }
}
