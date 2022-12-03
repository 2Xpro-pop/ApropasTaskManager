using ApropasTaskManager.Shared;

namespace ApropasTaskManager.Server.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserProfile Profile { get; set; }

    public UserViewModel(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Profile = user.Profile;
    }

    public void ApplyToUser(User user)
    {
        user.UserName = UserName;
        user.Email = Email;
    }
}
