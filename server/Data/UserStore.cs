public class UserStore
{
    private List<User> users = new List<User>();

    public void addUser(User user)
    {
        users.Add(user);
    }

    public IReadOnlyList<User> getUsers()
    {
        return users.AsReadOnly();
    }
}
