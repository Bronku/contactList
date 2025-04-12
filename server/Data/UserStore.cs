public class UserStore
{
    private List<User> users = new List<User>();

    public void addUser(User user)
    {
        user.Id = users.Count;
        users.Add(user);
    }

    public User getUserById(int id)
    {
        foreach (User user in users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }
        throw new Exception("User not found");
    }

    public IReadOnlyList<User> getUsers()
    {
        return users.AsReadOnly();
    }
}
