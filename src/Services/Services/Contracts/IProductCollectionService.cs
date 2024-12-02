namespace Services.Contracts;

public interface IProductCollectionService
{
    Task<IEnumerable<ProductCollectionDto>> GetAllAsync();
    Task<ProductCollectionDto> GetByIdAsync(Guid Id);
    Task CreateAsync(ProductCollectionDto productCollectionDto);
    Task UpdateAsync(ProductCollectionDto productCollectionDto,Guid Id);
    Task DeleteAsync(Guid Id);
}
