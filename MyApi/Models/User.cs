using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FullName { get; set; }

    // You can add other user-specific properties here
}
