namespace Services.Contracts;

public interface ITagService
{
    Task<IEnumerable<TagsDto>> GetAllAsync();
    Task<TagsDto> GetByIdAsync(Guid TagId);
    Task CreateAsync(TagsDto tagDto);
    Task UpdateAsync(TagsDto tag,Guid TagId);
    Task DeleteAsync(Guid TagId);
}
