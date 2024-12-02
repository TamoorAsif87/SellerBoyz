namespace DataAccess.Models;

public class Tags:BaseEntity
{
    public string Tag { get; set; }
    public ICollection<ProductTag>? Products { get; set; }

}
