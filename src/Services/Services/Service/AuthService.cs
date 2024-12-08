namespace Services.Service;

public class AuthService(ApplicationDbContext context,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager) : IAuth
{
    public async Task RegisterUer(RegisterDto registerDto)
    {
        var role = await context.ApplicationUsers.CountAsync() > 0 ? "user" : "admin";

        var user = new ApplicationUser
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var registerResult = await userManager.CreateAsync(user,registerDto.Password);

        if(!registerResult.Succeeded)
        {
            throw new Exception(ErrorsInString(registerResult.Errors));
        }

        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole { Name = role });
        }

        await userManager.AddToRoleAsync(user, role);
    }

    public async Task LoginUser(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) throw new Exception($"No user with email {loginDto.Email} is found.");

        var checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!checkPassword) throw new Exception("Password is invalid or incorrect");

        await signInManager.SignInAsync(user, loginDto.RememberMe);

    }


    private string ErrorsInString(IEnumerable<IdentityError> identityErrors)
    {
        return string.Join(Environment.NewLine, identityErrors.Select(err => err.Description));
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}
