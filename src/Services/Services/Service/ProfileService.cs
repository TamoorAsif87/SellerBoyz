
using AutoMapper;

namespace Services.Service;

public class ProfileService(UserManager<ApplicationUser> userManager, ApplicationDbContext context,IFileUploadService fileUploadService) : IProfileService
{
    public async Task<ProfileDto> GetProfile(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null) throw new Exception("Email does not exist");

        var profile = user.ConvertUserToProfile();
        return profile;
    }

    public async Task UpdateProfile(ProfileDto profile,string userName)
    {
        var user = await userManager.FindByEmailAsync(userName);
        if (user == null) throw new Exception("User not found");

        if(profile.File != null)
        {
            profile.ProfileImage = fileUploadService.ImageUpload(profile.File, "images");
        }

        user = profile.ConvertProfileToUser(user);
        await userManager.UpdateAsync(user);    

    }

    public async Task UpdatePassword(string userName, string oldPassword, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(userName);
        if (user == null) throw new Exception("User not found");

        var result =  await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(err => err.Description)));
        } 
    }
}
