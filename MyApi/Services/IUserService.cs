public interface IUserService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user object to register.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>A Task that returns the registered user object on success, null on failure.</returns>
    Task<User> RegisterUser(User user, string password);

    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A Task that returns the user object with the specified username, null if not found.</returns>
    Task<User> GetUserByUsername(string username);

    // You can add other user-related methods here (e.g., update user information, assign roles)
}
