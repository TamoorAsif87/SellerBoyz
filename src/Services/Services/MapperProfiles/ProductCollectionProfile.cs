namespace Services.MapperProfiles;

public class ProductCollectionProfile:Profile
{
    public ProductCollectionProfile()
    {
        ProductCollectionMaps();
    }

    private void ProductCollectionMaps()
    {
        CreateMap<ProductCollection,ProductCollectionDto>().ReverseMap();
    }
}
