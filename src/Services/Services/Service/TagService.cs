using Services.DTO;

namespace Services.Service;

public class TagService(ApplicationDbContext context,IMapper mapper) : ITagService
{
    public async Task CreateAsync(TagsDto tagDto)
    {
        if(context.Tags.ToListAsync().GetAwaiter().GetResult().Where(t => t.Tag.ToLower() == tagDto.Tag.ToLower()).Count() > 0)
        {
            throw new Exception($"Tag name already exist");
        }


        var tag = mapper.Map<Tags>(tagDto);
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid TagId)
    {
        var tag = await GetByIdAsync(TagId);
        context.Tags.Remove(new Tags { Id = tag.Id });
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TagsDto>> GetAllAsync()
    {
        var tags = await context.Tags.ToListAsync();
        return mapper.Map<List<TagsDto>>(tags);  
    }

    public async Task<TagsDto> GetByIdAsync(Guid TagId)
    {
       var tag = await context.Tags.AsNoTracking().SingleOrDefaultAsync(t => t.Id == TagId);
       if (tag == null) throw new Exception("Not found tag");
       return mapper.Map<TagsDto>(tag);
    }

    public async Task UpdateAsync(TagsDto tagsDto, Guid TagId)
    {
        var tag = await context.Tags.SingleOrDefaultAsync(t => t.Id == TagId);
        if (tag == null) throw new Exception("Not found tag");

        if (context.Tags.ToListAsync().GetAwaiter().GetResult().Where(t => t.Tag.ToLower() == tagsDto.Tag.ToLower() && t.Id != tagsDto.Id).Count() > 0)
        {
            throw new Exception($"Tag name already exist");
        }


        tag.Tag = tagsDto.Tag;
        context.Tags.Update(tag);
        await context.SaveChangesAsync();
    }
}
