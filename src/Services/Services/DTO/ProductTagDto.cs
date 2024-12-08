namespace Services.DTO;

public class ProductTagDto
{
    public Guid Id { get; set; }
    public Guid TagsId { get; set; }
    public TagsDto Tags { get; set; }
}
