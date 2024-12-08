using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Services.DTO;

public class ProfileDto
{
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? ProfileImage { get; set; }
    [Phone]
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public IFormFile? File { get; set; }
}

public static class ProfileAndUserExtension
{
    public static ProfileDto ConvertUserToProfile(this ApplicationUser user)
    {
        var profileDto = new ProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            Phone = user.Phone,
            Address = user.Address,
            Country = user.Country,
            ProfileImage = user.ProfileImage,
        };

        return profileDto;
    }

    public static ApplicationUser ConvertProfileToUser(this ProfileDto profileDto,ApplicationUser user)
    {
        user.Id = profileDto.Id;
        user.Name = profileDto.Name;
        user.Phone = profileDto.Phone;
        user.Address = profileDto.Address;
        user.Country = profileDto.Country;
        user.ProfileImage = profileDto.ProfileImage;

        return user;
    }
}
