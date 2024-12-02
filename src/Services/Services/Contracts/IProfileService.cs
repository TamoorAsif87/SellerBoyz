namespace Services.Contracts;

public interface IProfileService
{
    Task UpdateProfile(ProfileDto profile,string userName);
    Task UpdatePassword(string userName, string oldPassword,string newPassword);
    Task<ProfileDto> GetProfile(string userName);
}
