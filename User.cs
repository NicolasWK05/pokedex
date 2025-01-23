namespace pokedex;

using System;

public class User
{
    protected string Username { get; set; }
    protected string Password { get; set; }
    protected bool IsLoggedIn { get; set; } = false; // default value

    public bool Login(string username, string password)
    {
        List<User> users = ReadUsers();

        foreach (User user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                Console.WriteLine("Login successful");
                IsLoggedIn = true;
                return true;
            }
        }

        Console.WriteLine("Login failed");
        return false;
    }

    public void Logout()
    {
        IsLoggedIn = false;
    }

    public static User Register(string username, string password)
    {
        List<User> users = ReadUsers();

        foreach (User user in users)
        {
            if (user.Username == username)
            {
                Console.WriteLine("Username already exists");
                return null;
            }
        }

        User newUser = new User { Username = username, Password = password };
        users.Add(newUser);
        WriteUsers(users);

        return newUser;
    }

    public bool IsUserLoggedIn()
    {
        return IsLoggedIn;
    }

    protected static List<User> ReadUsers()
    {
        List<User> users = new();
        using (StreamReader sr = new StreamReader("users.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(",");
                users.Add(new User { Username = values[0], Password = values[1] });
            }
        }
        return users;
    }

    protected static void WriteUsers(List<User> users)
    {
        using (StreamWriter sw = new StreamWriter("users.csv"))
        {
            foreach (User user in users)
            {
                sw.WriteLine($"{user.Username},{user.Password}");
            }
        }
    }
}
