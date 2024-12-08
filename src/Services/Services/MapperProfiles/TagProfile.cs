namespace Services.MapperProfiles;

public class TagProfile:Profile
{
    public TagProfile()
    {
        TagMaps();
    }

    private void TagMaps()
    {
        CreateMap<Tags,TagsDto>().ReverseMap();
    }
}
