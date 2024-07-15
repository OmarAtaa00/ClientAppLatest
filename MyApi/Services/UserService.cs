using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> RegisterUser(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return null; // Registration failed
        }

        return user; // Registration successful, return the user object
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }
}
