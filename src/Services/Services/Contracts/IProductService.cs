using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(Guid Id);
    Task CreateAsync(ProductDto productDto);
    Task UpdateAsync(ProductDto productDto,Guid Id);
    Task DeleteAsync(Guid Id);

    Task AddTag(Guid TagId, Guid ProductId);
    Task RemoveTag(Guid TagId, Guid ProductId);

    Task AddProductImage(Guid ProductId,IFormFile formFile);
    Task RemoveProductImage(Guid ProductId,string ImageName);
}
