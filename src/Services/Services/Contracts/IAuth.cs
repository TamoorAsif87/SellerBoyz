namespace Services.Contracts;
public interface IAuth
{
    Task RegisterUer(RegisterDto registerDto);
    Task LoginUser(LoginDto loginDto);
    Task Logout();
}
