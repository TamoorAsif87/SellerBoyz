namespace Services.MapperProfiles;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        ProductMaps();
    }

    private void ProductMaps()
    {
        CreateMap<Product,ProductDto>().ReverseMap();
    }
}
