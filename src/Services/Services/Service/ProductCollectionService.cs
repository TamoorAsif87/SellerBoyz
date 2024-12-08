

namespace Services.Service;

public class ProductCollectionService(ApplicationDbContext context,IMapper mapper) : IProductCollectionService
{
    public async Task CreateAsync(ProductCollectionDto productCollectionDto)
    {
        if (context.ProductCollections.ToListAsync().GetAwaiter().GetResult().Where(pc => pc.CollectionName.ToLower() == productCollectionDto.CollectionName.ToLower()).Count() > 0)
        {
            throw new Exception($"Collection name already exist");
        }

        var productCollection = mapper.Map<ProductCollection>(productCollectionDto);
        context.ProductCollections.Add(productCollection);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid Id)
    {
        var productCollectionDto = await GetByIdAsync(Id);
        context.ProductCollections.Remove(new ProductCollection { Id = productCollectionDto.Id });
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductCollectionDto>> GetAllAsync()
    {
        var productCollections = await context.ProductCollections.ToListAsync();
        return mapper.Map<List<ProductCollectionDto>>(productCollections);  
    }

    public async Task<ProductCollectionDto> GetByIdAsync(Guid Id)
    {
       var productCollection = await context.ProductCollections.AsNoTracking().SingleOrDefaultAsync(t => t.Id == Id);
       if (productCollection == null) throw new Exception("Not found collection");
       return mapper.Map<ProductCollectionDto>(productCollection);
    }

    public async Task UpdateAsync(ProductCollectionDto productCollectionDto, Guid Id)
    {
        var productCollection = await context.ProductCollections.AsNoTracking().SingleOrDefaultAsync(t => t.Id == Id);
        if (productCollection == null) throw new Exception("Not found collection");

        if (context.ProductCollections.AsNoTracking().ToListAsync().GetAwaiter().GetResult().Where(pc => pc.CollectionName.ToLower() == productCollectionDto.CollectionName.ToLower() && pc.Id != Id).Any())
        {
            throw new Exception($"Collection name already exist");
        }


        productCollection.CollectionName = productCollectionDto.CollectionName;
        context.ProductCollections.Update(productCollection);
        await context.SaveChangesAsync();
    }
}
