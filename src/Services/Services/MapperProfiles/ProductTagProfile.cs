
namespace Services.MapperProfiles;

public class ProductTagProfile:Profile
{
    public ProductTagProfile()
    {
        ProductTagMaps();
    }

    private void ProductTagMaps()
    {
        CreateMap<ProductTag, ProductTagDto>().ReverseMap();
    }
}
